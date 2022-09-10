using Playnite.SDK;
using Playnite.SDK.Models;
using SunshineAppsExporter.Models;

namespace SunshineAppsExporter.Exporters {
    internal class PlayniteAppExporter : AppExporterBase {
        public PlayniteAppExporter(SunshineAppsExporterSettings settings, IGameDatabaseAPI gameDatabaseAPI) : base(settings, gameDatabaseAPI) {
        }

        public override SunshineApp ExportGame(Game game, IGameDatabaseAPI gameDatabaseAPI) {
            var app = GetBaseApp(game)
                .AddDetachedCommand($"explorer.exe \"playnite://playnite/start/{game.Id}\"");
            return app;
        }
    }
}
