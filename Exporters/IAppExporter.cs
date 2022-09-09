using SunshineAppsExporter.Models;

namespace SunshineAppsExporter.Exporters {
    internal interface IAppExporter {
        SunshineApp ExportGame(Playnite.SDK.Models.Game game, Playnite.SDK.IGameDatabaseAPI databaseAPI);
    }
}
