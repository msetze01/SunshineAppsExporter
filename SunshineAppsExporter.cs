using Playnite.SDK;
using Playnite.SDK.Events;
using Playnite.SDK.Plugins;
using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace SunshineAppsExporter {
    public class SunshineAppsExporter : GenericPlugin {
        private static readonly ILogger logger = LogManager.GetLogger();

        private SunshineAppsExporterSettingsViewModel Settings { get; set; }

        private SunshineExporter Exporter { get; set; }

        public override Guid Id { get; } = Guid.Parse("ec5e8181-731e-4ff6-bd9b-3142e79bb1ed");

        public SunshineAppsExporter(IPlayniteAPI api) : base(api) {
            Settings = new SunshineAppsExporterSettingsViewModel(this);
            Properties = new GenericPluginProperties {
                HasSettings = true
            };
            Exporter = new SunshineExporter(api, Settings.Settings);
        }

        public override void OnGameInstalled(OnGameInstalledEventArgs args) {
            // Add code to be executed when game is finished installing.
        }

        public override void OnGameStarted(OnGameStartedEventArgs args) {
            // Add code to be executed when game is started running.
        }

        public override void OnGameStarting(OnGameStartingEventArgs args) {
            // Add code to be executed when game is preparing to be started.
        }

        public override void OnGameStopped(OnGameStoppedEventArgs args) {
            // Add code to be executed when game is preparing to be started.
        }

        public override void OnGameUninstalled(OnGameUninstalledEventArgs args) {
            // Add code to be executed when game is uninstalled.
        }

        public override void OnApplicationStarted(OnApplicationStartedEventArgs args) {
            // Add code to be executed when Playnite is initialized.
        }

        public override void OnApplicationStopped(OnApplicationStoppedEventArgs args) {
            // Add code to be executed when Playnite is shutting down.
        }

        public override void OnLibraryUpdated(OnLibraryUpdatedEventArgs args) {
            // Add code to be executed when library is updated.
        }

        public override ISettings GetSettings(bool firstRunSettings) {
            return Settings;
        }

        public override UserControl GetSettingsView(bool firstRunSettings) {
            return new SunshineAppsExporterSettingsView();
        }

        public override IEnumerable<MainMenuItem> GetMainMenuItems(GetMainMenuItemsArgs args) {
            return new List<MainMenuItem> {
                new MainMenuItem {
                    Description = "Export All",
                    MenuSection = "Sunshine Export",
                    Action = a => {
                        ExportAllGames();
                    }
                },
                new MainMenuItem {
                    Description = "Export Installed Games",
                    MenuSection = "Sunshine Export",
                    Action = a => {
                        ExportInstalledGames();

                    }
                },
                new MainMenuItem {
                    Description = "Restart Sunshine",
                    MenuSection = "Sunshine Export",
                    Action = a => {
                        RestartSunshineService();
                    }
                }
            };
        }

        private void ExportAllGames() {
            Exporter.ExportAll();
            PlayniteApi.Dialogs.ShowMessage("Export complete!", "Sunshine Export");
        }

        private void ExportInstalledGames() {
            Exporter.ExportInstalled();
            PlayniteApi.Dialogs.ShowMessage("Export complete!", "Sunshine Export");
        }

        private void RestartSunshineService() {
            var handler = new SunshineServiceHandler();
            handler.RestartSunshineService();
            PlayniteApi.Dialogs.ShowMessage("Restarted Sunshine service.", "Sunshine Export");
        }
    }
}