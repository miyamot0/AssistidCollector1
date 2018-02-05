using Xamarin.Forms;

namespace AssistidCollector1.Views
{
    public class CardStatusView : ContentView
    {
        public CardStatusView()
        {
            var statusBoxView = new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill
            };

            statusBoxView.BackgroundColor = Color.FromHex("C5C5C5");

            Content = statusBoxView;
        }
    }
}
