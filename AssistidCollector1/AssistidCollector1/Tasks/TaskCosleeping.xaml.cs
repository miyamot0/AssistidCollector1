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
    public partial class TaskCosleeping : ContentPage
    {
        List<SleepTasks> taskModels;
        CardCheckTemplate cardCheckTemplate;
        DateTime startTime;

        /// <summary>
        /// 
        /// </summary>
        public TaskCosleeping()
        {
            InitializeComponent();

            startTime = DateTime.Now;

            taskModels = new List<SleepTasks>();

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Graduated Extinction: Step 1",
                PageDescription = "Child goes to their own bed and parent sits on the child’s bed, or on a chair next to the bed, and remains until the child falls asleep.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Graduated Extinction: Step 2",
                PageDescription = "Parent then moves away from the bed and closer to the bedroom door, until the child falls asleep.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Graduated Extinction: Step 3",
                PageDescription = "Parent sits in the doorway and remains there until the child falls asleep.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Graduated Extinction: Step 4",
                PageDescription = "Parent sits outside of the child’s bedroom out of view of the child.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Graduated Extinction: Step 5",
                PageDescription = "Parent starts to take breaks that increase in length until the child is consistently falling asleep without the parent present in the hallway.",
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                Strategy = Identifiers.Strategies.Specific,
                PageTitle = "Graduated Extinction: Step 6",
                PageDescription = "Be consistent in your approach and if your child leaves their bedroom through the night calmly and quietly  return the child every time, and simply state that it’s still night-time, go back to sleep.",
            });

            ContentHelper.AddSleepHygieneContent(taskModels);

            ContentHelper.AddSleepRelaxationContent(taskModels);

            foreach (SleepTasks item in taskModels)
            {
                cardCheckTemplate = new CardCheckTemplate(item.PageTitle, item.PageDescription, item.Strategy);
                coSleepingStackContent.Children.Add(cardCheckTemplate);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void coSleepingButtonBottom_Clicked(object sender, EventArgs e)
        {
            if ((sender as Button) != null) { (sender as Button).IsEnabled = false; }

            string returnString = ViewTools.CommaSeparatedValue("Data,Value", "Intervention,Cosleeping Intervention",
                coSleepingStackContent, taskModels,
                startTime, DateTime.Now.Subtract(startTime));

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
    }
}