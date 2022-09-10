using System.Diagnostics;

namespace SunshineAppsExporter {
    internal class SunshineServiceHandler {
        public bool RestartSunshineService() {
            ProcessStartInfo stopInfo = new ProcessStartInfo("net", "stop sunshinesvc");
            stopInfo.UseShellExecute = true;
            stopInfo.Verb = "runas";
            System.Diagnostics.Process.Start(stopInfo);

            ProcessStartInfo startInfo = new ProcessStartInfo("net", "start sunshinesvc");
            startInfo.UseShellExecute = true;
            startInfo.Verb = "runas";
            System.Diagnostics.Process.Start(startInfo);

            return true;
        }
    }
}
