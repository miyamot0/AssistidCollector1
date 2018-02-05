using AssistidCollector1.Enums;
using AssistidCollector1.Models;
using Xamarin.Forms;

namespace AssistidCollector1.Views
{
    /// <summary>
    /// Card view template
    /// </summary>
    public class CardViewTemplate : ContentView
    {
        public Identifiers.Pages PageId;

        public CardViewTemplate(SleepTasks sleepTask)
        {
            PageId = sleepTask.PageId;

            Grid grid = new Grid
            {
                Padding = new Thickness(0, 1, 1, 1),
                RowSpacing = 1,
                ColumnSpacing = 1,
                BackgroundColor = Color.FromHex("E3E3E3"),
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowDefinitions = {
                    new RowDefinition { Height = new GridLength (70, GridUnitType.Absolute) },
                    new RowDefinition { Height = new GridLength (30, GridUnitType.Absolute) }
                },
                ColumnDefinitions = {
                    new ColumnDefinition { Width = new GridLength (4, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (70, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength (100, GridUnitType.Absolute) },
                    new ColumnDefinition { Width = new GridLength (50, GridUnitType.Absolute) }
                }
            };

            grid.Children.Add(new CardStatusView(), 0, 1, 0, 2);            
            grid.Children.Add(new Image()
            {
                Source = sleepTask.PageImage,
                HeightRequest = 70,
                WidthRequest = 70,
                Aspect = Aspect.AspectFill
            }, 1, 2, 0, 1);
            grid.Children.Add(new CardDetailsView(sleepTask.PageTitle, sleepTask.PageDescription), 2, 5, 0, 1);
            grid.Children.Add(new CardButtonView(sleepTask.PageButton), 1, 5, 1, 2);

            Content = grid;
        }
    }
}