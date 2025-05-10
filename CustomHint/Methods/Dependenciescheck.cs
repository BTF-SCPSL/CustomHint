using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Threading.Tasks;
using Exiled.API.Features;
using Newtonsoft.Json.Linq;

namespace CustomHint.Methods
{
    public class DependenciesCheck
    {
        private static readonly string[] RequiredDependenciesDlls =
        {
            "Newtonsoft.Json.dll",
            "YamlDotNet.dll"
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
                    Log.Debug("All required dependencies are present.");
                    return;
                }

                Log.Info("Missing dependencies detected. Downloading dependencies.zip...");

                using HttpClient client = new();
                client.DefaultRequestHeaders.Add("User-Agent", UserAgent);

                var response = await client.GetAsync(GitHubApiUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                var assets = json["assets"];
                string downloadUrl = null;

                foreach (var asset in assets)
                {
                    string name = asset["name"]?.ToString();
                    if (name != null && name.Equals("dependencies.zip", StringComparison.OrdinalIgnoreCase))
                    {
                        downloadUrl = asset["browser_download_url"]?.ToString();
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

                        string? directory = Path.GetDirectoryName(filePath);
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