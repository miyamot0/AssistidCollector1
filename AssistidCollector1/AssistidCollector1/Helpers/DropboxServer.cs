using AssistidCollector1.Interfaces;
using AssistidCollector1.Models;
using AssistidCollector1.Storage;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AssistidCollector1.Helpers
{
    public static class DropboxServer
    {
        public static async Task<string> DownloadManifest()
        {
            using (var response = await App.dropboxClient.Files.DownloadAsync("/Manifest.json"))
            {
                Debug.WriteLineIf(App.Debugging, Settings.AppName + " >>> Downloading manifest");

                string receivedData = await response.GetContentAsStringAsync();

                DependencyService.Get<InterfaceSaveLoad>().SaveFile("Manifest.json", receivedData);

                return receivedData;
            }
        }

        public static async void GetManifest(Manifest currentManifest)
        {
            await DownloadManifest(currentManifest);
        }

        // TODO: hacky, downloads every time right now

        /// <summary>
        /// Pull manifest from dropbox
        /// </summary>
        /// <returns></returns>
        public static async Task DownloadManifest(Manifest currentManifest)
        {
            Debug.WriteLineIf(App.Debugging, "DownloadManifest() <<< Downloading Manifest ...");

            /*
            var full = await App.DropBoxClient.Users.GetCurrentAccountAsync();

            Debug.WriteLineIf(App.Debugging, "Account id    : " + full.AccountId);
            Debug.WriteLineIf(App.Debugging, "Country       : " + full.Country);
            Debug.WriteLineIf(App.Debugging, "Email         : " + full.Email);
            Debug.WriteLineIf(App.Debugging, "Is paired     : " + (full.IsPaired ? "Yes" : "No"));
            Debug.WriteLineIf(App.Debugging, "Locale        : " + full.Locale);
            Debug.WriteLineIf(App.Debugging, "Name");
            Debug.WriteLineIf(App.Debugging, "  Display  : " + full.Name.DisplayName);
            Debug.WriteLineIf(App.Debugging, "  Familiar : " + full.Name.FamiliarName);
            Debug.WriteLineIf(App.Debugging, "  Given    : " + full.Name.GivenName);
            Debug.WriteLineIf(App.Debugging, "  Surname  : " + full.Name.Surname);
            Debug.WriteLineIf(App.Debugging, "Referral link : " + full.ReferralLink);
            */

            using (var response = await App.dropboxClient.Files.DownloadAsync("/Manifest.json"))
            {
                Debug.WriteLineIf(App.Debugging, Settings.AppName + " >>> Deserializing ...");

                var json = await response.GetContentAsStringAsync();

                Debug.WriteLineIf(App.Debugging, Settings.AppName + " >>> " + json);

                Manifest latestManifest = JsonConvert.DeserializeObject<Manifest>(json);

                if (currentManifest == null || currentManifest.Iteration < latestManifest.Iteration)
                {
                    Debug.WriteLineIf(App.Debugging, "Updating files...");

                    foreach (var item in latestManifest.Tasks)
                    {
                        await DownloadFile(item.Content);
                    }

                    ManifestModel saveItem = new ManifestModel()
                    {
                        ID = 0,
                        JSON = JsonConvert.SerializeObject(latestManifest)
                    };

                    if (currentManifest == null)
                    {
                        await App.Database.SaveItemAsync(saveItem);
                    }
                    else
                    {
                        await App.Database.UpdateItemAsync(saveItem);
                    }

                    App.MainManifest = latestManifest;
                }
                else
                {
                    Debug.WriteLineIf(App.Debugging, 
                        "Curr Manifest: " +
                        currentManifest.Iteration.ToString() +
                        " Latest Manifest: " +
                        latestManifest.Iteration.ToString());

                    App.MainManifest = currentManifest;
                }
            }
        }

        /// <summary>
        /// Download file to local
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static async Task DownloadFile(string filePath)
        {
            using (var response = await App.dropboxClient.Files.DownloadAsync("/Tasks/" + filePath))
            {
                Debug.WriteLineIf(App.Debugging, Settings.AppName + " >>> Downloading " + filePath);

                var receivedData = await response.GetContentAsStringAsync();

                DependencyService.Get<InterfaceSaveLoad>().SaveFile(filePath, receivedData);
            }
        }

        /*
        Debug.WriteLineIf(App.Debugging, Settings.AppName + " >>> Staring up...");

        #region Initial Manifest Setup

        if (!DependencyService.Get<InterfaceSaveLoad>().FileExists("Manifest.json"))
        {
            MainManifest = null;

            DependencyService.Get<InterfaceSaveLoad>().SaveFile("Manifest.json", JsonConvert.SerializeObject(MainManifest));
        }
        else
        {
            MainManifest = JsonConvert.DeserializeObject<Manifest>(DependencyService.Get<InterfaceSaveLoad>().LoadFile("Manifest.json"));
        }

        if (CrossConnectivity.Current.IsConnected)
        {
            Debug.WriteLineIf(App.Debugging, Settings.AppName + " >>> Connected, Updating ...");

            DropboxServer.GetManifest(MainManifest);

            Debug.WriteLineIf(App.Debugging, Settings.AppName + " >>> Creating Client Folder...");

            DropBoxClient.Files.CreateFolderV2Async("/" + ApplicationId + "/files");

            Task<string> getManifest = DropboxServer.DownloadManifest();

            MainManifest = JsonConvert.DeserializeObject<Manifest>(getManifest.Result);
        }
        */


        /*

        /// <summary>
        /// Async call to update files
        /// </summary>
        private async void DoUpdate()
        {
            await DownloadManifest();

            MainWebView.Eval("setAwaiter(false);");
        }             

         */
    }
}
