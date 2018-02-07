//----------------------------------------------------------------------------------------------
// <copyright file="Settings.cs" 
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

using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace AssistidCollector1.Helpers
{
	/// <summary>
	/// This is the Settings static class that can be used in your Core solution or in any
	/// of your client applications. All settings are laid out the same exact way with getters
	/// and setters. 
	/// </summary>
	public static class Settings
	{
        /// <summary>
        /// Helper class for settings
        /// </summary>
		private static ISettings AppSettings
		{
			get
			{
				return CrossSettings.Current;
			}
		}

		private const string SettingsKey = "settings_key";
		private static readonly string SettingsDefault = string.Empty;

        private const string SettingsToken = "settings_token";
        private const string SettingsName = "settings_name";
        private const string SettingsId = "settings_id";

        public static string GeneralSettings
		{
			get
			{
				return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
			}
			set
			{
				AppSettings.AddOrUpdateValue(SettingsKey, value);
			}
		}

        public static string AuthToken
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsToken, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsToken, value);
            }
        }

        public static string AppName
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsName, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsName, value);
            }
        }

        public static string AppId
        {
            get
            {
                return AppSettings.GetValueOrDefault(SettingsId, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(SettingsId, value);
            }
        }
    }
}