
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
                PageTitle = "Independent Sleeping Arrangements",
                PageDescription = "Co-sleeping (bed-sharing) should not be allowed because this may serve to reinforce nighttime awakenings.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                PageTitle = "Developing Structured Routines",
                PageDescription = "The bedtime routine should be structured, consistent, and delivered in a calm and positive manner. ",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                PageTitle = "Reinforce Appropriate Sleep-related Behavior",
                PageDescription = "Leave the bedroom when your child is still awake, as you do so provide verbal praise and positive touch if, and only if, the child is lying quietly in bed.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                PageTitle = "Reduce Nighttime Interaction",
                PageDescription = "The duration for the bedtime interactions for sleep compatible behaviors should be gradually reduced over time as your child learns to fall asleep independently. ",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                PageTitle = "Nighttime Behavior Management",
                PageDescription = "Managing middle-of-the-night awakenings:  Parents can handle this by either letting the child \"cry it out\" or by using the progressive delayed responding approach until the child learns to fall asleep independently.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                PageTitle = "Decrease Reliance on Adult Reassurance",
                PageDescription = "For progressive delayed responding parents should engage in behaviors which will promote independent sleep onset.  If your child wakes at night and cries out, then parents should initially enter the room briefly and re-assure their child that everything is okay and quickly exit the room to minimize attention and interpersonal interaction during nighttime hours. This re-assuring interaction should be gradually reduced in frequency and duration over consecutive nights. ",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                PageTitle = "Employ Relaxation Exercises",
                PageDescription = " Develop the child’s self-calming skills (go to the relaxation screen for examples).",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                PageTitle = "Avoid Corrective Measures",
                PageDescription = "Do not use early bedtime as a consequence for misbehavior.",
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