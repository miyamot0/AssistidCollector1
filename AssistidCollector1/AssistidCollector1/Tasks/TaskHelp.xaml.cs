using System;
using Xamarin.Forms;

namespace AssistidCollector1.Tasks
{
    public partial class TaskHelp : ContentPage
    {
        public TaskHelp()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Close help menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void helpButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}