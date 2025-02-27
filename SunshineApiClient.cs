using Newtonsoft.Json;
using SunshineAppsExporter.Exporters;
using SunshineAppsExporter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SunshineAppsExporter {
    internal class SunshineRestApiClient {
        public Uri ServiceUri { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        
        private readonly HttpClient client = new HttpClient();

        public SunshineRestApiClient(Uri serviceUri, string userName, string password) {
            ServiceUri = serviceUri ?? throw new ArgumentNullException(nameof(serviceUri));
            UserName = userName ?? throw new ArgumentNullException(nameof(userName));
            Password = password ?? throw new ArgumentNullException(nameof(password));

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var authenticationString = $"{UserName}:{Password}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.ASCIIEncoding.Default.GetBytes(authenticationString));
            client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);
        }

        public async Task<IList<SunshineApp>> GetApps() {
            var allApps = await GetAllApps();
            return allApps.Apps;
        }

        private async Task<SunshineAppsFile> GetAllApps() {
            var url = new Uri(ServiceUri.ToString() + "/api/apps");
            var result = await client.GetStringAsync(url.ToString());
            return JsonConvert.DeserializeObject<SunshineAppsFile>(result);
        }

        public async Task PutApp(SunshineApp app) {
            var allApps = await GetAllApps();
            var apps = allApps.Apps;
            var remoteApps = apps.Where(a => a.Name == app.Name);

            // More than one? Bail
            if (remoteApps.Count() > 1) {
                throw new GameNotFoundException($"More than one game on the remote server matches '{app.Name}', skipping");
            }

            // Already there? Delete it first
            var remoteApp = remoteApps.FirstOrDefault();
            if (remoteApp != null) {
                await DeleteApp(app);
            }

            var content = new StringContent(JsonConvert.SerializeObject(app));
            var url = new Uri(ServiceUri.ToString() + "/api/apps");
            await client.PostAsync(url, content);
        }

        public async Task DeleteApp(SunshineApp app) {
            var allApps = await GetAllApps();
            await DeleteApp(app, allApps);
        }

        public async Task DeleteApp(SunshineApp app, SunshineAppsFile appsFile) {
            var remoteApps = appsFile.Apps.Where(a => a.Name == app.Name);
            if (remoteApps.Count() == 0) {
                throw new GameNotFoundException($"No game with the name '{app.Name}' was found on the remote server.");
            }

            if (remoteApps.Count() > 1) {
                throw new InvalidOperationException($"Found {remoteApps.Count()} games matching the name '{app.Name}', cannot continue.");
            }

            var remoteApp = remoteApps.Single();
            int index = appsFile.Apps.IndexOf(remoteApp);

            var uri = ServiceUri + $"/api/apps/{index}";
            var result = await client.DeleteAsync(uri);
            
            if (!result.IsSuccessStatusCode) {
                throw new ApplicationException($"The remote operation failed: ({result.StatusCode}) {result.RequestMessage}");
            }
        }
    }
}
