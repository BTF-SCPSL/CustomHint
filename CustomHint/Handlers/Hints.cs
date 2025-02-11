using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MEC;
using Exiled.API.Features;
using PlayerRoles.PlayableScps.Scp079;
using CustomHint.API;
using HintServiceMeow.Core.Models.Hints;
using HintServiceMeow.Core.Utilities;
using HintServiceMeow.Core.Enum;

namespace CustomHint.Handlers
{
    public class HintsSystem
    {
        private CoroutineHandle _hintUpdaterCoroutine;
        private Queue<string> randomizedHints = new Queue<string>();
        private List<string> hints = new List<string>();
        private readonly Dictionary<Player, List<DynamicHint>> activeHints = new();

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
            { "{round_time}", (player, roundDuration) => Methods.GetRoundTime(roundDuration) },
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

        public void AssignHints(Player player)
        {
            if (player == null || player.ReferenceHub == null)
            {
                Log.Warn("Player or ReferenceHub is null. Skipping hint assignment.");
                return;
            }

            var playerDisplay = PlayerDisplay.Get(player.ReferenceHub);
            if (playerDisplay == null)
            {
                Log.Warn($"PlayerDisplay is null for {player.Nickname} ({player.UserId}). Hints will not be assigned.");
                return;
            }

            Log.Debug($"Assigning hints to {player.Nickname} ({player.UserId}) - Role: {player.Role.Type}");

            foreach (var hint in Plugin.Instance.Config.Hints)
            {
                var hintIdPlaceholder = $"{{ch_{hint.Id}}}";

                var dynamicHint = new DynamicHint
                {
                    AutoText = update =>
                    {
                        if (!hint.Roles.Contains(player.Role.Type) ||
                            (Plugin.Instance.HiddenHudPlayers.Contains(player.UserId) && hint.CanBeHidden))
                        {
                            return "";
                        }

                        string processedText = ReplacePlaceholders(hint.Text, player, Round.ElapsedTime);
                        return processedText.Replace(hintIdPlaceholder, processedText);
                    },
                    SyncSpeed = HintSyncSpeed.Fastest,
                    FontSize = (int)hint.FontSize,
                    TargetX = hint.PositionX,
                    TargetY = hint.PositionY
                };

                playerDisplay.AddHint(dynamicHint);
                Log.Debug($"Hint '{hint.Id}' assigned to {player.Nickname}.");

                if (!activeHints.ContainsKey(player))
                    activeHints[player] = new List<DynamicHint>();

                activeHints[player].Add(dynamicHint);
            }
        }

        public void RemoveHints(Player player)
        {
            if (player == null || player.ReferenceHub == null)
            {
                Log.Warn("Player or ReferenceHub is null. Skipping hint removal.");
                return;
            }

            var playerDisplay = PlayerDisplay.Get(player.ReferenceHub);
            if (playerDisplay == null)
            {
                Log.Warn($"PlayerDisplay is null for {player.Nickname} ({player.UserId}). Unable to remove hints.");
                return;
            }

            if (activeHints.TryGetValue(player, out var hints))
            {
                foreach (var hint in hints)
                {
                    playerDisplay.RemoveHint(hint);
                }

                activeHints.Remove(player);
                Log.Debug($"Removed all active hints for {player.Nickname}.");
            }

            playerDisplay.ClearHint();
        }

        private string ReplacePlaceholders(string message, Player player, TimeSpan roundDuration)
        {
            foreach (var placeholder in CorePlaceholders)
            {
                message = message.Replace(placeholder.Key, placeholder.Value(player, roundDuration));
            }

            foreach (var placeholder in PlaceholderManager.GetAllGlobalPlaceholders())
            {
                if (CorePlaceholders.ContainsKey(placeholder.Key))
                    continue;

                var value = placeholder.Value.Invoke();
                message = message.Replace(placeholder.Key, value ?? string.Empty);
            }

            foreach (var placeholder in PlaceholderManager.GetAllPlayerPlaceholders())
            {
                if (CorePlaceholders.ContainsKey(placeholder.Key))
                    continue;

                var value = placeholder.Value.Invoke(player);
                message = message.Replace(placeholder.Key, value ?? string.Empty);
            }

            return message;
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
