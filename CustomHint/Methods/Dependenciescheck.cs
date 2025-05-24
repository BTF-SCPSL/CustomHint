using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using Exiled.API.Features;
using System.Text.Json;

namespace CustomHint.Methods
{
    public class DependenciesCheck
    {
        private static readonly string[] RequiredDependenciesDlls =
        {
            "Newtonsoft.Json.dll"
        };

        private static readonly string[] RequiredPluginsDlls =
        {
            "HintServiceMeow.dll",
            "SSMenuSystem.dll"
        };

        private const string UserAgent = "CustomHint-DependencyChecker";
        private const string GitHubApiUrl = "https://api.github.com/repos/BTF-SCPSL/CustomHint/releases/latest";

        public static async Task CheckAndDownloadDependencies()
        {
            try
            {
                string depsPath = Path.Combine(Paths.Plugins, "dependencies");
                Directory.CreateDirectory(depsPath);

                bool missing = false;

                foreach (string pluginDll in RequiredPluginsDlls)
                {
                    string fullPath = Path.Combine(Paths.Plugins, pluginDll);
                    if (!File.Exists(fullPath))
                    {
                        Log.Warn($"Missing required plugin: {pluginDll}");
                        missing = true;
                    }
                }

                foreach (string dll in RequiredDependenciesDlls)
                {
                    string fullPath = Path.Combine(depsPath, dll);
                    if (!File.Exists(fullPath))
                    {
                        Log.Warn($"Missing dependency: {dll}");
                        missing = true;
                    }
                }

                if (!missing)
                {
                    Log.Debug("All required plugins and dependencies are present.");
                    return;
                }

                Log.Info("Missing dependencies detected. Downloading dependencies.zip...");

                using HttpClient client = new();
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

                var response = await client.GetAsync(GitHubApiUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                using var document = JsonDocument.Parse(content);
                JsonElement root = document.RootElement;

                if (!root.TryGetProperty("assets", out var assets))
                {
                    Log.Error("Missing 'assets' in release metadata.");
                    return;
                }
                string downloadUrl = null;

                foreach (var asset in assets.EnumerateArray())
                {
                    if (asset.TryGetProperty("name", out var nameProp) &&
                        nameProp.GetString()?.Equals("dependencies.zip", StringComparison.OrdinalIgnoreCase) == true &&
                        asset.TryGetProperty("browser_download_url", out var urlProp))
                    {
                        downloadUrl = urlProp.GetString();
                        break;
                    }
                }

                if (string.IsNullOrWhiteSpace(downloadUrl))
                {
                    Log.Error("dependencies.zip not found in the latest release assets.");
                    return;
                }

                string zipPath = Path.Combine(depsPath, "dependencies.zip");
                byte[] zipBytes = await client.GetByteArrayAsync(downloadUrl);
                File.WriteAllBytes(zipPath, zipBytes);

                using (var archive = ZipFile.OpenRead(zipPath))
                {
                    foreach (var entry in archive.Entries)
                    {
                        string filePath = Path.Combine(depsPath, entry.FullName);

                        string directory = Path.GetDirectoryName(filePath);
                        if (!string.IsNullOrEmpty(directory))
                            Directory.CreateDirectory(directory);

                        entry.ExtractToFile(filePath, overwrite: true);
                    }
                }

                File.Delete(zipPath);

                Log.Info("Dependencies successfully downloaded and extracted. Restarting...");
                Server.ExecuteCommand("sr");
            }
            catch (Exception ex)
            {
                Log.Error($"Dependency check failed: {ex.Message}");
            }
        }
    }
}