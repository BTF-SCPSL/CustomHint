using CustomHint.API;
using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.IO;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace CustomHint.Methods
{
    public class FilesGenerator
    {
        private readonly IDeserializer _deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        private readonly ISerializer _serializer = new SerializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        public void GenerateHintsFile()
        {
            string filePath = FileDotNet.GetPath("Hints.yml");
            string directory = Path.GetDirectoryName(filePath);
            string hintsDir = Path.Combine(directory, "Hints");
            string serverFile = Path.Combine(hintsDir, $"{Server.Port}.yml");

            try
            {
                if (!File.Exists(filePath))
                {
                    Directory.CreateDirectory(hintsDir);

                    var defaultHints = new List<string>
                    {
                        "Hint 1",
                        "Hint 2",
                        "Hint 3"
                    };

                    string yaml = _serializer.Serialize(defaultHints);
                    File.WriteAllText(serverFile, yaml);

                    Log.Info($"Created {serverFile} with default hints.");
                }
                else
                {
                    string yamlContent = File.ReadAllText(filePath);

                    try
                    {
                        var hints = _deserializer.Deserialize<List<string>>(yamlContent);

                        Directory.CreateDirectory(hintsDir);

                        if (!File.Exists(serverFile))
                        {
                            File.WriteAllText(serverFile, yamlContent);
                            Log.Warn($"Migrated Hints.yml content into {serverFile}");
                        }
                        else
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Warn($"Invalid YAML in Hints.yml: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Warn($"Failed to generate hint files: {ex}");
            }
        }
    }
}