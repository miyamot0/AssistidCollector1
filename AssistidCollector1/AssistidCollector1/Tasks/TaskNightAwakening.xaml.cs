
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
    public partial class TaskNightAwakening : ContentPage
    {
        List<SleepTasks> taskModels;
        CardCheckTemplate cardCheckTemplate;
        DateTime startTime;

        public TaskNightAwakening()
        {
            InitializeComponent();

            startTime = DateTime.Now;

            taskModels = new List<SleepTasks>();

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Independent Sleeping Arrangements",
                PageDescription = "Co-sleeping (bed-sharing) should not be allowed because this may serve to reinforce nighttime awakenings.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Developing Structured Routines",
                PageDescription = "The bedtime routine should be structured, consistent, and delivered in a calm and positive manner. ",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Reinforce Appropriate Sleep-related Behavior",
                PageDescription = "Leave the bedroom when your child is still awake, as you do so provide verbal praise and positive touch if, and only if, the child is lying quietly in bed.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Reduce Nighttime Interaction",
                PageDescription = "The duration for the bedtime interactions for sleep compatible behaviors should be gradually reduced over time as your child learns to fall asleep independently. ",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Nighttime Behavior Management",
                PageDescription = "Managing middle-of-the-night awakenings:  Parents can handle this by either letting the child \"cry it out\" or by using the progressive delayed responding approach until the child learns to fall asleep independently.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Decrease Reliance on Adult Reassurance",
                PageDescription = "For progressive delayed responding parents should engage in behaviors which will promote independent sleep onset.  If your child wakes at night and cries out, then parents should initially enter the room briefly and re-assure their child that everything is okay and quickly exit the room to minimize attention and interpersonal interaction during nighttime hours. This re-assuring interaction should be gradually reduced in frequency and duration over consecutive nights. ",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Employ Relaxation Exercises",
                PageDescription = " Develop the child’s self-calming skills (go to the relaxation screen for examples).",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Avoid Corrective Measures",
                PageDescription = "Do not use early bedtime as a consequence for misbehavior.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Sleep Environment",
                PageDescription = "The bedroom environment should be dark, quiet, and cool to minimize distractions. Children with autism may be particularly sensitive to these sources of distraction.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Bedtime Routine",
                PageDescription = "Develop a consistent timeline for bedtime routines and stick to them.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Establish a Reasonable Bedtime",
                PageDescription = "Tell your child when it is and, under normal circumstances, put your child to bed at that time every night.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Quiet Time",
                PageDescription = "Institute a \"Quiet Time\" 20-30 minutes before bedtime. During this time, your child can engage in quiet activities such as reading while avoiding activities such as screen time.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Preparing for Bed",
                PageDescription = "A bedtime routine involves preparing for bed (such as getting into pajamas, brushing teeth, going to the bathroom, last drink of water) and activities to relax your children. The bedtime routine, once established, will become a powerful signal to your child that the time for sleep has come.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Sleep and Wake schedule",
                PageDescription = "The schedule should be regular with not much of a difference between the weekday and weekend schedule.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Morning Awakening Time",
                PageDescription = "Keep morning awakening time consistent and reduce or avoid daytime naps so the child does not 'make up' for lost sleep following a difficult night. This will increase the likelihood that they will fall asleep more quickly the next evening.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                Strategy = Identifiers.Strategies.SleepHygiene,
                PageTitle = "Avoid caffeine",
                PageDescription = "Avoid caffeine particularly close to bedtime, as it could make it more difficult for your child to fall asleep. Caffeine is often found in tea, chocolate and fizzy drinks.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                Strategy = Identifiers.Strategies.Relaxation,
                PageTitle = "Relaxation Technique 1",
                PageDescription = "Help your child relax before bed by reading a book, give a gentle back massage, or turn on soft music.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                Strategy = Identifiers.Strategies.Relaxation,
                PageTitle = "Relaxation Technique 2",
                PageDescription = "Show your child how to take a deep breath and hold it for 5 seconds, and then exhale slowly. Practice this with your child and when you think your child has got it right add the next step. This time ask your child to take a deep breath as practiced and when your child is holding their breath for the 5 seconds you say to your child \"relax and let your breath out slowly\".  Get your child to repeat this for 5 breaths. Supervise closely until you are sure your child can do it correctly.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                Strategy = Identifiers.Strategies.Relaxation,
                PageTitle = "Relaxation Technique 3",
                PageDescription = "Help your child to select a picture of a quiet, calm place and talk to your child about this place. Find out from your child why they think it is  a calm place. Make sure it is somewhere that your child has selected as calming for them. Use the words your child has provided to describe this place. Speak slowly and use a soft voice when you  describe the place and remember use your child’s words.  This will create a relaxing feeling. After you have practiced this with your child you can then write a short script to help you consistently describe this place. At this stage you can ask your child to close their eyes and you can slowly and softly describe the place for the child. It is possible with practice for your child to progress to doing this on their own.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                Strategy = Identifiers.Strategies.Relaxation,
                PageTitle = "Stimulus control and reinforcement",
                PageDescription = "The goal is to associate the child's bed with falling asleep quickly. It is important that your child does not use the bedroom for anything else except sleep. (no TV or computer games, etc). A sleep friendly environment with no stimulation is important. Move bedtime later than usual to ensure that the child is very tired on the first night. Every 2-3days the bedtime can then be moved 15 minutes earlier if the child fell asleep quickly (within 15-30 min) until an age appropriate bedtime has been achieved. Leave the bedroom when your child is still awake and provide verbal praise and positive touch if, and only if, the child is lying quietly in bed. Establish a consistent wake time, use audible and/or visual cues to signal wake time. When the alarm goes off, enter the child’s bedroom and open the curtains, saying “it’s time to wake up. No day time napping. Remember to reward your child the next morning for falling asleep quickly.",
            });

            foreach (SleepTasks item in taskModels)
            {
                cardCheckTemplate = new CardCheckTemplate(item.PageTitle, item.PageDescription, item.Strategy);
                nightAwakeningStackContent.Children.Add(cardCheckTemplate);
            }
        }

        private async void nightAwakeningButtonBottom_Clicked(object sender, System.EventArgs e)
        {
            if ((sender as Button) != null) { (sender as Button).IsEnabled = false; }

            string returnString = ViewTools.CommaSeparatedValue("Data,Value", "Intervention,Night Awakening",
                nightAwakeningStackContent, taskModels,
                startTime, DateTime.Now.Subtract(startTime));

            int result = await App.Database.SaveItemAsync(new StorageModel()
            {
                CSV = returnString,
                Intervention = "Night Awakening"
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