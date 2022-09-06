using SunshineAppsExporter.Models;

namespace SunshineAppsExporter {
    internal interface IAppExporter {
        SunshineApp ExportGame(Playnite.SDK.Models.Game game, Playnite.SDK.IGameDatabaseAPI databaseAPI);
    }
}
