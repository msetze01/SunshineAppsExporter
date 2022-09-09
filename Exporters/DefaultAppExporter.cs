using Playnite.SDK;
using Playnite.SDK.Models;
using SunshineAppsExporter.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace SunshineAppsExporter.Exporters {
    internal class DefaultAppExporter : AppExporterBase {
        public DefaultAppExporter(SunshineAppsExporterSettings settings, IGameDatabaseAPI gameDatabaseAPI) : base(settings, gameDatabaseAPI) { }

        public override SunshineApp ExportGame(Game game, IGameDatabaseAPI gameDatabaseAPI) {
            var app = GetBaseApp(game);

            var action = game.GameActions?.SingleOrDefault(g => g.IsPlayAction);
            if (action == null) { return app; }

            var workingDirectory = action.WorkingDir;
            if (workingDirectory == "{InstallDir}") { workingDirectory = game.InstallDirectory; }
            app.WorkingDir = workingDirectory;

            app.Command = Path.Combine(workingDirectory, action.Path);

            var path = game.InstallDirectory;
            return app;
        }

        internal static List<SunshineApp> DefaultAppsList() {
            return new List<SunshineApp>() {
                new SunshineApp {
                    Name = "Steam BigPicture",
                    Output = "steam.txt",
                    DetachedCommands = new List<string> {
                        "steam steam://open/bigpicture"
                    },
                    ImagePath = "./assets/steam.png"
                }
            };
        }
    }
}
