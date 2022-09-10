using Playnite.SDK;
using Playnite.SDK.Models;
using SunshineAppsExporter.Models;

namespace SunshineAppsExporter.Exporters {
    internal class SteamAppExporter : AppExporterBase {
        public SteamAppExporter(SunshineAppsExporterSettings settings, IGameDatabaseAPI gameDatabaseAPI) : base(settings, gameDatabaseAPI) {
        }

        public override SunshineApp ExportGame(Game game, IGameDatabaseAPI gameDatabaseAPI) {
            var app = GetBaseApp(game)
                .AddDetachedCommand($"steam steam://rungameid/{game.GameId}")
                .WithOutput("steam.txt");
            return app;
        }
    }
}
