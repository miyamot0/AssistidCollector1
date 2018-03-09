//----------------------------------------------------------------------------------------------
// <copyright file="TaskMorningAwakening.xaml.cs" 
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
using AssistidCollector1.Models;
using AssistidCollector1.Storage;
using AssistidCollector1.Views;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AssistidCollector1.Tasks
{
    public partial class TaskMorningAwakening : ContentPage
    {
        List<SleepTasks> taskModels;
        CardCheckTemplate cardCheckTemplate;
        DateTime startTime;

        public TaskMorningAwakening()
        {
            InitializeComponent();

            Title = "Early Morning Awakening Strategies";

            startTime = DateTime.Now;

            taskModels = new List<SleepTasks>();

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Manage Sleep Environment",
                PageDescription = "Sleep environment should be the same in the morning as at bedtime, with no cues that it is daytime.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Address Sleep Motivation",
                PageDescription = "Decrease reinforcement for early awakening (child may play quietly in their room), but may not leave the bedroom.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Detecting Sleep Avoidance",
                PageDescription = "Use an inexpensive motion detector to signal when the child leaves the room. (Something like chimes/bells).",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Maintaining Expectations for Sleep Behaviour",
                PageDescription = "If the child awakens early than scheduled, return the child to their bedroom and simply state “it's still night-time, go back to sleep.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Napping Behaviour",
                PageDescription = "Decrease day time napping.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Waking Behaviour",
                PageDescription = "Establish a consistent wake time, use audible and/or visual cues to signal wake time. When the alarm goes off, enter the child’s bedroom and open the curtains, saying “it’s time to wake up. Gradually delay the stimulus (start time of day) by 15minutes every 2-3 days.",
            });

            ContentHelper.AddSleepHygieneContent(taskModels);

            ContentHelper.AddSleepRelaxationContent(taskModels);

            foreach (SleepTasks item in taskModels)
            {
                cardCheckTemplate = new CardCheckTemplate(item.PageTitle, item.PageDescription, item.Strategy);
                earlyAwakeningStackContent.Children.Add(cardCheckTemplate);
            }

            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, true);
            NavigationPage.SetBackButtonTitle(this, "Back");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void earlyAwakeningButtonBottom_Clicked(object sender, EventArgs e)
        {
            if ((sender as Button) != null) { (sender as Button).IsEnabled = false; }

            string returnString = ViewTools.CommaSeparatedValue("Data,Value", "Intervention,Early Morning Awakening",
                earlyAwakeningStackContent, taskModels,
                startTime, DateTime.Now.Subtract(startTime));

            int result = await App.Database.SaveItemAsync(new StorageModel()
            {
                CSV = returnString,
                Intervention = "Early Morning Awakening"
            });

            if (CrossConnectivity.Current.IsConnected)
            {
                CancellationTokenSource cancelSrc = new CancellationTokenSource();
                ProgressDialogConfig config = new ProgressDialogConfig()
                    .SetTitle("Uploading to Server")
                    .SetIsDeterministic(false)
                    .SetMaskType(MaskType.Black)
                    .SetCancel(onCancel: cancelSrc.Cancel);

                using (IProgressDialog progress = UserDialogs.Instance.Progress(config))
                {
                    await DropboxServer.UploadFile(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(returnString)), App.Database.GetLargestID());

                    await Task.Delay(100);
                }
            }

            App.RefreshServer = true;

            await Navigation.PopAsync();
        }
    }
}