using AssistidCollector1.Enums;
using Xamarin.Forms;

namespace AssistidCollector1.Views
{
    public class CardCheckTemplate : ContentView
    {
        public Identifiers.Pages PageId = Identifiers.Pages.LateOnset;
        public Grid grid;

        public CardCheckTemplate(string title, string instructions, Identifiers.Strategies strategyType)
        {
            grid = new Grid
            {
                Padding = new Thickness(0, 1, 1, 1),
                RowSpacing = 1,
                ColumnSpacing = 1,
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (70, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength (30, GridUnitType.Absolute) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (4, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (70, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength (100, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (100, GridUnitType.Absolute) }
                }
            };

            grid.Children.Add(new CardStatusView(strategyType), 0, 1, 0, 2);

            grid.Children.Add(new Switch
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                BackgroundColor = Color.White
            }, 4, 5, 0, 1);

            grid.Children.Add(new CardDetailsView(title, instructions), 1, 4, 0, 1);

            grid.Children.Add(new CardButtonView("Please check the box if completed."), 1, 5, 1, 2);

            Content = grid;
        }
    }
}