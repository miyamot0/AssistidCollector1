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
                PageDescription = "For instructions on how to work on your child waking at night, you can follow the instructions provided here.",
                PageButton = "Select this option for more information",
                PageImage = "NightAwakeningsCropped.jpg"
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.BedtimeResistance,
                PageTitle = "Bedtime Resistance",
                PageDescription = "...",
                PageButton = "Select this option for more information",
                PageImage = "BedtimeResistanceCropped.jpg"
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.CoSleeping,
                PageTitle = "Co-sleeping",
                PageDescription = "...",
                PageButton = "Select this option for more information",
                PageImage = "CoSleepingCropped.jpg"
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.LateOnset,
                PageTitle = "Late Onset of Sleep",
                PageDescription = "...",
                PageButton = "Select this option for more information",
                PageImage = "LateOnsetCropped.jpg"
            });

            taskModels.Add(new SleepTasks()
            {
                PageId = Identifiers.Pages.EarlyMorningAwakenings,
                PageTitle = "Early morning awakenings",
                PageDescription = "...",
                PageButton = "Select this option for more information",
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
        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
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
                }                
            }
        }
    }
}