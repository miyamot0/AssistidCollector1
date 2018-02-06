using AssistidCollector1.Models;
using AssistidCollector1.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace AssistidCollector1.Helpers
{
    public static class ViewTools
    {
        /// <summary>
        /// Get Value of Switch
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static bool GetSwitchValue(Grid grid)
        {
            Switch temp;

            foreach (var child in grid.Children)
            {
                temp = child as Switch;

                if (temp != null)
                {
                    return temp.IsToggled;
                }
            }

            return false;
        }

        public static string CommaSeparatedValue(string header, string intervention, StackLayout layout, List<SleepTasks> taskModels,
            DateTime startTime, TimeSpan timeDifference)
        {
            string returnString = header + Environment.NewLine;
            returnString = intervention + Environment.NewLine;

            CardCheckTemplate holder;

            bool isChecked;
            string currentTitle;
            int counter = 0;

            foreach (var child in layout.Children)
            {
                holder = child as CardCheckTemplate;

                if (holder != null)
                {
                    isChecked = ViewTools.GetSwitchValue(holder.grid);
                    currentTitle = taskModels[counter].PageTitle;

                    returnString += currentTitle + ",";
                    returnString += (isChecked) ? "True" : "False";
                    returnString += Environment.NewLine;

                    counter++;
                }
            }

            returnString += "Date," + startTime.Date.ToString() + Environment.NewLine;
            returnString += "Start," + startTime.TimeOfDay.ToString() + Environment.NewLine;
            returnString += "Seconds," + timeDifference.TotalSeconds.ToString() + Environment.NewLine;

            return returnString;
        }
    }
}
