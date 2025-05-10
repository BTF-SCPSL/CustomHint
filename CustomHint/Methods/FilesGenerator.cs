using System;
using System.Collections.Generic;
using Exiled.API.Features;
using CustomHint.API;
using System.IO;

namespace CustomHint.Methods
{
    public class FilesGenerator
    {
        public void GenerateHintsFile()
        {
            string filePath = FileDotNet.GetPath("Hints.yml");

            try
            {
                if (!File.Exists(filePath))
                {
                    var defaultHints = new List<string>
                    {
                        "Hint 1",
                        "Hint 2",
                        "Hint 3"
                    };

                    FileDotNet.SaveFile(filePath, defaultHints);
                    Log.Info("Generated Hints.yml config with default content.");
                }
            }
            catch (Exception ex)
            {
                Log.Warn($"Failed to create Hints.yml: {ex}");
            }
        }
    }
}