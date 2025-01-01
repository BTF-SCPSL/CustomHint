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
        private static readonly Dictionary<string, Func<string, string>> SteamID64Placeholders = new();

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
        /// <param name="key">The placeholder key (e.g., {custom_placeholder}).</param>
        /// <param name="steamId64">The SteamID64 as a <see cref="long"/>.</param>
        /// <param name="valueProvider">A function that returns the value for the placeholder.</param>
        public static void RegisterSteamID64Placeholder(string key, long steamId64, Func<string> valueProvider)
        {
            RegisterSteamID64Placeholder(key, steamId64.ToString(), valueProvider);
        }

        /// <summary>
        /// Registers a placeholder associated with a specific SteamID64.
        /// </summary>
        /// <param name="key">The placeholder key (e.g., {custom_placeholder}).</param>
        /// <param name="identifier">The SteamID64 or Player.UserId as a <see cref="string"/>.</param>
        /// <param name="valueProvider">A function that returns the value for the placeholder.</param>
        public static void RegisterSteamID64Placeholder(string key, string identifier, Func<string> valueProvider)
        {
            if (string.IsNullOrWhiteSpace(key) || !key.StartsWith("{") || !key.EndsWith("}"))
                throw new ArgumentException($"Placeholder key must be in the format {{key}}, but got: {key}");

            if (HintsSystem.CorePlaceholders.ContainsKey(key))
                throw new ArgumentException($"Placeholder '{key}' is already defined in the core plugin and cannot be overridden.");

            if (string.IsNullOrWhiteSpace(identifier))
                throw new ArgumentException("Identifier cannot be null or whitespace.");

            // Extract SteamID64 if the identifier contains '@steam'
            string steamId64 = identifier.Contains("@steam")
                ? identifier.Split('@')[0]
                : identifier;

            if (!SteamID64Placeholders.ContainsKey(key))
            {
                SteamID64Placeholders.Add(key, _ => valueProvider());
            }
        }

        /// <summary>
        /// Retrieves the value for the specified SteamID64-specific placeholder.
        /// </summary>
        /// <param name="key">The placeholder key.</param>
        /// <param name="steamID64">The SteamID64 for whom the placeholder value is requested.</param>
        /// <returns>The placeholder value, or null if it is not registered.</returns>
        public static string GetSteamID64PlaceholderValue(string key, string steamID64)
        {
            return SteamID64Placeholders.TryGetValue(key, out var valueProvider) ? valueProvider(steamID64) : null;
        }

        /// <summary>
        /// Returns all registered SteamID64-specific placeholders.
        /// </summary>
        /// <returns>A dictionary of registered SteamID64-specific placeholders.</returns>
        public static IReadOnlyDictionary<string, Func<string, string>> GetAllSteamID64Placeholders()
        {
            return SteamID64Placeholders;
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
