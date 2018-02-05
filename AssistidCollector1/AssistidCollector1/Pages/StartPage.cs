using AssistidCollector1.Constants;
using AssistidCollector1.Helpers;
using AssistidCollector1.Interfaces;
using AssistidCollector1.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AssistidCollector1.Pages
{
    public class StartPage : ContentPage
    {
        private string LoadingScreenHtml = "";
        WebView MainWebView;

        public StartPage()
        {
            Debug.WriteLineIf(App.Debugging, Settings.AppName + " >>> Starting Viewer Page ...");

            // Hack, always loading base currently
            using (Stream stream = App.MainAssembly.GetManifestResourceStream(App.StandardStartPage))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    LoadingScreenHtml = reader.ReadToEnd();
                }
            }


            /*
            if (!DependencyService.Get<InterfaceSaveLoad>().FileExists("StartPage.html"))
            {
                using (Stream stream = App.MainAssembly.GetManifestResourceStream(App.StandardStartPage))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        DependencyService.Get<InterfaceSaveLoad>().SaveFile("StartPage.html", reader.ReadToEnd());
                        
                        LoadingScreenHtml = reader.ReadToEnd();
                    }
                }
            }
            else
            {
                LoadingScreenHtml = DependencyService.Get<InterfaceSaveLoad>().LoadFile("StartPage.html");
            }
            */

            LoadingScreenHtml = DependencyService.Get<InterfaceSaveLoad>().LoadFile("startpage.html");

            //Debug.WriteLine(LoadingScreenHtml);
            Debug.WriteLine("Base: " + DependencyService.Get<InterfaceContentLocation>().GetPersonalLocation());

            MainWebView = new WebView
            {
                Source = new HtmlWebViewSource
                {
                    Html = LoadingScreenHtml,
                    BaseUrl = DependencyService.Get<InterfaceContentLocation>().GetBaseLocation(),                    
                },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            MainWebView.Navigating += MainWebView_Navigating;

            Content = MainWebView;
        }

        /// <summary>
        /// Navigating event, hacking way to manage web-based displays
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            Debug.WriteLineIf(App.Debugging, "Originating Page: " + GetOriginatingPage(e.Url));

            Debug.WriteLineIf(App.Debugging, "Command: " + GetCommand(e.Url));

            e.Cancel = true;

            switch (GetCommand(e.Url).Trim())
            {
                case NavigationConstants.MoveCommand:
                    MainWebView.Source.SetValue(HtmlWebViewSource.HtmlProperty, LoadHtmlString(GetMove(e.Url)));

                    break;

                default:
                    MainWebView.Source.SetValue(HtmlWebViewSource.HtmlProperty, LoadHtmlString("error.html"));

                    break;
            }

        }

        private string LoadHtmlString(string htmlFileName)
        {
            if (DependencyService.Get<InterfaceSaveLoad>().FileExists(htmlFileName))
            {
                return DependencyService.Get<InterfaceSaveLoad>().LoadFile(htmlFileName);
            }
            else
            {
                return "<html><body>Error</body></html>";
            }
        }

        /// <summary>
        /// Pull original location from params
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetOriginatingPage(string url)
        {
            return url.Split('?')[0].Replace("http://", "").Replace("/", "");
        }

        /// <summary>
        /// Pull command from params
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetCommand(string url)
        {
            string[] commands = url.Split('?')[1].Split('&');
            string[] tempCommand;

            foreach (string command in commands)
            {
                tempCommand = command.Split('=');

                if (tempCommand[0].Contains(NavigationConstants.CommandString))
                {
                    return tempCommand[1];
                }
            }

            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private string GetMove(string url)
        {
            string[] commands = url.Split('?')[1].Split('&');
            string[] tempCommand;

            foreach (string command in commands)
            {
                tempCommand = command.Split('=');

                if (tempCommand[0].Contains(NavigationConstants.MoveCommand))
                {
                    return tempCommand[1];
                }
            }

            return "";
        }
    }
}