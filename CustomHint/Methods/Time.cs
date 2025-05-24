using System;
using System.Globalization;
using Exiled.API.Features;

namespace CustomHint.Methods
{
    public class Time
    {
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

        public static string GetRoundTime(TimeSpan roundDuration)
        {
            var translationroundtime = Plugin.Instance.Translation.RoundTimeFormats;

            if (roundDuration.TotalSeconds <= 60)
                return translationroundtime["seconds"]
                    .Replace("{round_duration_seconds}", roundDuration.Seconds.ToString("D2"));

            if (roundDuration.TotalMinutes < 60)
                return translationroundtime["minutes"]
                    .Replace("{round_duration_minutes}", roundDuration.Minutes.ToString("D2"))
                    .Replace("{round_duration_seconds}", roundDuration.Seconds.ToString("D2"));

            return translationroundtime["hours"]
                .Replace("{round_duration_hours}", roundDuration.Hours.ToString("D2"))
                .Replace("{round_duration_minutes}", roundDuration.Minutes.ToString("D2"))
                .Replace("{round_duration_seconds}", roundDuration.Seconds.ToString("D2"));
        }
    }
}