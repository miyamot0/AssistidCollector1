
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

            ContentHelper.AddSleepHygieneContent(taskModels);

            ContentHelper.AddSleepRelaxationContent(taskModels);

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