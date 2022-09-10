using Playnite.SDK;
using Playnite.SDK.Models;
using SunshineAppsExporter.Models;

namespace SunshineAppsExporter.Exporters {
    internal abstract class AppExporterBase : IAppExporter {
        protected SunshineAppsExporterSettings Settings { get; set; }
        protected IGameDatabaseAPI GameDatabaseAPI { get; set; }
        internal AppExporterBase(SunshineAppsExporterSettings settings, IGameDatabaseAPI gameDatabaseAPI) {
            Settings = settings;
            GameDatabaseAPI = gameDatabaseAPI;
        }

        public abstract SunshineApp ExportGame(Game game, IGameDatabaseAPI gameDatabaseAPI);

        protected virtual string GetGameImage(Game game) {
            return GameImageConverter.GetGameImage(game, GameDatabaseAPI, Settings);
        }

        protected virtual SunshineApp GetBaseApp(Game game) {
            var app = new SunshineApp {
                Name = game.Name,
                ImagePath = GetGameImage(game)
            };
            return app;
        }
    }
}
