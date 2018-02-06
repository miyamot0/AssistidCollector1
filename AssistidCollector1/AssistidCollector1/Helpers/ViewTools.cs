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
    }
}
