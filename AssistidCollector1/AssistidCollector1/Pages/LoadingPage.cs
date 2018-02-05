using AssistidCollector1.Helpers;
using AssistidCollector1.Models;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace AssistidCollector1.Pages
{
    public class LoadingPage : ContentPage
    {
        public LoadingPage()
        {
            Content = new StackLayout
            {
                Children = {
                    new Label {
                        Text = "Loading current files..."
                    }
                }
            };

            LoadAssets();
        }

        public async void LoadAssets()
        {
            Debug.WriteLineIf(App.Debugging, "LoadAssets()");

            var mManifest = await App.Database.GetManifestAsync();

            Debug.WriteLineIf(App.Debugging, "get manifest");

            if (mManifest != null && mManifest.Count == 1)
            {
                Debug.WriteLineIf(App.Debugging, "Count: " + mManifest.Count);

                App.MainManifest = JsonConvert.DeserializeObject<Manifest>(mManifest.First().JSON);
            }
            else
            {
                App.MainManifest = null;
            }

            await DropboxServer.DownloadManifest(App.MainManifest);

            Debug.WriteLineIf(App.Debugging, "Updated!");

            App.Current.MainPage = new StartPage();
        }
    }
}