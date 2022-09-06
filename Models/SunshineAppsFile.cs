using Newtonsoft.Json;
using System.Collections.Generic;

namespace SunshineAppsExporter.Models {
    internal class SunshineAppsFile {
        [JsonProperty("env")]
        public IDictionary<string, string> Env { get; set; }
        [JsonProperty("apps")]
        public IList<SunshineApp> Apps { get; set; }
    }
}
