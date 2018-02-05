using Xamarin.Forms;

namespace AssistidCollector1.Views
{
    /// <summary>
    /// Card button view
    /// </summary>
    public class CardButtonView : ContentView
    {
        public CardButtonView(string buttonText)
        {
            BackgroundColor = Color.FromHex("F6F6F6");

            Content = new StackLayout()
            {
                Padding = new Thickness(5),
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Children = {
                    new Label()
                    {
                        Text = buttonText,
                        FontSize = 9,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Color.FromHex("383838")
                    }
                }
            };
        }
    }
}