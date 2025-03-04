﻿using Playnite.SDK;
using Playnite.SDK.Data;
using System.Collections.Generic;

namespace SunshineAppsExporter {
    public class SunshineAppsExporterSettings : ObservableObject {
        private string sunshineAssetsPath = string.Empty;
        private string sunshineAppPath = string.Empty;
        private string sunshineUserName = string.Empty;
        private string sunshinePassword = string.Empty;
        private string sunshineServiceUri = string.Empty;
        private bool exportOnLibraryUpdate;
        private bool exportOnGameInstalled;

        public string SunshineAssetsPath { get => sunshineAssetsPath; set => SetValue(ref sunshineAssetsPath, value); }
        public string SunshineAppPath { get => sunshineAppPath; set => SetValue(ref sunshineAppPath, value); }
        public bool ExportOnLibraryUpdate { get => exportOnLibraryUpdate; set => SetValue(ref exportOnLibraryUpdate, value); }
        public bool ExportOnGameInstalled { get => exportOnGameInstalled; set => SetValue(ref exportOnGameInstalled, value); }
        public string SunshineUserName { get => sunshineUserName; set => SetValue(ref sunshineUserName, value); }
        public string SunshinePassword { get => sunshinePassword; set => SetValue(ref sunshinePassword, value); }
        public string SunshineServiceUri { get => sunshineServiceUri; set => SetValue(ref sunshineServiceUri, value); }
    }

    public class SunshineAppsExporterSettingsViewModel : ObservableObject, ISettings {
        private readonly SunshineAppsExporter plugin;
        private SunshineAppsExporterSettings EditingClone { get; set; }

        private SunshineAppsExporterSettings settings;
        public SunshineAppsExporterSettings Settings { 
            get => settings;
            set {
                settings = value;
                OnPropertyChanged();
            }
        }

        public SunshineAppsExporterSettingsViewModel(SunshineAppsExporter plugin) {
            // Injecting your plugin instance is required for Save/Load method because Playnite saves data to a location based on what plugin requested the operation.
            this.plugin = plugin;

            // Load saved settings.
            var savedSettings = plugin.LoadPluginSettings<SunshineAppsExporterSettings>();

            // LoadPluginSettings returns null if not saved data is available.
            if (savedSettings != null) {
                Settings = savedSettings;
            } else {
                Settings = new SunshineAppsExporterSettings();
            }
        }

        public void BeginEdit() {
            // Code executed when settings view is opened and user starts editing values.
            EditingClone = Serialization.GetClone(Settings);
        }

        public void CancelEdit() {
            // Code executed when user decides to cancel any changes made since BeginEdit was called.
            // This method should revert any changes made to Option1 and Option2.
            Settings = EditingClone;
        }

        public void EndEdit() {
            // Code executed when user decides to confirm changes made since BeginEdit was called.
            // This method should save settings made to Option1 and Option2.
            plugin.SavePluginSettings(Settings);
        }

        public bool VerifySettings(out List<string> errors) {
            // Code execute when user decides to confirm changes made since BeginEdit was called.
            // Executed before EndEdit is called and EndEdit is not called if false is returned.
            // List of errors is presented to user if verification fails.
            errors = new List<string>();
            return true;
        }
    }
}