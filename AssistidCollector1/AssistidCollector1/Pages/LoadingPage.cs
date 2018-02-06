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

            CancellationTokenSource cancelSrc = new CancellationTokenSource();
            ProgressDialogConfig config = new ProgressDialogConfig()
                .SetTitle("Contacting Server")
                .SetIsDeterministic(true)
                .SetMaskType(MaskType.Black)
                .SetCancel(onCancel: cancelSrc.Cancel);

            using (IProgressDialog progress = UserDialogs.Instance.Progress(config))
            {
                try
                {
                    var mManifest = await App.Database.GetManifestAsync();

                    progress.PercentComplete = (int)((1.0 / 5.0) * 100);

                    if (mManifest != null && mManifest.Count == 1)
                    {
                        App.MainManifest = JsonConvert.DeserializeObject<Manifest>(mManifest.First().JSON);
                    }
                    else
                    {
                        App.MainManifest = null;
                    }

                    progress.PercentComplete = (int)((2.0 / 5.0) * 100);

                    if (CrossConnectivity.Current.IsConnected)
                    {
                        await DropboxServer.CreateDropboxFolder();

                        progress.PercentComplete = (int)((3.0 / 5.0) * 100);

                        await DropboxServer.DownloadManifest(App.MainManifest);

                        progress.PercentComplete = (int)((4.0 / 5.0) * 100);
                    }

                    progress.PercentComplete = (int)((5.0 / 5.0) * 100);
                }
                catch (Exception e)
                {
                    Debug.WriteLineIf(App.Debugging, e.ToString());
                }

                progress.PercentComplete = (int)((5.0 / 5.0) * 100);

                App.Current.MainPage = new NavigationPage(new TaskPageStart());
            }
        }
    }
}