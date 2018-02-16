//----------------------------------------------------------------------------------------------
// <copyright file="TaskCosleeping.xaml.cs" 
// Copyright February 2, 2018 Shawn Gilroy
//
// This file is part of AssistidCollector2
//
// AssistidCollector2 is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, version 3.
//
// AssistidCollector2 is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with AssistidCollector2.  If not, see http://www.gnu.org/licenses/. 
// </copyright>
//
// <summary>
// The AssistidCollector2 is a tool to assist clinicans and researchers in the treatment of communication disorders.
// 
// Email: shawn(dot)gilroy(at)temple.edu
//
// </summary>
//----------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using Xamarin.Forms;
using AssistidCollector1.Models;
using AssistidCollector1.Interfaces;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using AssistidCollector1.Views;
using Acr.UserDialogs;
using AssistidCollector1.Helpers;
using AssistidCollector1.Storage;
using Plugin.Connectivity;
using System.Threading;
using System.Text;
using System.Threading.Tasks;

namespace AssistidCollector1.Tasks
{
    public partial class TaskFeedback : ContentPage
    {
        DateTime startTime;
        StackLayout[] labelLayouts = new StackLayout[8];
        Slider[] sliderArray = new Slider[8];

        /// <summary>
        /// Initializes a new instance of the <see cref="T:AssistidCollector1.Tasks.TaskFeedback"/> class.
        /// </summary>
        public TaskFeedback()
        {
            InitializeComponent();

            Title = "Application Ratings";

            startTime = DateTime.Now;

            feedbackStackLayout.Padding = new Thickness(5, 5, 5, 5);

            feedbackStackLayout.Children.Add(new Label()
            {
                Text = "Please rate your experience with the app (1 or Red = No, 2 or Orange = Slightly, 3 or Yellow = Somewhat, 4 or Green = Fairly, 5 or Blue = Definitely):",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold | FontAttributes.Italic,
                Margin = new Thickness(20, 20, 0, 0)
            });

            feedbackStackLayout.Children.Add(new RatingStars("Did you like the App?"));
            feedbackStackLayout.Children.Add(new RatingStars("Was it easy to understand and follow?"));
            feedbackStackLayout.Children.Add(new RatingStars("Was it convenient to use?"));
            feedbackStackLayout.Children.Add(new RatingStars("Was it helpful; did it support you?"));

            feedbackStackLayout.Children.Add(new Label()
            {
                Text = "Please rate the sleep intervention (1 or Red = No, 2 or Orange = Slightly, 3 or Yellow = Somewhat, 4 or Green = Fairly, 5 or Blue = Definitely):",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                FontAttributes = FontAttributes.Bold | FontAttributes.Italic,
                Margin = new Thickness(20, 20, 0, 0)
            });

            feedbackStackLayout.Children.Add(new RatingStars("Did the sleep intervention suit the family routine?"));
            feedbackStackLayout.Children.Add(new RatingStars("Were the instructions easy to understand and follow?"));
            feedbackStackLayout.Children.Add(new RatingStars("Was the intervention appropriate for your child?"));
            feedbackStackLayout.Children.Add(new RatingStars("Was the intervention successful"));

            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, true);
            NavigationPage.SetBackButtonTitle(this, "Back");
        }

        /// <summary>
        /// Handles the clicked async.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            bool allCompleted = true;

            foreach (var view in feedbackStackLayout.Children)
            {
                var mView = view as RatingStars;

                if (mView != null)
                {
                    if (mView.SelectedRating == -1)
                    {
                        allCompleted = false;
                    }
                }
            }

            if (allCompleted)
            {
                string returnString = ViewTools.CommaSeparatedValue("Question,Rating",
                                                                    feedbackStackLayout, startTime, 
                                                                    DateTime.Now.Subtract(startTime));

                int result = await App.Database.SaveItemAsync(new SleepFeedbackModel()
                {
                    CSV = returnString
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
                        await DropboxServer.UploadFeedback(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(returnString)), App.Database.GetLargestFeedbackID());

                        await Task.Delay(100);
                    }
                }

                App.RefreshServer = true;

                await Navigation.PopModalAsync();

            }
            else
            {
                await UserDialogs.Instance.AlertAsync("Please answer all questions", Title = "Error", okText: "Okay");
            }
        }
    }
}
