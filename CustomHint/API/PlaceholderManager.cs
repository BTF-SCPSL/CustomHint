using CustomHint.Handlers;
using System.Collections.Generic;
using System;

namespace CustomHint.API
{
    public static class PlaceholderManager
    {
        private static readonly Dictionary<string, Func<string>> Placeholders = new();

        /// <summary>
        /// Registers a custom placeholder.
        /// </summary>
        /// <param name="key">The placeholder key (e.g., {custom_placeholder}).</param>
        /// <param name="valueProvider">A function that returns the value for the placeholder.</param>
        /// <exception cref="ArgumentException">Thrown if the key is already registered in the core plugin.</exception>
        public static void RegisterPlaceholder(string key, Func<string> valueProvider)
        {
            if (string.IsNullOrWhiteSpace(key) || !key.StartsWith("{") || !key.EndsWith("}"))
                throw new ArgumentException($"Placeholder key must be in the format {{key}}, but got: {key}");

            if (HintsSystem.CorePlaceholders.ContainsKey(key))
                throw new ArgumentException($"Placeholder '{key}' is already defined in the core plugin and cannot be overridden.");

            if (!Placeholders.ContainsKey(key))
            {
                Placeholders.Add(key, valueProvider);
            }
        }

        /// <summary>
        /// Retrieves the value for the specified placeholder.
        /// </summary>
        /// <param name="key">The placeholder key.</param>
        /// <returns>The placeholder value, or null if it is not registered.</returns>
        public static string GetPlaceholderValue(string key)
        {
            return Placeholders.TryGetValue(key, out var valueProvider) ? valueProvider() : null;
        }

        /// <summary>
        /// Returns all registered placeholders.
        /// </summary>
        /// <returns>A dictionary of registered placeholders.</returns>
        public static IReadOnlyDictionary<string, Func<string>> GetAllPlaceholders()
        {
            return Placeholders;
        }
    }
}