using Newtonsoft.Json;
using Playnite.SDK;
using Playnite.SDK.Models;
using SunshineAppsExporter.Models;
using System.Collections.Generic;
using System.Linq;

namespace SunshineAppsExporter.Exporters {
    internal class EpicGamesAppExporter : AppExporterBase {
        private EpicGamesInstalledAppFile LauncherInstalled { get; set; }

        public EpicGamesAppExporter(SunshineAppsExporterSettings settings, IGameDatabaseAPI gameDatabaseAPI) : base(settings, gameDatabaseAPI) {
            LauncherInstalled = JsonConvert.DeserializeObject<EpicGamesInstalledAppFile>(System.IO.File.ReadAllText(@"C:\ProgramData\Epic\UnrealEngineLauncher\LauncherInstalled.dat"));
        }

        public override SunshineApp ExportGame(Game game, IGameDatabaseAPI gameDatabaseAPI) {
            var gameCommand = GetCommand(game);
            var app = GetBaseApp(game)
                .AddDetachedCommand(gameCommand);
            return app;
        }

        private string GetCommand(Game game) {
            var apps = LauncherInstalled.InstallationList?.Where(a => a.InstallLocation == game.InstallDirectory) ?? Enumerable.Empty<EpicGamesInstalledApp>();
            if (apps.Count() == 0) {
                throw new GameNotFoundException($"Game {game.Name} was not found at {game.InstallDirectory}, skipping.");
            }

            var app = apps.Count() == 1 ? apps.FirstOrDefault() : apps.FirstOrDefault(a => a.ArtifactId == game.GameId);
            return $"explorer.exe \"com.epicgames.launcher://apps/{app.ArtifactId}?action=launch&silent=true\"";
        }
    }
}
