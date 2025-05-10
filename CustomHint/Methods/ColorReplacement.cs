using System;
using System.Collections.Generic;

namespace CustomHint.Methods
{
    public class ColorReplacement
    {
        public static string ReplaceColorsInString(string input)
        {
            const string serverNamePlaceholder = "[SERVERNAME_PLACEHOLDER]";
            input = input.Replace("{servername}", serverNamePlaceholder);

            Dictionary<string, string> colorMapping = new Dictionary<string, string>
            {
                { "pink", "#FF96DE" },
                { "red", "#C50000" },
                { "brown", "#944710" },
                { "silver", "#A0A0A0" },
                { "light_green", "#32CD32" },
                { "crimson", "#DC143C" },
                { "cyan", "#00B7EB" },
                { "aqua", "#00FFFF" },
                { "deep_pink", "#FF1493" },
                { "tomato", "#FF6448" },
                { "yellow", "#FAFF86" },
                { "magenta", "#FF0090" },
                { "blue_green", "#4DFFB8" },
                { "orange", "#FF9666" },
                { "lime", "#BFFF00" },
                { "green", "#228B22" },
                { "emerald", "#50C878" },
                { "carmine", "#960018" },
                { "nickel", "#727472" },
                { "mint", "#98FF98" },
                { "army_green", "#4B5320" },
                { "pumpkin", "#EE7600" }
            };

            foreach (var pair in colorMapping)
            {
                input = ReplaceIgnoreCase(input, $"<color={pair.Key}>", $"<color={pair.Value}>");
            }

            input = input.Replace(serverNamePlaceholder, "{servername}");

            return input;
        }

        private static string ReplaceIgnoreCase(string input, string oldValue, string newValue)
        {
            int index = input.IndexOf(oldValue, StringComparison.OrdinalIgnoreCase);

            while (index != -1)
            {
                input = input.Remove(index, oldValue.Length).Insert(index, newValue);
                index = input.IndexOf(oldValue, index + newValue.Length, StringComparison.OrdinalIgnoreCase);
            }

            return input;
        }
    }
}
