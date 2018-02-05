using AssistidCollector1.Helpers;
using AssistidCollector1.Interfaces;
using AssistidCollector1.Models;
using AssistidCollector1.Pages;
using AssistidCollector1.Storage;
using AssistidCollector1.Tasks;
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
        
        public static string AccessToken
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

        public static string ApplicationId
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

        public static string ApplicationName
        {
            get
            {
                return Settings.AppName;
            }
            set
            {
                Settings.AppName = value;
            }
        }

        public static DropboxClient dropboxClient;
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

            if (ApplicationId == string.Empty)
            {
                ApplicationId = String.Format("{0:X}", RandomString(12));
            }

            if (ApplicationName == string.Empty)
            {
                ApplicationName = "AssistidApp01";
            }

            //MainPage = new LoadingPage();
            MainPage = new TaskPageStart();
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }

        public static void ReloadDropbox()
        {
            dropboxClient = new DropboxClient(AccessToken, new DropboxClientConfig(ApplicationName));
        }
    }
}
