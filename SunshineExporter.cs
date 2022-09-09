using Playnite.SDK;
using Playnite.SDK.Models;
using SunshineAppsExporter.Exporters;
using SunshineAppsExporter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SunshineAppsExporter {
    internal class SunshineExporter {
        private ILogger Logger { get; set; } = LogManager.GetLogger(nameof(SunshineExporter));

        private IPlayniteAPI API { get; set; }

        private SunshineAppsExporterSettings Settings { get; set; }

        public SunshineExporter(IPlayniteAPI api, SunshineAppsExporterSettings settings) {
            API = api;
            Settings = settings;
        }

        internal void ExportAll() {
            var writer = new SunshineAppsFileWriter(Settings);
            var file = new SunshineAppsFile {
                Env = new Dictionary<string, string> {
                    ["PATH"] = "$(PATH);C:\\Program Files (x86)\\Steam"
                },
                Apps = GetApps(API.Database.Games)
            };
            writer.Write(file);
        }

        internal void ExportInstalled() {
            var writer = new SunshineAppsFileWriter(Settings);
            var file = new SunshineAppsFile {
                Env = new Dictionary<string, string> {
                    ["PATH"] = "$(PATH);C:\\Program Files (x86)\\Steam"
                },
                Apps = GetApps(API.Database.Games.Where(g => g.IsInstalled))
            };
            writer.Write(file);
        }

        internal List<SunshineApp> GetApps(IEnumerable<Game> games) {
            var apps = DefaultAppExporter.DefaultAppsList();

            foreach (var game in games) {
                var action = game.GameActions?.SingleOrDefault(g => g.IsPlayAction);
                if (action == null && !game.IncludeLibraryPluginAction) {
                    Logger.Warn($"Game {game.Name} has no Play action set, skipping export.");
                    continue;
                }

                IAppExporter exporter = GetExporter(game.PluginId);
                apps.Add(exporter.ExportGame(game, API.Database));
            }
            return apps;
        }

        internal IAppExporter GetExporter(Guid gamePluginId) {
            var storeType = AppStorePluginMapper.GetById(gamePluginId);
            switch (storeType) {
                case AppStore.Steam:
                    return new SteamAppExporter(Settings, API.Database);
                default:
                    return new DefaultAppExporter(Settings, API.Database);
            }
        }
    }
}
