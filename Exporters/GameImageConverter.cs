using Playnite.SDK;
using Playnite.SDK.Models;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace SunshineAppsExporter.Exporters {
    internal class GameImageConverter {
        public static string GetGameImage(Game game, IGameDatabaseAPI gameDatabaseAPI, SunshineAppsExporterSettings settings) {
            // Valid cover image?
            if (game.CoverImage == null) { return null; }

            var folderPath = gameDatabaseAPI.GetFileStoragePath(game.Id);
            var mediaFolder = new DirectoryInfo(folderPath).Parent.FullName;
            var imagePath = Path.Combine(mediaFolder, game.CoverImage);
            if (!File.Exists(imagePath)) { return null; }

            // Valid save path for assets?
            if (string.IsNullOrWhiteSpace(settings.SunshineAssetsPath)) {
                settings.SunshineAssetsPath = @"c:\program files\sunshine\assets";
            }
            var targetPath = Path.GetFullPath(settings.SunshineAssetsPath);
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
                return targetImageInfo.FullName;

            } catch (FileNotFoundException) {
                return null;
            }
        }

    }
}
