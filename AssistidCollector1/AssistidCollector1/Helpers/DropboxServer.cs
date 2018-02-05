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
            using (var response = await App.DropBoxClient.Files.DownloadAsync("/Manifest.json"))
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
            Debug.WriteLineIf(App.Debugging, Settings.AppName + " >>> Downloading Manifest ...");

            using (var response = await App.DropBoxClient.Files.DownloadAsync("/Manifest.json"))
            {
                Debug.WriteLineIf(App.Debugging, Settings.AppName + " >>> Deserializing ...");

                var json = await response.GetContentAsStringAsync();

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
            using (var response = await App.DropBoxClient.Files.DownloadAsync("/Tasks/" + filePath))
            {
                Debug.WriteLineIf(App.Debugging, Settings.AppName + " >>> Downloading " + filePath);

                var receivedData = await response.GetContentAsStringAsync();

                DependencyService.Get<InterfaceSaveLoad>().SaveFile(filePath, receivedData);
            }
        }
    }
}
