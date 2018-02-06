﻿using Acr.UserDialogs;
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
    public partial class TaskCosleeping : ContentPage
    {
        List<SleepTasks> taskModels;
        CardCheckTemplate cardCheckTemplate;
        DateTime startTime;

        public TaskCosleeping()
        {
            InitializeComponent();

            startTime = DateTime.Now;

            taskModels = new List<SleepTasks>();

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Graduated Extinction: Step 1",
                PageDescription = "Child goes to their own bed and parent sits on the child’s bed, or on a chair next to the bed, and remains until the child falls asleep.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Graduated Extinction: Step 2",
                PageDescription = "Parent then moves away from the bed and closer to the bedroom door, until the child falls asleep.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Graduated Extinction: Step 3",
                PageDescription = "Parent sits in the doorway and remains there until the child falls asleep.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Graduated Extinction: Step 4",
                PageDescription = "Parent sits outside of the child’s bedroom out of view of the child.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Graduated Extinction: Step 5",
                PageDescription = "Parent starts to take breaks that increase in length until the child is consistently falling asleep without the parent present in the hallway.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Graduated Extinction: Step 6",
                PageDescription = "Be consistent in your approach and if your child leaves their bedroom through the night calmly and quietly  return the child every time, and simply state that it’s still night-time, go back to sleep.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Manage Sleep Environment",
                PageDescription = "Sleep environment should be the same in the morning as at bedtime, with no cues that it is daytime.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Address Sleep Motivation",
                PageDescription = "Decrease reinforcement for early awakening (child may play quietly in their room), but may not leave the bedroom.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Detecting Sleep Avoidance",
                PageDescription = "Use an inexpensive motion detector to signal when the child leaves the room. (Something like chimes/bells).",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Maintaining Expectations for Sleep Behavior",
                PageDescription = "If the child awakens early than scheduled, return the child to their bedroom and simply state “it's still night-time, go back to sleep.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Napping Behavior",
                PageDescription = "Decrease day time napping.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Waking Behavior",
                PageDescription = "Establish a consistent wake time, use audible and/or visual cues to signal wake time. When the alarm goes off, enter the child’s bedroom and open the curtains, saying “it’s time to wake up.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Modifying Sleep Conditions",
                PageDescription = "Gradually delay the stimulus (start time of day) by 15 minutes every 2-3 days.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Sleep Environment",
                PageDescription = "The bedroom environment should be dark, quiet, and cool to minimize distractions. Children with autism may be particularly sensitive to these sources of distraction.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Bedtime Routine",
                PageDescription = "Develop a consistent timeline for bedtime routines and stick to them.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Establish a Reasonable Bedtime",
                PageDescription = "Tell your child when it is and, under normal circumstances, put your child to bed at that time every night.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Quiet Time",
                PageDescription = "Institute a \"Quiet Time\" 20-30 minutes before bedtime. During this time, your child can engage in quiet activities such as reading while avoiding activities such as screen time.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Preparing for Bed",
                PageDescription = "A bedtime routine involves preparing for bed (such as getting into pajamas, brushing teeth, going to the bathroom, last drink of water) and activities to relax your children. The bedtime routine, once established, will become a powerful signal to your child that the time for sleep has come.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Sleep and Wake schedule",
                PageDescription = "The schedule should be regular with not much of a difference between the weekday and weekend schedule.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Morning Awakening Time",
                PageDescription = "Keep morning awakening time consistent and reduce or avoid daytime naps so the child does not 'make up' for lost sleep following a difficult night. This will increase the likelihood that they will fall asleep more quickly the next evening.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Avoid caffeine",
                PageDescription = "Avoid caffeine particularly close to bedtime, as it could make it more difficult for your child to fall asleep. Caffeine is often found in tea, chocolate and fizzy drinks.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Relaxation Technique 1",
                PageDescription = "Help your child relax before bed by reading a book, give a gentle back massage, or turn on soft music.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Relaxation Technique 2",
                PageDescription = "Show your child how to take a deep breath and hold it for 5 seconds, and then exhale slowly. Practice this with your child and when you think your child has got it right add the next step. This time ask your child to take a deep breath as practiced and when your child is holding their breath for the 5 seconds you say to your child \"relax and let your breath out slowly\".  Get your child to repeat this for 5 breaths. Supervise closely until you are sure your child can do it correctly.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Relaxation Technique 3",
                PageDescription = "Help your child to select a picture of a quiet, calm place and talk to your child about this place. Find out from your child why they think it is  a calm place. Make sure it is somewhere that your child has selected as calming for them. Use the words your child has provided to describe this place. Speak slowly and use a soft voice when you  describe the place and remember use your child’s words.  This will create a relaxing feeling. After you have practiced this with your child you can then write a short script to help you consistently describe this place. At this stage you can ask your child to close their eyes and you can slowly and softly describe the place for the child. It is possible with practice for your child to progress to doing this on their own.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Stimulus control and reinforcement",
                PageDescription = "The goal is to associate the child's bed with falling asleep quickly. It is important that your child does not use the bedroom for anything else except sleep. (no TV or computer games, etc). A sleep friendly environment with no stimulation is important. Move bedtime later than usual to ensure that the child is very tired on the first night. Every 2-3days the bedtime can then be moved 15 minutes earlier if the child fell asleep quickly (within 15-30 min) until an age appropriate bedtime has been achieved. Leave the bedroom when your child is still awake and provide verbal praise and positive touch if, and only if, the child is lying quietly in bed. Establish a consistent wake time, use audible and/or visual cues to signal wake time. When the alarm goes off, enter the child’s bedroom and open the curtains, saying “it’s time to wake up. No day time napping. Remember to reward your child the next morning for falling asleep quickly.",
            });

            foreach (SleepTasks item in taskModels)
            {
                cardCheckTemplate = new CardCheckTemplate(item.PageTitle, item.PageDescription);
                coSleepingStackContent.Children.Add(cardCheckTemplate);
            }
        }

        private async void coSleepingButtonBottom_Clicked(object sender, EventArgs e)
        {
            if ((sender as Button) != null) { (sender as Button).IsEnabled = false; }

            TimeSpan timeDifference = DateTime.Now.Subtract(startTime);

            CardCheckTemplate holder;

            bool isChecked;
            string currentTitle;
            int counter = 0;

            string returnString = "Data,Value" + Environment.NewLine;

            returnString = "Intervention,Cosleeping Intervention" + Environment.NewLine;

            foreach (var child in coSleepingStackContent.Children)
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
                Intervention = "Cosleeping Intervention"
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