using System.Globalization;
using System;
using System.Linq;
using Exiled.API.Features;

namespace CustomHint.Handlers
{
    public static class Methods
    {
        public static string GameRole(Player player)
        {
            var role = Plugin.Instance.Translation.GameRoles
                .FirstOrDefault(r => r.Role == player.Role.Type);

            return role?.Name ?? player.Role.Type.ToString();
        }

        public static string GetCurrentTime(string timeZone)
        {
            try
            {
                TimeZoneInfo tzInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
                DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, tzInfo);

                return localTime.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch (TimeZoneNotFoundException)
            {
                Log.Warn($"TimeZone '{timeZone}' not found. Falling back to UTC.");
                return DateTime.UtcNow.ToString("HH:mm:ss", CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Log.Error($"Error while getting current time: {ex}");
                return "N/A";
            }
        }
    }
}