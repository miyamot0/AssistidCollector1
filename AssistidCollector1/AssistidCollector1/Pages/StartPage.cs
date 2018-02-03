using System.Diagnostics;
using System.IO;

using Xamarin.Forms;

namespace AssistidCollector1.Pages
{
    public class StartPage : ContentPage
    {
        private string LoadingScreenHtml = "";
        WebView MainWebView;

        public StartPage()
        {
            // Hack, always loading base
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

            Debug.WriteLine(LoadingScreenHtml);

            MainWebView = new WebView
            {
                Source = new HtmlWebViewSource
                {
                    Html = LoadingScreenHtml
                },
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            MainWebView.Navigating += MainWebView_Navigating;

            Content = MainWebView;
        }

        private void MainWebView_Navigating(object sender, WebNavigatingEventArgs e)
        {
            Debug.WriteLine("Originating Page: " + GetOriginatingPage(e.Url));

            Debug.WriteLine("Command: " + GetCommand(e.Url));

            e.Cancel = true;

            MainWebView.Source.SetValue(HtmlWebViewSource.HtmlProperty, "<html><body>asdf</body></html>");
        }

        private string GetOriginatingPage(string url)
        {
            return url.Split('?')[0].Replace("http://", "").Replace("/", "");
        }

        private string GetCommand(string url)
        {
            string[] commands = url.Split('?')[1].Split('&');
            string[] tempCommand;

            foreach (string command in commands)
            {
                tempCommand = command.Split('=');

                if (tempCommand[0].Contains("command"))
                {
                    return tempCommand[1];
                }
            }

            return null;
        }
    }
}