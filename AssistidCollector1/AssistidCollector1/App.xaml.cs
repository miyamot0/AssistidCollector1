﻿//----------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" 
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

using AssistidCollector1.Helpers;
using AssistidCollector1.Interfaces;
using AssistidCollector1.Models;
using AssistidCollector1.Pages;
using AssistidCollector1.Storage;
using Dropbox.Api;
using System;
using System.Linq;
using Xamarin.Forms;

namespace AssistidCollector1
{
    public partial class App : Application
    {
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

        private static DropboxClient dropboxClient;
        public static DropboxClient DropboxClient
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
        public static bool UpdatingAttempts = false;

        public static int DropboxDeltaTimeout = 2000;

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

            if (Device.RuntimePlatform == Device.Android)
            {
                bool isAdministrator = DependencyService.Get<InterfaceAdministrator>().IsAdmin();
                DependencyService.Get<InterfaceAdministrator>().RequestAdmin(isAdministrator);
            }

            MainPage = new LoadingPage();
        }

        protected override void OnStart() { }

        protected override void OnSleep() { }

        protected override void OnResume() { }

        /// <summary>
        /// Re-init dropbox
        /// </summary>
        public static void ReloadDropbox()
        {
            dropboxClient = new DropboxClient(AccessToken, new DropboxClientConfig(ApplicationName));
        }
    }
}
