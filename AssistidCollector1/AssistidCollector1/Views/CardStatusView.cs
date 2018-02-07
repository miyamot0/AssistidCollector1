using AssistidCollector1.Enums;
using Xamarin.Forms;

namespace AssistidCollector1.Views
{
    /// <summary>
    /// Card status view
    /// </summary>
    public class CardStatusView : ContentView
    {
        public CardStatusView(Identifiers.Strategies strategyType)
        {
            Content = new BoxView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = GetStatusColor(strategyType)
            };
        }

        /// <summary>
        /// Get color from identifier
        /// </summary>
        /// <param name="strategyType"></param>
        /// <returns></returns>
        public static Color GetStatusColor(Identifiers.Strategies strategyType)
        {
            Color mColor;

            switch (strategyType)
            {
                case Identifiers.Strategies.Specific:
                    mColor = Color.FromHex("C5C5C5");
                    break;

                case Identifiers.Strategies.Relaxation:
                    mColor = Color.FromHex("00A2D3");
                    break;

                case Identifiers.Strategies.SleepHygiene:
                    mColor = Color.FromHex("E74C3C");
                    break;

                default:
                    mColor = Color.FromHex("E3E3E3");
                    break;
            }

            return mColor;
        }
    }
}
