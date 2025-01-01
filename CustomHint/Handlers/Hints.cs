using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MEC;
using Exiled.API.Features;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp079;
using CustomHint.API;

namespace CustomHint.Handlers
{
    public class HintsSystem
    {
        private CoroutineHandle _hintUpdaterCoroutine;
        private Queue<string> randomizedHints = new Queue<string>();
        private List<string> hints = new List<string>();

        public void LoadHints()
        {
            string hintsFilePath = FileDotNet.GetPath("Hints.txt");

            try
            {
                if (File.Exists(hintsFilePath))
                {
                    hints = File.ReadAllLines(hintsFilePath)
                        .Where(line => !line.TrimStart().StartsWith("#") && !string.IsNullOrWhiteSpace(line))
                        .ToList();

                    Log.Debug($"Loaded {hints.Count} hints from Hints.txt.");
                }
                else
                {
                    Log.Warn("Hints.txt not found. No hints loaded.");
                    hints = new List<string>();
                }
            }
            catch (Exception ex)
            {
                Log.Warn($"Failed to load Hints.txt: {ex}");
                hints = new List<string>();
            }
        }

        public IEnumerator<float> ContinuousHintDisplay()
        {
            while (Plugin.Instance.RoundEvents.IsRoundActive)
            {
                foreach (var player in Player.List)
                {
                    if (player.Role.Type == RoleTypeId.Spectator ||
                        (!Plugin.Instance.Config.ExcludedRoles.Contains(RoleTypeId.Overwatch) &&
                         player.Role.Type == RoleTypeId.Overwatch))
                    {
                        if (Plugin.Instance.Config.HintForSpectatorsIsEnabled)
                        {
                            DisplayHintForSpectators(player, Round.ElapsedTime);
                        }
                    }
                    else if (!Plugin.Instance.Config.ExcludedRoles.Contains(player.Role.Type) &&
                             !Plugin.Instance.HiddenHudPlayers.Contains(player.UserId))
                    {
                        DisplayHint(player, Round.ElapsedTime);
                    }
                }

                yield return Timing.WaitForSeconds(0.5f);
            }
        }

        public void StartHintUpdater()
        {
            _hintUpdaterCoroutine = Timing.RunCoroutine(HintUpdater());
        }

        public void StopHintUpdater()
        {
            Timing.KillCoroutines(_hintUpdaterCoroutine);
            CurrentHint = "Hint not available";
        }

        private IEnumerator<float> HintUpdater()
        {
            while (Plugin.Instance.RoundEvents.IsRoundActive)
            {
                if (randomizedHints.Count == 0 && hints.Count > 0)
                {
                    randomizedHints = new Queue<string>(hints.OrderBy(_ => Guid.NewGuid()));
                    Log.Debug("Refilled hints queue.");
                }

                if (randomizedHints.Count > 0)
                {
                    CurrentHint = randomizedHints.Dequeue();
                    Log.Debug($"Updated current hint: {CurrentHint}");
                }

                yield return Timing.WaitForSeconds(Plugin.Instance.Config.HintMessageTime);
            }
        }

        public static readonly Dictionary<string, Func<Player, TimeSpan, string>> CorePlaceholders = new()
        {
            { "{round_duration_hours}", (player, roundDuration) => roundDuration.Hours.ToString("D2") },
            { "{round_duration_minutes}", (player, roundDuration) => roundDuration.Minutes.ToString("D2") },
            { "{round_duration_seconds}", (player, roundDuration) => roundDuration.Seconds.ToString("D2") },
            { "{player_nickname}", (player, roundDuration) => player.Nickname },
            { "{player_role}", (player, roundDuration) => GetColoredRoleName(player) },
            { "{player_gamerole}", (player, roundDuration) => Methods.GameRole(player) },
            { "{tps}", (player, roundDuration) => ((int)Server.Tps).ToString() },
            { "{servername}", (player, roundDuration) => Server.Name },
            { "{ip}", (player, roundDuration) => Server.IpAddress },
            { "{port}", (player, roundDuration) => Server.Port.ToString() },
            { "{classd_num}", (player, roundDuration) => Methods.CountRoles().ClassD.ToString() },
            { "{scientist_num}", (player, roundDuration) => Methods.CountRoles().Scientist.ToString() },
            { "{facilityguard_num}", (player, roundDuration) => Methods.CountRoles().FacilityGuard.ToString() },
            { "{mtf_num}", (player, roundDuration) => Methods.CountRoles().MTF.ToString() },
            { "{ci_num}", (player, roundDuration) => Methods.CountRoles().ChaosInsurgency.ToString() },
            { "{scp_num}", (player, roundDuration) => Methods.CountRoles().SCPs.ToString() },
            { "{spectators_num}", (player, roundDuration) => Methods.CountRoles().Spectators.ToString() },
            { "{generators_activated}", (player, roundDuration) => Scp079Recontainer.AllGenerators.Count(gen => gen.Engaged).ToString() },
            { "{generators_max}", (player, roundDuration) => Scp079Recontainer.AllGenerators.Count.ToString() },
            { "{current_time}", (player, roundDuration) => Methods.GetCurrentTime(Plugin.Instance.Config.ServerTimeZone) },
            { "{hints}", (player, roundDuration) => Plugin.Instance.Hints.CurrentHint }
        };

        private string ReplacePlaceholders(string message, Player player, TimeSpan roundDuration)
        {
            foreach (var placeholder in CorePlaceholders)
            {
                message = message.Replace(placeholder.Key, placeholder.Value(player, roundDuration));
            }

            foreach (var placeholder in PlaceholderManager.GetAllPlaceholders())
            {
                if (CorePlaceholders.ContainsKey(placeholder.Key))
                    continue;

                var value = placeholder.Value.Invoke();
                message = message.Replace(placeholder.Key, value ?? string.Empty);
            }

            return message;
        }

        private void DisplayHint(Player player, TimeSpan roundDuration)
        {
            if (!Plugin.Instance.Config.GameHint)
                return;

            string hintMessage;
            if (roundDuration.TotalSeconds <= 60)
                hintMessage = Plugin.Instance.Translation.HintMessageUnderMinute;
            else if (roundDuration.TotalMinutes < 60)
                hintMessage = Plugin.Instance.Translation.HintMessageUnderHour;
            else
                hintMessage = Plugin.Instance.Translation.HintMessageOverHour;

            hintMessage = ReplacePlaceholders(hintMessage, player, roundDuration);

            hintMessage = Plugin.ReplaceColorsInString(hintMessage);
            player.ShowHint(hintMessage, 1.5f);
        }

        private void DisplayHintForSpectators(Player player, TimeSpan roundDuration)
        {
            string hintMessage = Plugin.Instance.Translation.HintMessageForSpectators;

            hintMessage = ReplacePlaceholders(hintMessage, player, roundDuration);

            hintMessage = Plugin.ReplaceColorsInString(hintMessage);
            player.ShowHint(hintMessage, 1.5f);
        }

        private static string GetColoredRoleName(Player player)
        {
            return player.Group != null
                ? $"<color={player.Group.BadgeColor ?? Plugin.Instance.Config.DefaultRoleColor}>{player.Group.BadgeText}</color>"
                : $"<color={Plugin.Instance.Config.DefaultRoleColor}>{Plugin.Instance.Config.DefaultRoleName}</color>";
        }

        public string CurrentHint { get; private set; } = "Hint not available";
    }
}
