using Newtonsoft.Json;
using SunshineAppsExporter.Models;
using System.IO;

namespace SunshineAppsExporter {
    internal class SunshineAppsFileWriter {
        private SunshineAppsExporterSettings Settings { get; set; }
        public SunshineAppsFileWriter(SunshineAppsExporterSettings settings) {
            Settings = settings;
        }

        internal void Write(SunshineAppsFile appsFile) {
            var outputPath = Path.Combine(Settings.SunshineAppPath, "apps.json");
            if (!File.Exists(outputPath)) {
                outputPath = @"C:\Program Files\Sunshine\apps.json";
            }
            File.WriteAllText(outputPath, JsonConvert.SerializeObject(appsFile, Formatting.Indented));
        }
    }
}
