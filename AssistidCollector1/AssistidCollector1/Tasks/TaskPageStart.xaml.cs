//----------------------------------------------------------------------------------------------
// <copyright file="TaskPageStart.xaml.cs" 
// Copyright February 2, 2018 Shawn Gilroy
//
// This file is part of AssistidCollector2
//
// AssistidCollector2 is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, version 3.
//
// AssistidCollector2 is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with AssistidCollector2.  If not, see http://www.gnu.org/licenses/. 
// </copyright>
//
// <summary>
// The AssistidCollector2 is a tool to assist clinicans and researchers in the treatment of communication disorders.
// 
// Email: shawn(dot)gilroy(at)temple.edu
//
// </summary>
//----------------------------------------------------------------------------------------------

using Acr.UserDialogs;
using AssistidCollector1.Enums;
using AssistidCollector1.Helpers;
using AssistidCollector1.Interfaces;
using AssistidCollector1.Models;
using AssistidCollector1.Views;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Dropbox.Api.Files;
using AssistidCollector1.Storage;
using System.Linq;

namespace AssistidCollector1.Tasks
{
    public partial class TaskPageStart : ContentPage
    {
        List<SleepTasks> taskModels;
        TapGestureRecognizer tapGestureRecognizer;
        CardViewTemplate cardViewTemplate;

        /// <summary>
        /// Starting page for app
        /// </summary>
        public TaskPageStart()
        {
            InitializeComponent();

            taskModels = new List<SleepTasks>();

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                PageTitle = "Night-time Awakenings",
                PageDescription = "For instructions on how to work on your child waking at night, you can follow the instructions provided here",
                PageButton = "Select this option for more information on Night-time awakenings",
                PageImage = "NightTimeAwakening.png"
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                PageTitle = "Bedtime Resistance",
                PageDescription = "This section provides information on how to address resistance to bedtime routines",
                PageButton = "Select this option for ways to address Bedtime Resistance",
                PageImage = "BedtimeResistance.png"
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Co-sleeping",
                PageDescription = "This area focuses on strategies for managing co-sleeping behavior.",
                PageButton = "Select this option for co-sleeping behavior strategies",
                PageImage = "CoSleeping.png"
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                PageTitle = "Late Onset of Sleep",
                PageDescription = "This section focuses on how to address times when children do not get tired until very late.",
                PageButton = "Select this option for late-onset sleep strategies",
                PageImage = "LateOnsetSleep.png"
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Early morning awakenings",
                PageDescription = "Information in this area discusses early morning sleep awakenings.",
                PageButton = "Select this option for tips on addressing early morning awakenings.",
                PageImage = "EarlyMorningAwakening.png"
            });

            tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnTapGestureRecognizerTapped;

            foreach (SleepTasks item in taskModels)
            {
                cardViewTemplate = new CardViewTemplate(item);
                cardViewTemplate.GestureRecognizers.Add(tapGestureRecognizer);

                startPageStackContent.Children.Add(cardViewTemplate);
            }

            // Remove this toolbar item if on iOS, is unnecessary
            if (Device.RuntimePlatform == Device.iOS)
            {
                ToolbarItems.Remove(settingsItem);
            }
        }

        /// <summary>
        /// Gesture recognizer, link to individual pages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            var getCardTapped = sender as CardViewTemplate;

            ContentPage view = null;

            if (getCardTapped != null)
            {
                switch (getCardTapped.PageId)
                {
                    case Identifiers.Pages.Start:
                        /* Stubbed out */

                        break;

                    case Identifiers.Pages.NightAwakenings:
                        view = new TaskNightAwakening();

                        break;

                    case Identifiers.Pages.BedtimeResistance:
                        view = new TaskBedtimeResistance();

                        break;

                    case Identifiers.Pages.CoSleeping:
                        view = new TaskCosleeping();

                        break;

                    case Identifiers.Pages.EarlyMorningAwakenings:
                        view = new TaskMorningAwakening();

                        break;

                    case Identifiers.Pages.LateOnset:
                        view = new TaskSleepOnset();

                        break;
                }

                App.RefreshServer = false;

                view.Disappearing += (sender2, e) => 
                {
                    if (App.RefreshServer)
                    {
                        ToolbarItem_Clicked(sender2, e);
                    }

                    App.RefreshServer = false;
                };

                await Navigation.PushAsync(view, true);
            }
        }

        /// <summary>
        /// Help menu button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void startPageButtonBottom_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TaskHelp());
        }

        /// <summary>
        /// Force re-sync of data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Debug.WriteLineIf(App.Debugging, "Update_Clicked()");

            if (!CrossConnectivity.Current.IsConnected)
            {
                return;
            }

            //int count = 0;

            CancellationTokenSource cancelSrc = new CancellationTokenSource();
            ProgressDialogConfig config = new ProgressDialogConfig()
                .SetTitle("Syncing with server")
                .SetIsDeterministic(false)
                .SetMaskType(MaskType.Black)
                .SetCancel(onCancel: cancelSrc.Cancel);

            using (IProgressDialog progress = UserDialogs.Instance.Progress(config))
            {
                try
                {
                    ListFolderResult serverFiles = await DropboxServer.CountIndividualFiles();
                    List<StorageModel> currentData = await App.Database.GetDataAsync();

                    if (serverFiles == null || currentData == null || cancelSrc.IsCancellationRequested)
                    {
                        // Nothing.. just move on
                    }
                    else if (currentData.Count == serverFiles.Entries.Count)
                    {
                        // Same.. no worries
                    }
                    else if (currentData.Count > serverFiles.Entries.Count)
                    {
                        List<int> localIds = currentData.Select(l => l.ID).ToList();

                        List<string> remoteIdsStr = serverFiles.Entries.Select(r => r.Name).ToList();
                        remoteIdsStr = remoteIdsStr.Select(r => r.Split('_')[1]).ToList();
                        remoteIdsStr = remoteIdsStr.Select(r => r.Split('.')[0]).ToList();

                        List<int> remoteIds = remoteIdsStr.Select(r => int.Parse(r)).ToList();

                        var missing = localIds.Except(remoteIds);

                        for (int count = 0; count < missing.Count() && !cancelSrc.IsCancellationRequested; count++)
                        {
                            if (cancelSrc.IsCancellationRequested)
                            {
                                continue;
                            }
                            else
                            {
                                await Task.Delay(App.DropboxDeltaTimeout);
                            }

                            progress.Title = "Uploading File " + (count + 1) + " of " + missing.Count().ToString();

                            var mStorageModel = currentData.Single(m => m.ID == missing.ElementAt(count));

                            await DropboxServer.UploadFile(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(mStorageModel.CSV)), mStorageModel.ID);
                        }

                        /*
                        foreach (int index in missing)
                        {
                            if (cancelSrc.IsCancellationRequested)
                            {
                                continue;
                            }
                            else
                            {
                                await Task.Delay(App.DropboxDeltaTimeout);
                            }

                            progress.Title = "Uploading File " + (count + 1) + " of " + missing.Count().ToString();

                            var mStorageModel = currentData.Single(m => m.ID == index);

                            await DropboxServer.UploadFile(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(mStorageModel.CSV)), mStorageModel.ID);


                            count++;
                        }
                        */
                    }
                }
                catch (Exception exc)
                {
                    Debug.WriteLineIf(App.Debugging, exc.ToString());
                }
            }

            // TODO: upload feedback
        }

        /// <summary>
        /// Access settings, with some baby-sitting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {
            DependencyService.Get<InterfaceAdministrator>().AccessSettings();
        }

        /// <summary>
        /// Send message to cloud
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void ToolbarItem_Clicked_2(object sender, EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                PromptResult userMessage = await UserDialogs.Instance.PromptAsync(new PromptConfig()
                    .SetMessage("Enter your message")
                    .SetText(""));

                if (userMessage.Ok && userMessage.Text.Trim().Length > 0)
                {
                    try
                    {
                        CancellationTokenSource cancelSrc = new CancellationTokenSource();
                        ProgressDialogConfig config = new ProgressDialogConfig()
                            .SetTitle("Uploading to Server")
                            .SetIsDeterministic(false)
                            .SetMaskType(MaskType.Black)
                            .SetCancel(onCancel: cancelSrc.Cancel);

                        using (IProgressDialog progress = UserDialogs.Instance.Progress(config))
                        {
                            await DropboxServer.UploadMessage(userMessage.Text);

                            await Task.Delay(500);
                        }                        

                        UserDialogs.Instance.Toast(new ToastConfig("Successfully Sent")
                            .SetDuration(TimeSpan.FromSeconds(3))
                            .SetPosition(ToastPosition.Bottom));
                    }
                    catch
                    {
                        UserDialogs.Instance.Toast(new ToastConfig("Error")
                            .SetDuration(TimeSpan.FromSeconds(3))
                            .SetPosition(ToastPosition.Bottom));
                    }
                }
            }
            else
            {
                UserDialogs.Instance.Toast(new ToastConfig("No Internet Connected")
                    .SetDuration(TimeSpan.FromSeconds(3))
                    .SetPosition(ToastPosition.Bottom));
            }
        }

        /// <summary>
        /// Handles the feedback clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void Handle_Feedback_ClickedAsync(object sender, System.EventArgs e)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var newView = new TaskFeedback();

                App.RefreshServer = false;

                newView.Disappearing += (sender2, e2) =>
                {
                    if (App.RefreshServer)
                    {
                        ToolbarItem_Clicked(sender2, e2);
                    }

                    App.RefreshServer = false;
                };


                await Navigation.PushModalAsync(new NavigationPage(newView), true);
            }
            else
            {
                UserDialogs.Instance.Alert("Please connect to the internet", Title = "Error", okText:"Okay");
            }
        }

        /// <summary>
        /// Base methods
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();

            return true;
        }
    }
}