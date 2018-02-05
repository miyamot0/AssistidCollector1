using Xamarin.Forms;

namespace AssistidCollector1.Views
{
    /// <summary>
    /// Card details view
    /// </summary>
    public class CardDetailsView : ContentView
    {
        public CardDetailsView(string Text, string Description)
        {
            BackgroundColor = Color.White;

            Content = new StackLayout()
            {
                Spacing = 0,
                Padding = new Thickness(10, 0, 0, 0),
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = {
                    new Label()
                    {
                        FormattedText = Text,
                        FontSize = 18,
                        TextColor = Color.FromHex("383838")
                    },
                    new Label()
                    {
                        FormattedText = Description,
                        FontSize = 12,
                        TextColor = Color.FromHex("383838")
                    },
                }
            };
        }
    }
}