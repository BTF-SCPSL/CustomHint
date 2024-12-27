using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using MEC;
using PlayerRoles;
using PlayerRoles.PlayableScps.Scp079;
using CustomHint.API;

namespace CustomHint.Handlers
{
    public class EventHandlers
    {
        private CoroutineHandle _hintUpdaterCoroutine;
        private CoroutineHandle _hintCoroutine;
        private DateTime _roundStartTime;
        private bool _isRoundActive;
        private List<string> hints = new List<string>();
        private Queue<string> randomizedHints = new Queue<string>();
        private string PreviousHint { get; set; }

        public void OnWaitingForPlayers()
        {
            if (Plugin.Instance.Config.Debug)
                Log.Debug("Waiting for players, disabling hints.");

            _isRoundActive = false;

            Timing.KillCoroutines(_hintCoroutine);

            Plugin.Instance.SaveHiddenHudPlayers();
        }

        public void OnRoundStarted()
        {
            if (Plugin.Instance.Config.Debug)
                Log.Debug("Round started, enabling hints.");

            _isRoundActive = true;
            _roundStartTime = DateTime.UtcNow;

            LoadHints();
            StartHintUpdater();
            _hintCoroutine = Timing.RunCoroutine(ContinuousHintDisplay());
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            if (Plugin.Instance.Config.Debug)
                Log.Debug("Round ended, disabling hints.");

            _isRoundActive = false;
            _roundStartTime = default;

            Timing.KillCoroutines(_hintCoroutine);
            StopHintUpdater();
        }

        public void OnPlayerVerified(Exiled.Events.EventArgs.Player.VerifiedEventArgs ev)
        {
            Player player = ev.Player;

            if (player.DoNotTrack)
            {
                if (!Plugin.Instance.HiddenHudPlayers.Remove(player.UserId))
                    return;

                Plugin.Instance.SaveHiddenHudPlayers();
                Log.Debug($"Player {player.Nickname} ({player.UserId}) has DNT enabled and was removed from HiddenHudPlayers.");
                return;
            }

            if (player.Role.Type == RoleTypeId.Spectator)
            {
                Log.Debug($"Player {player.Nickname} ({player.UserId}) is a spectator. HUD will be shown.");
                return;
            }

            if (Plugin.Instance.HiddenHudPlayers.Contains(player.UserId))
            {
                Log.Debug($"Player {player.Nickname} ({player.UserId}) has HUD hidden.");
            }
            else
            {
                Log.Debug($"Player {player.Nickname} ({player.UserId}) has HUD shown.");
            }
        }

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

        private IEnumerator<float> ContinuousHintDisplay()
        {
            while (_isRoundActive)
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

        private void DisplayHint(Player player, TimeSpan roundDuration)
        {
            if (!Plugin.Instance.Config.GameHint)
                return;

            int classDCount = 0;
            int scientistCount = 0;
            int facilityGuardCount = 0;
            int mtfCount = 0;
            int ciCount = 0;
            int scpCount = 0;
            int spectatorsCount = 0;

            foreach (Player p in Player.List)
            {
                switch (p.Role.Type)
                {
                    case RoleTypeId.ClassD:
                        classDCount++;
                        break;
                    case RoleTypeId.Scientist:
                        scientistCount++;
                        break;
                    case RoleTypeId.FacilityGuard:
                        facilityGuardCount++;
                        break;
                    case RoleTypeId.Spectator:
                        spectatorsCount++;
                        break;
                    case RoleTypeId.Overwatch:
                        if (Plugin.Instance.Config.EnableOverwatchCounting)
                            spectatorsCount++;
                        break;
                    default:
                        break;
                }

                switch (p.Role.Team)
                {
                    case Team.FoundationForces:
                        if (p.Role.Type != RoleTypeId.FacilityGuard && p.Role.Type != RoleTypeId.Scientist)
                            mtfCount++;
                        break;
                    case Team.ChaosInsurgency:
                        if (p.Role.Type != RoleTypeId.ClassD)
                            ciCount++;
                        break;
                    case Team.SCPs:
                        scpCount++;
                        break;
                    default:
                        break;
                }
            }

            int generatorsActivated = Scp079Recontainer.AllGenerators.Count(generator => generator.Engaged);
            int generatorsMax = Scp079Recontainer.AllGenerators.Count;

            string currentTime = Methods.GetCurrentTime(Plugin.Instance.Config.ServerTimeZone);

            string hintMessage;
            if (roundDuration.TotalSeconds <= 59)
                hintMessage = Plugin.Instance.Translation.HintMessageUnderMinute;
            else if (roundDuration.TotalMinutes < 60)
                hintMessage = Plugin.Instance.Translation.HintMessageUnderHour;
            else
                hintMessage = Plugin.Instance.Translation.HintMessageOverHour;

            hintMessage = hintMessage
                .Replace("{round_duration_hours}", roundDuration.Hours.ToString("D2"))
                .Replace("{round_duration_minutes}", roundDuration.Minutes.ToString("D2"))
                .Replace("{round_duration_seconds}", roundDuration.Seconds.ToString("D2"))
                .Replace("{player_nickname}", player.Nickname)
                .Replace("{player_role}", GetColoredRoleName(player))
                .Replace("{player_gamerole}", Methods.GameRole(player))
                .Replace("{tps}", ((int)Server.Tps).ToString())
                .Replace("{servername}", Server.Name)
                .Replace("{ip}", Server.IpAddress)
                .Replace("{port}", Server.Port.ToString())
                .Replace("{classd_num}", classDCount.ToString())
                .Replace("{scientist_num}", scientistCount.ToString())
                .Replace("{facilityguard_num}", facilityGuardCount.ToString())
                .Replace("{mtf_num}", mtfCount.ToString())
                .Replace("{ci_num}", ciCount.ToString())
                .Replace("{scp_num}", scpCount.ToString())
                .Replace("{spectators_num}", spectatorsCount.ToString())
                .Replace("{generators_activated}", generatorsActivated.ToString())
                .Replace("{generators_max}", generatorsMax.ToString())
                .Replace("{current_time}", currentTime)
                .Replace("{hints}", CurrentHint);

            hintMessage = Plugin.ReplaceColorsInString(hintMessage);

            player.ShowHint(hintMessage, 3f);
        }

        private void DisplayHintForSpectators(Player player, TimeSpan roundDuration)
        {
            int classDCount = 0;
            int scientistCount = 0;
            int facilityGuardCount = 0;
            int mtfCount = 0;
            int ciCount = 0;
            int scpCount = 0;
            int spectatorsCount = 0;

            foreach (Player p in Player.List)
            {
                switch (p.Role.Type)
                {
                    case RoleTypeId.ClassD:
                        classDCount++;
                        break;
                    case RoleTypeId.Scientist:
                        scientistCount++;
                        break;
                    case RoleTypeId.FacilityGuard:
                        facilityGuardCount++;
                        break;
                    case RoleTypeId.Spectator:
                        spectatorsCount++;
                        break;
                    case RoleTypeId.Overwatch:
                        if (Plugin.Instance.Config.EnableOverwatchCounting)
                            spectatorsCount++;
                        break;
                    default:
                        break;
                }

                switch (p.Role.Team)
                {
                    case Team.FoundationForces:
                        if (p.Role.Type != RoleTypeId.FacilityGuard && p.Role.Type != RoleTypeId.Scientist)
                            mtfCount++;
                        break;
                    case Team.ChaosInsurgency:
                        if (p.Role.Type != RoleTypeId.ClassD)
                            ciCount++;
                        break;
                    case Team.SCPs:
                        scpCount++;
                        break;
                    default:
                        break;
                }
            }

            int generatorsActivated = Scp079Recontainer.AllGenerators.Count(generator => generator.Engaged);
            int generatorsMax = Scp079Recontainer.AllGenerators.Count;

            string currentTime = Methods.GetCurrentTime(Plugin.Instance.Config.ServerTimeZone);

            string hintMessage = Plugin.Instance.Translation.HintMessageForSpectators
                .Replace("{round_duration_hours}", roundDuration.Hours.ToString("D2"))
                .Replace("{round_duration_minutes}", roundDuration.Minutes.ToString("D2"))
                .Replace("{round_duration_seconds}", roundDuration.Seconds.ToString("D2"))
                .Replace("{player_nickname}", player.Nickname)
                .Replace("{player_role}", GetColoredRoleName(player))
                .Replace("{player_gamerole}", Methods.GameRole(player))
                .Replace("{tps}", ((int)Server.Tps).ToString())
                .Replace("{servername}", Server.Name)
                .Replace("{ip}", Server.IpAddress)
                .Replace("{port}", Server.Port.ToString())
                .Replace("{classd_num}", classDCount.ToString())
                .Replace("{scientist_num}", scientistCount.ToString())
                .Replace("{facilityguard_num}", facilityGuardCount.ToString())
                .Replace("{mtf_num}", mtfCount.ToString())
                .Replace("{ci_num}", ciCount.ToString())
                .Replace("{scp_num}", scpCount.ToString())
                .Replace("{spectators_num}", spectatorsCount.ToString())
                .Replace("{generators_activated}", generatorsActivated.ToString())
                .Replace("{generators_max}", generatorsMax.ToString())
                .Replace("{current_time}", currentTime)
                .Replace("{hints}", CurrentHint);

            hintMessage = Plugin.ReplaceColorsInString(hintMessage);

            player.ShowHint(hintMessage, 3f);
        }

        private void StartHintUpdater()
        {
            _hintUpdaterCoroutine = Timing.RunCoroutine(HintUpdater());
        }

        private void StopHintUpdater()
        {
            Timing.KillCoroutines(_hintUpdaterCoroutine);
            CurrentHint = "Hint not available";
        }

        private IEnumerator<float> HintUpdater()
        {
            while (_isRoundActive)
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

        private string GetColoredRoleName(Player player)
        {
            return player.Group != null
                ? $"<color={player.Group.BadgeColor ?? Plugin.Instance.Config.DefaultRoleColor}>{player.Group.BadgeText}</color>"
                : $"<color={Plugin.Instance.Config.DefaultRoleColor}>{Plugin.Instance.Config.DefaultRoleName}</color>";
        }

        public string CurrentHint { get; private set; } = "Hint not available";
    }
}