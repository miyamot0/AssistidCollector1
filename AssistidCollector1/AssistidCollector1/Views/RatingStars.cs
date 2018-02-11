using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AssistidCollector1.Helpers;
using AssistidCollector1.Models;
using Xamarin.Forms;

namespace AssistidCollector1.Views
{
    public class RatingStars : ContentView
    {
        Color EmptyColor = Color.FromRgb(51, 51, 51);

        List<Frame> RateSelections = new List<Frame>();

        Dictionary<int, RatingClass> RatingOptions = new Dictionary<int, RatingClass>();

        public int SelectedRating = -1;

        Grid MainLayout;

        public string Question = "";

        public RatingStars(string mQuestion)
        {
            Question = mQuestion;

            HorizontalOptions = LayoutOptions.FillAndExpand;

            RatingOptions.Add(1, new RatingClass() { Color = Color.FromHex("bd2c33"), Text = "1" });
            RatingOptions.Add(2, new RatingClass() { Color = Color.FromHex("e49420"), Text = "2" });
            RatingOptions.Add(3, new RatingClass() { Color = Color.FromHex("ecdb00"), Text = "3" });
            RatingOptions.Add(4, new RatingClass() { Color = Color.FromHex("3bad54"), Text = "4" });
            RatingOptions.Add(5, new RatingClass() { Color = Color.FromHex("1b7db9"), Text = "5" });

            Grid rateGrid = DrawRateGrid();

            MainLayout = new Grid() {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition(),
                    new RowDefinition(),
                }
            };

            Label mTitle = new Label()
            {
                Text = Question,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Center,
                Margin = new Thickness(0, 0, 0, 5)
            };

            MainLayout.Children.Add(mTitle, 0, 0);
            MainLayout.Children.Add(rateGrid, 0, 1);

            Content = MainLayout;

            Margin = new Thickness(5, 0, 5, 10);
        }

        /// <summary>
        /// Draws the rate grid.
        /// </summary>
        /// <returns>The rate grid.</returns>
        private Grid DrawRateGrid()
        {
            Grid grid = new Grid();
            grid.HeightRequest = PlatformRateSize();
            grid.HorizontalOptions = LayoutOptions.CenterAndExpand;
            grid.VerticalOptions = LayoutOptions.End;
            grid.ColumnSpacing = 20;

            foreach (var rating in RatingOptions)
            {
                Frame rateSelection = DrawRateSelection(grid, rating.Key);
                TapGestureRecognizer rateTapGestureRecognizer = CreateRateTapGestureRecognizer();
                rateSelection.GestureRecognizers.Add(rateTapGestureRecognizer);

                RateSelections.Add(rateSelection);
                grid.Children.Add(rateSelection, rating.Key - 1, 0); // Count backwards for stacking from the bottom
            }

            return grid;
        }

        /// <summary>
        /// Draws the rate selection.
        /// </summary>
        /// <returns>The rate selection.</returns>
        /// <param name="grid">Grid.</param>
        /// <param name="ratingId">Rating identifier.</param>
        private Frame DrawRateSelection(Grid grid, int ratingId)
        {
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(PlatformRateSize()) });

            Frame frame = new Frame
            {
                HeightRequest = PlatformRateSize(),
                WidthRequest  = PlatformRateSize(),
                CornerRadius  = PlatformRateSize() / 2,
                BackgroundColor = EmptyColor,
                AutomationId = $"{ratingId}"
            };

            return frame;
        }

        /// <summary>
        /// Creates the rate tap gesture recognizer.
        /// </summary>
        /// <returns>The rate tap gesture recognizer.</returns>
        private TapGestureRecognizer CreateRateTapGestureRecognizer()
        {
            TapGestureRecognizer tgr = new TapGestureRecognizer();
            tgr.Tapped += async (s, e) =>
            {
                Frame tappedFrame = s as Frame;

                SelectedRating = int.Parse(tappedFrame.AutomationId); 

                await ColorFramesAsync(RatingOptions[SelectedRating].Color);
            };

            return tgr;
        }

        /// <summary>
        /// Colors the frames async.
        /// </summary>
        /// <returns>The frames async.</returns>
        /// <param name="color">Color.</param>
        private async Task ColorFramesAsync(Color color)
        {
            var colorAnimations = new List<Task<bool>>();

            for (int i = 0; i < RateSelections.Count; i++)
            {
                if (i < SelectedRating)
                {
                    colorAnimations.Add(RateSelections[i].ColorTo(color));
                }
                else
                {
                    colorAnimations.Add(RateSelections[i].ColorTo(EmptyColor));
                }
            }

            await Task.WhenAll(colorAnimations);
        }

        /// <summary>
        /// Platforms the size of the rate.
        /// </summary>
        /// <returns>The rate size.</returns>
        private int PlatformRateSize()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return 80;

                case Device.Android:
                    return 60;

                default:
                    throw new PlatformNotSupportedException();
            }
        }

        /// <summary>
        /// Platforms the size.
        /// </summary>
        /// <returns>The size.</returns>
        private int PlatformSize()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return 60;

                case Device.Android:
                    return 80;

                default:
                    throw new PlatformNotSupportedException();
            }
        }
    }
}

