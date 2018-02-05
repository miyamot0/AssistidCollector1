using Xamarin.Forms;

namespace AssistidCollector1.Views
{
    /// <summary>
    /// Card status view
    /// </summary>
    public class CardStatusView : ContentView
    {
        public CardStatusView()
        {
            Content = new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.FromHex("C5C5C5")
            };
        }
    }
}
