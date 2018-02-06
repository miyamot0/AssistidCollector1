using Acr.UserDialogs;
using AssistidCollector1.Helpers;
using AssistidCollector1.Models;
using AssistidCollector1.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AssistidCollector1.Pages
{
    public class LoadingPage : ContentPage
    {
        /// <summary>
        /// Loading page for manifest
        /// </summary>
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

            CheckCredentials();            
        }

        /// <summary>
        /// CheckCredentials()
        /// </summary>
        public async void CheckCredentials()
        {
            if (App.AccessToken == null || App.AccessToken == "")
            {
                var userInput = await UserDialogs.Instance.PromptAsync("Please input API token", null, "OK", "Cancel", "Api Token");

                Debug.WriteLineIf(App.Debugging, userInput.Text);

                App.AccessToken = userInput.Text;

                App.ReloadDropbox();
            }
            else
            {
                App.ReloadDropbox();
            }

            Debug.WriteLineIf(App.Debugging, "CheckCredentials() <<< Auth " + App.AccessToken);

            await Task.Delay(50);

            LoadAssets();
        }
        
        /// <summary>
        /// Load Stuff
        /// </summary>
        public async void LoadAssets()
        {
            Debug.WriteLineIf(App.Debugging, "LoadAssets()");

            var cancelSrc = new CancellationTokenSource();
            var config = new ProgressDialogConfig()
                .SetTitle("Downloading Manifest")
                .SetIsDeterministic(false)
                .SetMaskType(MaskType.Black)
                .SetCancel(onCancel: cancelSrc.Cancel);

            using (var progress = UserDialogs.Instance.Progress(config))
            {
                try
                {
                    var mManifest = await App.Database.GetManifestAsync();

                    if (mManifest != null && mManifest.Count == 1)
                    {
                        App.MainManifest = JsonConvert.DeserializeObject<Manifest>(mManifest.First().JSON);
                    }
                    else
                    {
                        App.MainManifest = null;
                    }

                    if (CrossConnectivity.Current.IsConnected)
                    {
                        await DropboxServer.CreateDropboxFolder();

                        await DropboxServer.DownloadManifest(App.MainManifest);
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLineIf(App.Debugging, e.ToString());
                }

                App.Current.MainPage = new TaskPageStart();
            }
        }
    }
}