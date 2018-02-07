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
    public partial class BedtimeResistance : ContentPage
    {
        List<SleepTasks> taskModels;
        CardCheckTemplate cardCheckTemplate;
        DateTime startTime;

        public BedtimeResistance()
        {
            InitializeComponent();

            startTime = DateTime.Now;

            taskModels = new List<SleepTasks>();

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Bedtime Pass: Step 1",
                PageDescription = "Establish a regular bedtime routine.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Bedtime Pass: Step 2",
                PageDescription = "Put child to bed at the same time every night.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Bedtime Pass: Step 3",
                PageDescription = "Give him/her 2 bedtime passes.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Bedtime Pass: Step 4",
                PageDescription = "When child leaves his/her room s/he must surrender a pass.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Bedtime Pass: Step 4a",
                PageDescription = "1 pass gets one “free trip” out of the room or one parent visit.",
            });
            
            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Bedtime Pass: Step 4b",
                PageDescription = "Visits should be short (less than 3 minutes) & have a specific purpose(drink, hug).",
            });
            
            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Bedtime Pass: Step 5",
                PageDescription = "Once passes are gone ignore all bids for attention.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Bedtime Pass: Step 5a",
                PageDescription = "If child leaves their room after all their passes are gone, guide them calmly back to the room while ignoring them(no speaking, avoid looking at them).",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Bedtime Pass: Step 6",
                PageDescription = "Allow the child to select a prize/reward in the morning if they have one or more un-used passes from the previous night.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Bedtime Pass: Step 7",
                PageDescription = "Repeat each night consistently.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Bedtime Pass: Step 8",
                PageDescription = "Reduce to 1 bedtime pass.",
            });
            
            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Manage Sleep Environment",
                PageDescription = "Sleep environment should be the same in the morning as at bedtime, with no cues that it is daytime.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Address Sleep Motivation",
                PageDescription = "Decrease reinforcement for early awakening (child may play quietly in their room), but may not leave the bedroom.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Detecting Sleep Avoidance",
                PageDescription = "Use an inexpensive motion detector to signal when the child leaves the room. (Something like chimes/bells).",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Maintaining Expectations for Sleep Behavior",
                PageDescription = "If the child awakens early than scheduled, return the child to their bedroom and simply state “it's still night-time, go back to sleep.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Napping Behavior",
                PageDescription = "Decrease day time napping.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Waking Behavior",
                PageDescription = "Establish a consistent wake time, use audible and/or visual cues to signal wake time. When the alarm goes off, enter the child’s bedroom and open the curtains, saying “it’s time to wake up.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Modifying Sleep Conditions",
                PageDescription = "Gradually delay the stimulus (start time of day) by 15 minutes every 2-3 days.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Sleep Environment",
                PageDescription = "The bedroom environment should be dark, quiet, and cool to minimize distractions. Children with autism may be particularly sensitive to these sources of distraction.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Bedtime Routine",
                PageDescription = "Develop a consistent timeline for bedtime routines and stick to them.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Establish a Reasonable Bedtime",
                PageDescription = "Tell your child when it is and, under normal circumstances, put your child to bed at that time every night.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Quiet Time",
                PageDescription = "Institute a \"Quiet Time\" 20-30 minutes before bedtime. During this time, your child can engage in quiet activities such as reading while avoiding activities such as screen time.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Preparing for Bed",
                PageDescription = "A bedtime routine involves preparing for bed (such as getting into pajamas, brushing teeth, going to the bathroom, last drink of water) and activities to relax your children. The bedtime routine, once established, will become a powerful signal to your child that the time for sleep has come.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Sleep and Wake schedule",
                PageDescription = "The schedule should be regular with not much of a difference between the weekday and weekend schedule.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Morning Awakening Time",
                PageDescription = "Keep morning awakening time consistent and reduce or avoid daytime naps so the child does not 'make up' for lost sleep following a difficult night. This will increase the likelihood that they will fall asleep more quickly the next evening.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Avoid caffeine",
                PageDescription = "Avoid caffeine particularly close to bedtime, as it could make it more difficult for your child to fall asleep. Caffeine is often found in tea, chocolate and fizzy drinks.",
            });

            ContentHelper.AddSleepRelaxationContent(taskModels);

            foreach (SleepTasks item in taskModels)
            {
                cardCheckTemplate = new CardCheckTemplate(item.PageTitle, item.PageDescription, item.Strategy);
                bedtimeResistanceStackContent.Children.Add(cardCheckTemplate);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void bedtimeResistanceButtonBottom_Clicked(object sender, EventArgs e)
        {
            if ((sender as Button) != null) { (sender as Button).IsEnabled = false; }

            string returnString = ViewTools.CommaSeparatedValue("Data,Value", "Intervention,Bedtime Resistance",
                bedtimeResistanceStackContent, taskModels,
                startTime, DateTime.Now.Subtract(startTime));

            int result = await App.Database.SaveItemAsync(new StorageModel()
            {
                CSV = returnString,
                Intervention = "Bedtime Resistance"
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

            await Navigation.PopModalAsync();
        }
    }
}