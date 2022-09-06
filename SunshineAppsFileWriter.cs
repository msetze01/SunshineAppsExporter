using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SunshineAppsExporter.Models;

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
