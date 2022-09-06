﻿using Playnite.SDK;
using Playnite.SDK.Data;
using System.Collections.Generic;

namespace SunshineAppsExporter {
    public class SunshineAppsExporterSettings : ObservableObject {
        private string sunshineAssetsPath = string.Empty;
        private string sunshineAppPath = string.Empty;
        public string SunshineAssetsPath { get => sunshineAssetsPath; set => SetValue(ref sunshineAssetsPath, value); }
        public string SunshineAppPath { get => sunshineAppPath; set => SetValue(ref sunshineAppPath, value); }
    }

    public class SunshineAppsExporterSettingsViewModel : ObservableObject, ISettings {
        private readonly SunshineAppsExporter plugin;
        private SunshineAppsExporterSettings editingClone { get; set; }

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
            editingClone = Serialization.GetClone(Settings);
        }

        public void CancelEdit() {
            // Code executed when user decides to cancel any changes made since BeginEdit was called.
            // This method should revert any changes made to Option1 and Option2.
            Settings = editingClone;
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