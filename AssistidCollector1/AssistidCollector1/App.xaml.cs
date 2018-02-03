using AssistidCollector1.Interfaces;
using AssistidCollector1.Models;
using AssistidCollector1.Pages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

using Xamarin.Forms;

namespace AssistidCollector1
{
    public partial class App : Application
    {
        public static string StandardStartPage = "AssistidCollector1.startpage.html";
        public static Assembly MainAssembly = typeof(App).GetTypeInfo().Assembly;
        public static Manifest MainManifest;

        public App()
        {
            InitializeComponent();

            MainPage = new StartPage();

            /**
             *  Create base manifest, if doesn't exist 
             */
            if (!DependencyService.Get<InterfaceSaveLoad>().FileExists("Manifest.json"))
            {
                MainManifest = new Manifest
                {
                    Tasks = new List<Items>()
                    {
                        new Items 
                        {
                            Version = 1,
                            Title = "startpage.html",
                        }
                    }
                };

                DependencyService.Get<InterfaceSaveLoad>().SaveFile("Manifest.json", JsonConvert.SerializeObject(MainManifest));
            }

            /**
             *  Open up existing manifest
             */
            else
            {
                MainManifest = JsonConvert.DeserializeObject<Manifest>( DependencyService.Get<InterfaceSaveLoad>().LoadFile("Manifest.json"));
            }
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }
    }
}
