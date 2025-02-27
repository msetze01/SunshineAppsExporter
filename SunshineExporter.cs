using Playnite.SDK;
using Playnite.SDK.Models;
using SunshineAppsExporter.Exporters;
using SunshineAppsExporter.Models;
using System;
using System.Collections.Generic;
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
            ExportAll(API.Database.Games.Where(g => g.IsInstalled));
        }

        internal void ExportAll(Game game) {
            ExportAll(new[] { game });
        }

        internal void ExportAll(IEnumerable<Game> games) {
            var writer = new SunshineAppsFileWriter(Settings);
            var file = new SunshineAppsFile {
                Env = new Dictionary<string, string> {
                    ["PATH"] = "$(PATH);C:\\Program Files (x86)\\Steam"
                },
                Apps = GetApps(games)
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

                if (game.Tags?.Any(t => t.Name == "sunshine-ignore") ?? false) {
                    Logger.Warn($"Game {game.Name} is tagged for ignore, skipping export.");
                    continue;
                }

                try {
                    var exporter = GetExporter(game.PluginId);
                    apps.Add(exporter.ExportGame(game, API.Database));
                } catch (GameNotFoundException e) {
                    Logger.Warn(e.ToString());
                }
            }
            return apps;
        }

        internal IAppExporter GetExporter(Guid gamePluginId) {
            var storeType = AppStorePluginMapper.GetById(gamePluginId);

            switch (storeType) {
                case AppStore.Steam:
                    return new SteamAppExporter(Settings, API.Database);
                case AppStore.Epic:
                    return new EpicGamesAppExporter(Settings, API.Database);
                default:
                    return new PlayniteAppExporter(Settings, API.Database);
            }
        }
    }
}
