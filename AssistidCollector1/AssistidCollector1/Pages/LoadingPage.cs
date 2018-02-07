using Acr.UserDialogs;
using AssistidCollector1.Helpers;
using AssistidCollector1.Models;
using AssistidCollector1.Tasks;
using Dropbox.Api.Files;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
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
                        Aspect = Aspect.AspectFill,
                        VerticalOptions = LayoutOptions.CenterAndExpand
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
            Debug.WriteLineIf(App.Debugging, "CheckCredentials()");

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

            LoadAssets();
        }
        
        /// <summary>
        /// Load Stuff
        /// </summary>
        public async void LoadAssets()
        {
            // Skip loading if no internet
            if (!CrossConnectivity.Current.IsConnected)
            {
                App.Current.MainPage = new NavigationPage(new TaskPageStart());
            }

            int filesUploaded = 0;

            Debug.WriteLineIf(App.Debugging, "LoadAssets()");

            CancellationTokenSource cancelSrc = new CancellationTokenSource();
            ProgressDialogConfig config = new ProgressDialogConfig()
                .SetTitle("Contacting Server")
                .SetIsDeterministic(false)
                .SetMaskType(MaskType.Black)
                .SetCancel(onCancel: cancelSrc.Cancel);

            using (IProgressDialog progress = UserDialogs.Instance.Progress(config))
            {
                try
                {
                    if (App.UpdatingAttempts)
                    {
                        progress.Title = "Downloading manifest";

                        var mManifest = await App.Database.GetManifestAsync();

                        if (mManifest != null && mManifest.Count == 1)
                        {
                            App.MainManifest = JsonConvert.DeserializeObject<Manifest>(mManifest.First().JSON);
                        }
                        else
                        {
                            App.MainManifest = null;
                        }

                        progress.Title = "Parsing Manifest";

                        await DropboxServer.DownloadManifest(App.MainManifest);
                    }

                    progress.Title = "Polling local database";

                    var currentItems = await App.Database.GetDataAsync();

                    progress.Title = "Polling remote database";

                    ListFolderResult filesExisting = await DropboxServer.CountIndividualFiles();

                    if (filesExisting == null)
                    {
                        filesUploaded = 0;
                    }
                    else
                    {
                        filesUploaded = filesExisting.Entries.Count;
                    }

                    progress.Title = "Comparing local and remote data";

                    bool missingInList = true;

                    if (currentItems != null && currentItems.Count != filesUploaded)
                    {
                        foreach (Storage.StorageModel currentDataPoint in currentItems)
                        {
                            missingInList = true;

                            Debug.WriteLineIf(App.Debugging, "CurrentDataName <<< " + App.ApplicationId + "_" + currentDataPoint.ID.ToString("d4"));

                            foreach (var file in filesExisting.Entries)
                            {
                                if (file.Name.Contains(App.ApplicationId + "_" + currentDataPoint.ID.ToString("d4")))
                                {
                                    missingInList = false;
                                }

                                Debug.WriteLineIf(App.Debugging, "FileNames <<< " + file.Name);
                            }

                            if (missingInList)
                            {
                                await DropboxServer.UploadFile(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(currentDataPoint.CSV)), currentDataPoint.ID);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLineIf(App.Debugging, e.ToString());
                }

                App.Current.MainPage = new NavigationPage(new TaskPageStart());
            }            
        }
    }
}