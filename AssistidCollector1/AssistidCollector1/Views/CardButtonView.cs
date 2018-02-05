using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AssistidCollector1.Views
{
    public class CardButtonView : ContentView
    {
        public CardButtonView(string buttonText)
        {
            BackgroundColor = Color.FromHex("F6F6F6");

            var label = new Label()
            {
                Text = buttonText,
                FontSize = 9,
                FontAttributes = FontAttributes.Bold,
                TextColor = Color.FromHex("383838")
            };

            var stack = new StackLayout()
            {
                Padding = new Thickness(5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    //new Image () {
                    //    Source = source,
                    //    HeightRequest = 10,
                    //    WidthRequest = 10
                    //},
                    label
                }
            };

            Content = stack;
        }
    }
}