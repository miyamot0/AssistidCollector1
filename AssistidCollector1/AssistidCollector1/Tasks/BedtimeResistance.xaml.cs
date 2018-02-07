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

            ContentHelper.AddSleepHygieneContent(taskModels);

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