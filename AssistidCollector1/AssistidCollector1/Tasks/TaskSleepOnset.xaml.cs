//----------------------------------------------------------------------------------------------
// <copyright file="TaskSleepOnset.xaml.cs" 
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
    public partial class TaskSleepOnset : ContentPage
    {
        List<SleepTasks> taskModels;
        CardCheckTemplate cardCheckTemplate;
        DateTime startTime;

        public TaskSleepOnset()
        {
            InitializeComponent();

            Title = "Late Sleep Onset Strategies";

            startTime = DateTime.Now;

            taskModels = new List<SleepTasks>();

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Associate Bedroom with Sleep",
                PageDescription = "The goal is to associate the child's bed with falling asleep quickly.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Create a \"Sleep-only\" Bedroom Environment",
                PageDescription = "It is important that your child does not use the bedroom for anything else except sleep. (no TV or computer games, etc). A sleep friendly environment with no stimulation is important.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Increase Motivation for Sleep",
                PageDescription = "Move bedtime later than usual to ensure that the child is very tired on the first night.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Gradually Adjust Bedtimes",
                PageDescription = "Every 2-3 days the bedtime can then be moved 15 minutes earlier if the child fell asleep quickly (within 15-30 min) until an age appropriate bedtime has been achieved.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Reward Positive Relaxation Behavior",
                PageDescription = "Leave the bedroom when your child is still awake and provide verbal praise and positive touch if, and only if, the child is lying quietly in bed. ",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Wake Time Routines",
                PageDescription = "Establish a consistent wake time, use audible and/or visual cues to signal wake time. When the alarm goes off, enter the child’s bedroom and open the curtains, saying “it’s time to wake up.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Block Napping",
                PageDescription = "No day time napping.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Reward Positive Sleep Behavior",
                PageDescription = "Remember to reward your child the next morning for falling asleep quickly.",
            });

            ContentHelper.AddSleepHygieneContent(taskModels);

            ContentHelper.AddSleepRelaxationContent(taskModels);

            foreach (SleepTasks item in taskModels)
            {
                cardCheckTemplate = new CardCheckTemplate(item.PageTitle, item.PageDescription, item.Strategy);
                sleepOnsetStackContent.Children.Add(cardCheckTemplate);
            }

            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, true);
            NavigationPage.SetBackButtonTitle(this, "Back");
        }

        /// <summary>
        /// Clicked Save Button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void sleepOnsetButtonBottom_Clicked(object sender, EventArgs e)
        {
            if ((sender as Button) != null) { (sender as Button).IsEnabled = false; }

            string returnString = ViewTools.CommaSeparatedValue("Data,Value", "Intervention,Sleep Onset",
                sleepOnsetStackContent, taskModels,
                startTime, DateTime.Now.Subtract(startTime));

            int result = await App.Database.SaveItemAsync(new StorageModel()
            {
                CSV = returnString,
                Intervention = "Sleep Onset"
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