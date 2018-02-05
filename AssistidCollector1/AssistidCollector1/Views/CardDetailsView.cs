using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AssistidCollector1.Views
{
    public class CardDetailsView : ContentView
    {
        public CardDetailsView(string Text, string Description)
        {
            BackgroundColor = Color.White;

            Label TitleText = new Label()
            {
                FormattedText = Text,
                FontSize = 18,
                TextColor = Color.FromHex("383838")
        };

            Label DescriptionText = new Label()
            {
                FormattedText = Description,
                FontSize = 12,
                TextColor = Color.FromHex("383838")
        };

            var stack = new StackLayout()
            {
                Spacing = 0,
                Padding = new Thickness(10, 0, 0, 0),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = {
                    TitleText,
                    DescriptionText,
                    //new DateTimeView (card)
                }
            };

            Content = stack;
        }
    }
}