using System;
using System.Collections.Generic;
using CustomHint.Handlers;
using Exiled.API.Features;

namespace CustomHint.API
{
    public static class PlaceholderManager
    {
        private static readonly Dictionary<string, Func<string>> GlobalPlaceholders = new();
        private static readonly Dictionary<string, Func<Player, string>> PlayerPlaceholders = new();
        private static readonly Dictionary<string, Dictionary<string, Func<string>>> SteamID64Placeholders = new();

        /// <summary>
        /// Registers a global placeholder.
        /// </summary>
        public static void RegisterGlobalPlaceholder(string key, Func<string> valueProvider)
        {
            if (string.IsNullOrWhiteSpace(key) || !key.StartsWith("{") || !key.EndsWith("}"))
                throw new ArgumentException($"Placeholder key must be in the format {{key}}, but got: {key}");

            if (HintsSystem.CorePlaceholders.ContainsKey(key))
                throw new ArgumentException($"Placeholder '{key}' is already defined in the core plugin and cannot be overridden.");

            if (!GlobalPlaceholders.ContainsKey(key))
            {
                GlobalPlaceholders.Add(key, valueProvider);
            }
        }

        /// <summary>
        /// Registers a player-specific placeholder.
        /// </summary>
        public static void RegisterPlayerPlaceholder(string key, Func<Player, string> valueProvider)
        {
            if (string.IsNullOrWhiteSpace(key) || !key.StartsWith("{") || !key.EndsWith("}"))
                throw new ArgumentException($"Placeholder key must be in the format {{key}}, but got: {key}");

            if (HintsSystem.CorePlaceholders.ContainsKey(key))
                throw new ArgumentException($"Placeholder '{key}' is already defined in the core plugin and cannot be overridden.");

            if (!PlayerPlaceholders.ContainsKey(key))
            {
                PlayerPlaceholders.Add(key, valueProvider);
            }
        }

        /// <summary>
        /// Registers a placeholder associated with a specific SteamID64.
        /// </summary>
        public static void RegisterSteamID64Placeholder(string key, string identifier, Func<string> valueProvider)
        {
            if (string.IsNullOrWhiteSpace(key) || !key.StartsWith("{") || !key.EndsWith("}"))
                throw new ArgumentException($"Placeholder key must be in the format {{key}}, but got: {key}");

            if (HintsSystem.CorePlaceholders.ContainsKey(key))
                throw new ArgumentException($"Placeholder '{key}' is already defined in the core plugin and cannot be overridden.");

            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentException("Identifier cannot be null or whitespace.");

            string steamId64 = identifier.Contains("@steam") ? identifier.Split('@')[0] : identifier;

            if (!SteamID64Placeholders.ContainsKey(key))
            {
                SteamID64Placeholders[key] = new Dictionary<string, Func<string>>();
            }

            SteamID64Placeholders[key][steamId64] = valueProvider;
        }

        /// <summary>
        /// Retrieves the value for the specified SteamID64-specific placeholder.
        /// </summary>
        public static string GetSteamID64PlaceholderValue(string key, string steamID64)
        {
            if (SteamID64Placeholders.TryGetValue(key, out var playerPlaceholders))
            {
                if (playerPlaceholders.TryGetValue(steamID64, out var valueProvider))
                {
                    try
                    {
                        return valueProvider();
                    }
                    catch (Exception ex)
                    {
                        Log.Error($"Error while retrieving value for placeholder '{key}' with SteamID64 '{steamID64}': {ex}");
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Removes a SteamID64-specific placeholder for a given key and identifier.
        /// </summary>
        public static void RemoveSteamID64Placeholder(string key, string identifier)
        {
            string steamId64 = identifier.Contains("@steam") ? identifier.Split('@')[0] : identifier;

            if (SteamID64Placeholders.TryGetValue(key, out var playerPlaceholders))
            {
                playerPlaceholders.Remove(steamId64);

                if (playerPlaceholders.Count == 0)
                {
                    SteamID64Placeholders.Remove(key);
                }
            }
        }

        /// <summary>
        /// Returns all registered SteamID64-specific placeholders.
        /// </summary>
        public static IReadOnlyDictionary<string, IReadOnlyDictionary<string, Func<string>>> GetAllSteamID64Placeholders()
        {
            var result = new Dictionary<string, IReadOnlyDictionary<string, Func<string>>>();

            foreach (var key in SteamID64Placeholders)
            {
                result[key.Key] = new Dictionary<string, Func<string>>(key.Value);
            }

            return result;
        }

        public static string GetGlobalPlaceholderValue(string key)
        {
            return GlobalPlaceholders.TryGetValue(key, out var valueProvider) ? valueProvider() : null;
        }

        public static string GetPlayerPlaceholderValue(string key, Player player)
        {
            return PlayerPlaceholders.TryGetValue(key, out var valueProvider) ? valueProvider(player) : null;
        }

        public static IReadOnlyDictionary<string, Func<string>> GetAllGlobalPlaceholders()
        {
            return GlobalPlaceholders;
        }

        public static IReadOnlyDictionary<string, Func<Player, string>> GetAllPlayerPlaceholders()
        {
            return PlayerPlaceholders;
        }
    }
}
