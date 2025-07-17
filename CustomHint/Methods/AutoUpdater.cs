using System;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Exiled.API.Features;
using System.IO.Compression;

namespace CustomHint.Methods
{
    public class AutoUpdater
    {
        private const string RepositoryUrl = "https://api.github.com/repos/BTF-SCPSL/CustomHint/releases/latest";
        private const string UserAgent = "CustomHint-Updater";

        public async Task CheckForUpdates()
        {
            Log.Info($"Current version of the plugin: v{Plugin.Instance.Version}");
            Log.Info("Checking for updates...");

            try
            {
                var latestVersion = await GetLatestVersionAsync();

                if (latestVersion == null)
                {
                    Log.Warn("Failed to fetch the latest plugin version.");
                    return;
                }

                CompareVersions(latestVersion);
            }
            catch (Exception ex)
            {
                Log.Warn($"Error while checking for updates: {ex}");
            }
        }

        private async Task<string> GetLatestVersionAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

                var response = await client.GetAsync(RepositoryUrl);
                if (!response.IsSuccessStatusCode)
                {
                    Log.Warn($"Failed to fetch data from GitHub API. Status code: {response.StatusCode}");
                    return null;
                }

                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                var tag = json["tag_name"]?.ToString();
                var assets = json["assets"];

                if (string.IsNullOrEmpty(tag) || assets == null)
                    return null;

                return tag;
            }
        }

        private void CompareVersions(string latestVersion)
        {
            var currentVersion = Plugin.Instance.Version.ToString();
            var latestVersionClean = latestVersion.TrimStart('v');

            int comparison = CompareSemanticVersions(currentVersion, latestVersionClean);

            if (comparison < 0)
            {
                if (Plugin.Instance.Config.AutoUpdater)
                {
                    Log.Warn("Attention! The plugin version is older than the latest version, downloading the update...");
                    Task.Run(() => DownloadAndInstallLatestVersion());
                }
                else
                {
                    Log.Warn($"Attention! The plugin version is older than the latest version, it is recommended to update the plugin: https://github.com/BTF-SCPSL/CustomHint/releases/tag/{latestVersion}");
                }
            }
            else if (comparison == 0)
            {
                Log.Info("No new version has been found. You are on the latest version of the plugin.");
            }
            else
            {
                Log.Info("No new version has been found. You must be using the pre-release version of the plugin.");
            }
        }

        private int CompareSemanticVersions(string current, string latest)
        {
            var currentParts = current.Split('.');
            var latestParts = latest.Split('.');

            for (int i = 0; i < Math.Max(currentParts.Length, latestParts.Length); i++)
            {
                int currentPart = i < currentParts.Length ? int.Parse(currentParts[i]) : 0;
                int latestPart = i < latestParts.Length ? int.Parse(latestParts[i]) : 0;

                if (currentPart < latestPart) return -1;
                if (currentPart > latestPart) return 1;
            }

            return 0;
        }

        private async Task DownloadAndInstallLatestVersion()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

                    var response = await client.GetAsync(RepositoryUrl);
                    if (!response.IsSuccessStatusCode)
                    {
                        Log.Error($"Failed to fetch release data. Status code: {response.StatusCode}");
                        return;
                    }

                    var content = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(content);

                    var assets = json["assets"];
                    if (assets == null)
                    {
                        Log.Error("No assets found in the release.");
                        return;
                    }

                    string pluginsPath = Path.Combine(Paths.Plugins, "CustomHint", "Auto Updater");
                    Directory.CreateDirectory(pluginsPath);

                    string dependenciesPath = Path.Combine(Paths.Plugins);
                    Directory.CreateDirectory(dependenciesPath);

                    foreach (var asset in assets)
                    {
                        var downloadUrl = asset["browser_download_url"]?.ToString();
                        var fileName = asset["name"]?.ToString();

                        if (downloadUrl == null || fileName == null)
                            continue;

                        if (fileName.Equals("dependencies.zip", StringComparison.OrdinalIgnoreCase))
                        {
                            string zipPath = Path.Combine(dependenciesPath, "dependencies.zip");
                            var fileBytes = await client.GetByteArrayAsync(downloadUrl);
                            File.WriteAllBytes(zipPath, fileBytes);

                            using (var archive = ZipFile.OpenRead(zipPath))
                            {
                                foreach (var entry in archive.Entries)
                                {
                                    string destinationPath = Path.Combine(dependenciesPath, entry.FullName);
                                    string directory = Path.GetDirectoryName(destinationPath);
                                    if (!string.IsNullOrEmpty(directory))
                                        Directory.CreateDirectory(directory);

                                    entry.ExtractToFile(destinationPath, overwrite: true);
                                }
                            }

                            File.Delete(zipPath);
                            Log.Info("Extracted dependencies.zip to dependencies folder.");
                        }
                        else if (fileName.Equals("CustomHint.dll", StringComparison.OrdinalIgnoreCase))
                        {
                            string targetPath = Path.Combine(Paths.Plugins, fileName);
                            var fileBytes = await client.GetByteArrayAsync(downloadUrl);
                            File.WriteAllBytes(targetPath, fileBytes);
                            Log.Info($"Downloaded and installed: {fileName} to {targetPath}");
                        }
                        else
                        {
                            string targetPath = Path.Combine(pluginsPath, fileName);
                            var fileBytes = await client.GetByteArrayAsync(downloadUrl);
                            File.WriteAllBytes(targetPath, fileBytes);
                            Log.Info($"Downloaded and installed: {fileName} to {targetPath}");
                        }
                    }

                    ServerStatic.StopNextRound = ServerStatic.NextRoundAction.Restart;
                    Log.Info("The plugin has been successfully updated! Changes are applied at the end of the round.");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to download or install the update: {ex}");
            }
        }
    }
}