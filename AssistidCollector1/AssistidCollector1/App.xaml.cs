using AssistidCollector1.Constants;
using AssistidCollector1.Helpers;
using AssistidCollector1.Interfaces;
using AssistidCollector1.Models;
using AssistidCollector1.Pages;
using AssistidCollector1.Storage;
using Dropbox.Api;
using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AssistidCollector1
{
    public partial class App : Application
    {
        public static string StandardStartPage = "AssistidCollector1.startpage.html";
        public static Assembly MainAssembly = typeof(App).GetTypeInfo().Assembly;
        public static Manifest MainManifest;
        
        public string AccessToken
        {
            get
            {
                return Settings.AuthToken;
            }
            set
            {
                Settings.AuthToken = value;
            }
        }

        public string ApplicationId
        {
            get
            {
                return Settings.AppId;
            }
            set
            {
                Settings.AppId = value;
            }
        }

        private static DropboxClient dropboxClient;
        public static DropboxClient DropBoxClient
        {
            get
            {
                return dropboxClient;
            }

            private set
            {
                using (DropboxClient old = dropboxClient)
                {
                    dropboxClient = value;
                }

                DropboxClientChanged?.Invoke(dropboxClient, EventArgs.Empty);
            }
        }

        private static ApplicationDatabase database;
        public static ApplicationDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ApplicationDatabase(DependencyService.Get<InterfaceSaveLoad>().GetLocalFilePath("Database.db3"));
                }

                return database;
            }
        }

        public static event EventHandler DropboxClientChanged;

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool Debugging = true;

        public App()
        {
            InitializeComponent();

            AccessToken = AuthenticationConstants.DevelopmentKey;
            Settings.AppName = "AssistidApp01";

            dropboxClient = new DropboxClient(AccessToken, new DropboxClientConfig(Settings.AppName));




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

            if (ApplicationId == string.Empty)
            {
                ApplicationId = String.Format("{0:X}", RandomString(12));
            }

            MainPage = new LoadingPage();
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
