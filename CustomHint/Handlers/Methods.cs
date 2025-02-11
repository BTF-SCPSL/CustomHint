using System;
using System.Linq;
using System.Globalization;
using Exiled.API.Features;
using PlayerRoles;

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

        public static RoleCounts CountRoles(bool includeOverwatchInSpectators = true)
        {
            var counts = new RoleCounts();

            foreach (Player player in Player.List)
            {
                switch (player.Role.Type)
                {
                    case RoleTypeId.ClassD:
                        counts.ClassD++;
                        break;
                    case RoleTypeId.Scientist:
                        counts.Scientist++;
                        break;
                    case RoleTypeId.FacilityGuard:
                        counts.FacilityGuard++;
                        break;
                    case RoleTypeId.Spectator:
                        counts.Spectators++;
                        break;
                    case RoleTypeId.Overwatch:
                        if (Plugin.Instance.Config.EnableOverwatchCounting)
                            counts.Spectators++;
                        break;
                    default:
                        break;
                }

                switch (player.Role.Team)
                {
                    case Team.FoundationForces:
                        if (player.Role.Type != RoleTypeId.FacilityGuard && player.Role.Type != RoleTypeId.Scientist)
                            counts.MTF++;
                        break;
                    case Team.ChaosInsurgency:
                        if (player.Role.Type != RoleTypeId.ClassD)
                            counts.ChaosInsurgency++;
                        break;
                    case Team.SCPs:
                        counts.SCPs++;
                        break;
                    default:
                        break;
                }
            }

            return counts;
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

        public struct RoleCounts
        {
            public int ClassD;
            public int Scientist;
            public int FacilityGuard;
            public int MTF;
            public int ChaosInsurgency;
            public int SCPs;
            public int Spectators;
        }
    }
}
