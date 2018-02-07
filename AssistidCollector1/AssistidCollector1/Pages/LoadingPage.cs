//----------------------------------------------------------------------------------------------
// <copyright file="LoadingPage.cs" 
// Copyright February 2, 2018 Shawn Gilroy
//
// This file is part of AssistidCollector2
//
// AssistidCollector2 is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, version 3.
//
// AssistidCollector2 is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with AssistidCollector2.  If not, see http://www.gnu.org/licenses/. 
// </copyright>
//
// <summary>
// The AssistidCollector2 is a tool to assist clinicans and researchers in the treatment of communication disorders.
// 
// Email: shawn(dot)gilroy(at)temple.edu
//
// </summary>
//----------------------------------------------------------------------------------------------

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
using System.Collections.Generic;
using AssistidCollector1.Storage;
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
                BackgroundColor = Color.FromHex("483A51"),
                Children = {
                    new Image()
                    {
                        Source = "splash.png",
                        Aspect = Aspect.AspectFill,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

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

                    List<StorageModel> currentItems = null;

                    try
                    {
                        currentItems = await App.Database.GetDataAsync();
                    }
                    catch (Exception e)
                    {
                        // Gracefully fail here

                        currentItems = null;

                        Debug.WriteLineIf(App.Debugging, e.ToString());
                    }

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

        /// <summary>
        /// Base methods
        /// </summary>
        /// <returns></returns>
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }
    }
}