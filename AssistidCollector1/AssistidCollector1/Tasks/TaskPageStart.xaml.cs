using AssistidCollector1.Enums;
using AssistidCollector1.Models;
using AssistidCollector1.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace AssistidCollector1.Tasks
{
    public partial class TaskPageStart : ContentPage
    {
        List<SleepTasks> taskModels;
        TapGestureRecognizer tapGestureRecognizer;
        CardViewTemplate cardViewTemplate;

        /// <summary>
        /// Starting page for app
        /// </summary>
        public TaskPageStart()
        {
            InitializeComponent();

            taskModels = new List<SleepTasks>();

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.NightAwakenings,
                PageTitle = "Night-time Awakenings",
                PageDescription = "For instructions on how to work on your child waking at night, you can follow the instructions provided here",
                PageButton = "Select this option for more information on Night-time awakenings",
                PageImage = "NightAwakeningsCropped.jpg"
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                PageTitle = "Bedtime Resistance",
                PageDescription = "This section provides information on how to address resistance to bedtime routines",
                PageButton = "Select this option for ways to address Bedtime Resistance",
                PageImage = "BedtimeResistanceCropped.jpg"
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Co-sleeping",
                PageDescription = "This area focuses on strategies for managing co-sleeping behavior.",
                PageButton = "Select this option for co-sleeping behavior strategies",
                PageImage = "CoSleepingCropped.jpg"
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                PageTitle = "Late Onset of Sleep",
                PageDescription = "This section focuses on how to address times when children do not get tired until very late.",
                PageButton = "Select this option for late-onset sleep strategies",
                PageImage = "LateOnsetCropped.jpg"
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Early morning awakenings",
                PageDescription = "Information in this area discusses early morning sleep awakenings.",
                PageButton = "Select this option for tips on addressing early morning awakenings.",
                PageImage = "EarlyMorningCropped.jpg"
            });

            tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OnTapGestureRecognizerTapped;

            foreach (SleepTasks item in taskModels)
            {
                cardViewTemplate = new CardViewTemplate(item);
                cardViewTemplate.GestureRecognizers.Add(tapGestureRecognizer);

                startPageStackContent.Children.Add(cardViewTemplate);
            }
        }

        /// <summary>
        /// Gesture recognizer, link to individual pages
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            var getCardTapped = sender as CardViewTemplate;

            if (getCardTapped != null)
            {
                switch (getCardTapped.PageId)
                {
                    case Identifiers.Pages.Start:
                        Debug.WriteLineIf(App.Debugging, "TODO: Go to home");

                        break;

                    case Identifiers.Pages.NightAwakenings:
                        Debug.WriteLineIf(App.Debugging, "TODO: Go to night awakenings");

                        break;

                    case Identifiers.Pages.BedtimeResistance:
                        Debug.WriteLineIf(App.Debugging, "TODO: Go to BedtimeResistance");

                        break;

                    case Identifiers.Pages.CoSleeping:
                        Debug.WriteLineIf(App.Debugging, "TODO: Go to CoSleeping");

                        break;

                    case Identifiers.Pages.EarlyMorningAwakenings:
                        Debug.WriteLineIf(App.Debugging, "TODO: Go to EarlyMorningAwakenings");

                        break;

                    case Identifiers.Pages.LateOnset:
                        await Navigation.PushModalAsync(new TaskSleepOnset());
                        //Debug.WriteLineIf(App.Debugging, "TODO: Go to LateOnset");

                        break;
                }
            }
        }

        /// <summary>
        /// Help menu button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void startPageButtonBottom_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TaskHelp());
        }
    }
}