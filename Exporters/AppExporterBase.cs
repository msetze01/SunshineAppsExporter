using Playnite.SDK.Models;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;
using Playnite.SDK;
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
            // Valid cover image?
            if (game.CoverImage == null) { return null; }

            var folderPath = GameDatabaseAPI.GetFileStoragePath(game.Id);
            var mediaFolder = new DirectoryInfo(folderPath).Parent.FullName;
            var imagePath = Path.Combine(mediaFolder, game.CoverImage);
            if (!File.Exists(imagePath)) { return null; }

            // Valid save path for assets?
            if (string.IsNullOrWhiteSpace(Settings.SunshineAssetsPath)) {
                Settings.SunshineAssetsPath = @"c:\program files\sunshine\assets";
            }
            var targetPath = Path.GetFullPath(Settings.SunshineAssetsPath);
            var targetPathDir = new DirectoryInfo(targetPath);
            if (!targetPathDir.Exists) { targetPathDir.Create(); }

            // Image exists already?
            var targetImage = Path.Combine(targetPath, game.Id.ToString() + ".png");
            var targetImageInfo = new FileInfo(targetImage);
            if (targetImageInfo.Exists) { return targetImageInfo.FullName; }

            // Get PNG cover image
            try {
                var image = Image.FromFile(imagePath);

                // Already a PNG image?
                if (image.RawFormat == ImageFormat.Png) {
                    File.Copy(imagePath, targetImage, true);
                    if (targetImageInfo.Exists) { return targetImageInfo.FullName; }
                }

                // Copy failed or cover image is not a PNG. Convert to PNG?
                image.Save(targetImage, ImageFormat.Png);
                if (targetImageInfo.Exists) { return targetImageInfo.FullName; }

                // Bail
                return null;

            } catch (FileNotFoundException) {
                return null;
            }
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
