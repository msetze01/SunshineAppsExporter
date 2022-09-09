using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunshineAppsExporter.Exporters {
    internal class AppStorePluginMapper {
        private static Dictionary<Guid, AppStore> PluginsById { get; set; } = new Dictionary<Guid, AppStore>() {
            [Guid.Parse("e3c26a3d-d695-4cb7-a769-5ff7612c7edd")] = AppStore.Battlenet,
            [Guid.Parse("0e2e793e-e0dd-4447-835c-c44a1fd506ec")] = AppStore.Bethesda,
            [Guid.Parse("00000002-dbd1-46c6-b5d0-b1ba559d10e4")] = AppStore.Epic,
            [Guid.Parse("aebe8b7c-6dc3-4a66-af31-e7375c6b5e9e")] = AppStore.Gog,
            [Guid.Parse("85dd7072-2f20-4e76-a007-41035e390724")] = AppStore.Origin,
            [Guid.Parse("88409022-088a-4de8-805a-fdbac291f00a")] = AppStore.Rockstar,
            [Guid.Parse("cb91dfc9-b977-43bf-8e70-55f46e410fab")] = AppStore.Steam,
            [Guid.Parse("c2f038e5-8b92-4877-91f1-da9094155fc5")] = AppStore.Uplay
        };
        public static AppStore GetById(Guid id) {
            AppStore result = PluginsById.TryGetValue(id, out result) ? result : AppStore.None;
            return result;
        }
    };

    public enum AppStore {
        None,
        Amazon,
        Battlenet,
        Bethesda,
        Epic,
        Gog,
        Origin,
        Rockstar,
        Steam,
        Uplay
    }
}