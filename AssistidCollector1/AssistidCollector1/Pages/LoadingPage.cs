using Acr.UserDialogs;
using AssistidCollector1.Helpers;
using AssistidCollector1.Models;
using AssistidCollector1.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
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
                    new Image()
                    {
                        Source = "splash.png",
                        Aspect = Aspect.AspectFill
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
            double steps = 7.0;
            double count = 0.0;
            
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

                    var currentItems = await App.Database.GetDataAsync();

                    if (currentItems != null)
                    {
                        steps = steps + currentItems.Count;
                    }

                    count += 1.0;

                    progress.PercentComplete = (int)((count / steps) * 100);

                    if (mManifest != null && mManifest.Count == 1)
                    {
                        App.MainManifest = JsonConvert.DeserializeObject<Manifest>(mManifest.First().JSON);
                    }
                    else
                    {
                        App.MainManifest = null;
                    }

                    count += 1.0;

                    progress.PercentComplete = (int)((count / steps) * 100);

                    if (CrossConnectivity.Current.IsConnected)
                    {
                        await Task.Delay(App.DropboxDeltaTimeout);

                        bool createdFolder = await DropboxServer.CreateDropboxFolder();

                        await Task.Delay(App.DropboxDeltaTimeout);

                        count += 1.0;

                        progress.PercentComplete = (int)((count / steps) * 100);

                        int filesUploaded = await DropboxServer.CountIndividualFiles();

                        count += 1.0;

                        progress.PercentComplete = (int)((count / steps) * 100);

                        if (currentItems != null && currentItems.Count != filesUploaded)
                        {
                            foreach (Storage.StorageModel currentDataPoint in currentItems)
                            {
                                DropboxServer.UploadFile(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(currentDataPoint.CSV)), currentDataPoint.ID);

                                await Task.Delay(App.DropboxDeltaTimeout);

                                count += 1.0;

                                progress.PercentComplete = (int)((count / steps) * 100);
                            }
                        }

                        count += 1.0;

                        progress.PercentComplete = (int)((count / steps) * 100);

                        await DropboxServer.DownloadManifest(App.MainManifest);
                    }

                    count = steps;

                    progress.PercentComplete = 100;

                    await Task.Delay(200);
                }
                catch (Exception e)
                {
                    Debug.WriteLineIf(App.Debugging, e.ToString());
                }

                progress.PercentComplete = (int)((7.0 / steps) * 100);

                App.Current.MainPage = new NavigationPage(new TaskPageStart());
            }
        }
    }
}