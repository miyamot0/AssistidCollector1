using Acr.UserDialogs;
using AssistidCollector1.Enums;
using AssistidCollector1.Helpers;
using AssistidCollector1.Models;
using AssistidCollector1.Storage;
using AssistidCollector1.Views;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

            startTime = DateTime.Now;

            taskModels = new List<SleepTasks>();

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Manage Sleep Environment",
                PageDescription = "Sleep environment should be the same in the morning as at bedtime, with no cues that it is daytime.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Address Sleep Motivation",
                PageDescription = "Decrease reinforcement for early awakening (child may play quietly in their room), but may not leave the bedroom.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Detecting Sleep Avoidance",
                PageDescription = "Use an inexpensive motion detector to signal when the child leaves the room. (Something like chimes/bells).",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Maintaining Expectations for Sleep Behavior",
                PageDescription = "If the child awakens early than scheduled, return the child to their bedroom and simply state “it's still night-time, go back to sleep.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Napping Behavior",
                PageDescription = "Decrease day time napping.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Waking Behavior",
                PageDescription = "Establish a consistent wake time, use audible and/or visual cues to signal wake time. When the alarm goes off, enter the child’s bedroom and open the curtains, saying “it’s time to wake up.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Modifying Sleep Conditions",
                PageDescription = "Gradually delay the stimulus (start time of day) by 15 minutes every 2-3 days.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Sleep Environment",
                PageDescription = "The bedroom environment should be dark, quiet, and cool to minimize distractions. Children with autism may be particularly sensitive to these sources of distraction.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Bedtime Routine",
                PageDescription = "Develop a consistent timeline for bedtime routines and stick to them.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Establish a Reasonable Bedtime",
                PageDescription = "Tell your child when it is and, under normal circumstances, put your child to bed at that time every night.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Quiet Time",
                PageDescription = "Institute a \"Quiet Time\" 20-30 minutes before bedtime. During this time, your child can engage in quiet activities such as reading while avoiding activities such as screen time.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Preparing for Bed",
                PageDescription = "A bedtime routine involves preparing for bed (such as getting into pajamas, brushing teeth, going to the bathroom, last drink of water) and activities to relax your children. The bedtime routine, once established, will become a powerful signal to your child that the time for sleep has come.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Sleep and Wake schedule",
                PageDescription = "The schedule should be regular with not much of a difference between the weekday and weekend schedule.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Morning Awakening Time",
                PageDescription = "Keep morning awakening time consistent and reduce or avoid daytime naps so the child does not 'make up' for lost sleep following a difficult night. This will increase the likelihood that they will fall asleep more quickly the next evening.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Avoid caffeine",
                PageDescription = "Avoid caffeine particularly close to bedtime, as it could make it more difficult for your child to fall asleep. Caffeine is often found in tea, chocolate and fizzy drinks.",
            });            

            foreach (SleepTasks item in taskModels)
            {
                cardCheckTemplate = new CardCheckTemplate(item.PageTitle, item.PageDescription);
                earlyAwakeningStackContent.Children.Add(cardCheckTemplate);
            }
        }

        private async void earlyAwakeningButtonBottom_Clicked(object sender, EventArgs e)
        {
            TimeSpan timeDifference = DateTime.Now.Subtract(startTime);

            CardCheckTemplate holder;

            bool isChecked;
            string currentTitle;
            int counter = 0;

            string returnString = "Data,Value" + Environment.NewLine;

            returnString = "Intervention,Early Morning Awakening" + Environment.NewLine;

            foreach (var child in earlyAwakeningStackContent.Children)
            {
                holder = child as CardCheckTemplate;

                if (holder != null)
                {
                    isChecked = getSwitchValue(holder.grid);
                    currentTitle = taskModels[counter].PageTitle;

                    Debug.WriteLineIf(App.Debugging, currentTitle + " = " + isChecked);

                    returnString += currentTitle + ",";
                    returnString += (isChecked) ? "True" : "False";
                    returnString += Environment.NewLine;

                    counter++;
                }
            }

            returnString += "Date," + startTime.Date.ToString() + Environment.NewLine;
            returnString += "Start," + startTime.TimeOfDay.ToString() + Environment.NewLine;
            returnString += "Seconds," + timeDifference.TotalSeconds.ToString() + Environment.NewLine;

            int result = await App.Database.SaveItemAsync(new StorageModel()
            {
                CSV = returnString,
                Intervention = "Early Morning Awakening"
            });

            List<StorageModel> allSavedData = await App.Database.GetDataAsync();

            int number = 0;

            if (allSavedData == null || allSavedData.Count == 0)
            {
                number = 0;
            }
            else
            {
                number = allSavedData.Count - 1;
            }

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
                    await DropboxServer.UploadFile(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(returnString)), number);

                    await Task.Delay(100);
                }
            }

            await Navigation.PopModalAsync();
        }

        /// <summary>
        /// Get Value of Switch
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private bool getSwitchValue(Grid grid)
        {
            Switch temp;

            foreach (var child in grid.Children)
            {
                temp = child as Switch;

                if (temp != null)
                {
                    return temp.IsToggled;
                }
            }

            return false;
        }
    }
}