using AssistidCollector1.Interfaces;
using AssistidCollector1.Models;
using AssistidCollector1.Storage;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AssistidCollector1.Helpers
{
    /// <summary>
    /// Static class for Dropbox services
    /// </summary>
    public static class DropboxServer
    {

        /// <summary>
        /// Download manifest and compare against existing
        /// </summary>
        /// <param name="currentManifest"></param>
        public static async void GetManifest(Manifest currentManifest)
        {
            await DownloadManifest(currentManifest);
        }

        /// <summary>
        /// Pull manifest from dropbox
        /// </summary>
        /// <returns></returns>
        public static async Task DownloadManifest(Manifest currentManifest)
        {
            Debug.WriteLineIf(App.Debugging, "DownloadManifest() <<< Downloading Manifest ...");

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
        
    }
}
