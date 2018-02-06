using AssistidCollector1.Interfaces;
using AssistidCollector1.Models;
using AssistidCollector1.Storage;
using Dropbox.Api.Files;
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
        /// Create remote folder
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> CreateDropboxFolder()
        {
            var response = await App.DropboxClient.Files.CreateFolderV2Async("/" + App.ApplicationId);

            return response.Metadata.IsFolder;
        }

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

            using (var response = await App.DropboxClient.Files.DownloadAsync("/Manifest.json"))
            {
                var json = await response.GetContentAsStringAsync();

                Manifest latestManifest = JsonConvert.DeserializeObject<Manifest>(json);

                if (currentManifest == null || currentManifest.Iteration < latestManifest.Iteration)
                {
                    foreach (var item in latestManifest.Tasks)
                    {
                        await DownloadFile(item.Content);
                    }

                    if (currentManifest == null)
                    {
                        await App.Database.SaveItemAsync(new ManifestModel()
                        {
                            ID = 0,
                            JSON = JsonConvert.SerializeObject(latestManifest)
                        });
                    }
                    else
                    {
                        await App.Database.UpdateItemAsync(new ManifestModel()
                        {
                            ID = 0,
                            JSON = JsonConvert.SerializeObject(latestManifest)
                        });
                    }

                    App.MainManifest = latestManifest;
                }
                else
                {
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
            Debug.WriteLineIf(App.Debugging, Settings.AppName + " >>> Downloading " + filePath);

            using (var response = await App.DropboxClient.Files.DownloadAsync("/Tasks/" + filePath))
            {
                var receivedData = await response.GetContentAsStringAsync();

                DependencyService.Get<InterfaceSaveLoad>().SaveFile(filePath, receivedData);
            }
        }

        /// <summary>
        /// Upload file
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="fileNumber"></param>
        public static async void UploadFile(System.IO.MemoryStream stream, int fileNumber)
        {
            string filePath = "/" + App.ApplicationId + "/" + App.ApplicationId + "_" + fileNumber.ToString("d4") + ".csv";

            await UploadFile(stream, filePath);
        }

        /// <summary>
        /// Upload file task
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static async Task<string> UploadFile(System.IO.MemoryStream stream, string filePath)
        {
            FileMetadata uploaded = await App.DropboxClient.Files.UploadAsync(filePath, WriteMode.Overwrite.Instance, body: stream);

            return uploaded.Id;
        }        
    }
}
