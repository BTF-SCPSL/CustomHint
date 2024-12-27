using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;

namespace CustomHint.Configs
{
    public class Translation : ITranslation
    {
        [Description("Hint message for spectators.")]
        public string HintMessageForSpectators { get; set; } = "<size=75%>{servername}\n{ip}:{port}\n\n{player_nickname}, spec, duration: {round_duration_hours}:{round_duration_minutes}:{round_duration_seconds}.\nRole: {player_role}\nTPS: {tps}/60\n\nInformation:\nClass-D personnel: {classd_num} || Scientists: {scientist_num} || Facility Guards: {facilityguard_num} || MTF: {mtf_num} || CI: {ci_num} || SCPs: {scp_num} || Spectators: {spectators_num}\nGenerators activated: {generators_activated}/{generators_max}\n\n{hints}</size>";

        [Description("Hint message for rounds lasting up to 59 seconds.")]
        public string HintMessageUnderMinute { get; set; } = "<size=75%>{servername}\n{ip}:{port}\n\nQuick start! {player_nickname}, round time: {round_duration_seconds}s.\nGame Role: {player_gamerole} || Server Role: {player_role}\nTPS: {tps}/60</size>";

        [Description("Hint message for rounds lasting from 1 to 59 min & 59 sec")]
        public string HintMessageUnderHour { get; set; } = "<size=75%>{servername}\n{ip}:{port}\n\nStill going, {player_nickname}! Time: {round_duration_minutes}:{round_duration_seconds}.\nGame Role: {player_gamerole} || Server Role: {player_role}\nTPS: {tps}/60</size>";

        [Description("Hint message for rounds lasting 1 hour or more.")]
        public string HintMessageOverHour { get; set; } = "<size=75%>{servername}\n{ip}:{port}\n\nLong run, {player_nickname}! Duration: {round_duration_hours}:{round_duration_minutes}:{round_duration_seconds}.\nGame Role: {player_gamerole} || Server Role: {player_role}\nTPS: {tps}/60</size>";

        [Description("Message displayed when the HUD is successfully hidden.")]
        public string HideHudSuccessMessage { get; set; } = "<color=green>You have successfully hidden the server HUD! To get the HUD back, use .showhud</color>";

        [Description("Message displayed when HUD is already hidden.")]
        public string HideHudAlreadyHiddenMessage { get; set; } = "<color=red>You've already hidden the server HUD.</color>";

        [Description("Message displayed when HUD is successfully shown.")]
        public string ShowHudSuccessMessage { get; set; } = "<color=green>You have successfully returned the server HUD! To hide again, use .hidehud</color>";

        [Description("Message displayed when HUD is already shown.")]
        public string ShowHudAlreadyShownMessage { get; set; } = "<color=red>You already have the server HUD displayed.</color>";

        [Description("Message displayed when DNT (Do Not Track) mode is enabled.")]
        public string DntEnabledMessage { get; set; } = "<color=red>Disable DNT (Do Not Track) mode.</color>";

        [Description("Message displayed when commands are disabled on the server.")]
        public string CommandDisabledMessage { get; set; } = "<color=red>This command is disabled on the server.</color>";

        [Description("Game Role of a player, {player_gamerole} is placeholder.")]
        public List<RoleDescription> GameRoles { get; set; } = new()
        {
            new RoleDescription { Role = RoleTypeId.Tutorial, Name = "Tutorial" },
            new RoleDescription { Role = RoleTypeId.ClassD, Name = "Class-D" },
            new RoleDescription { Role = RoleTypeId.Scientist, Name = "Scientist" },
            new RoleDescription { Role = RoleTypeId.FacilityGuard, Name = "Facility Guard" },
            new RoleDescription { Role = RoleTypeId.Filmmaker, Name = "Film Maker" },
            new RoleDescription { Role = RoleTypeId.Overwatch, Name = "Overwatch" },
            new RoleDescription { Role = RoleTypeId.NtfPrivate, Name = "MTF Private" },
            new RoleDescription { Role = RoleTypeId.NtfSergeant, Name = "MTF Sergeant" },
            new RoleDescription { Role = RoleTypeId.NtfSpecialist, Name = "MTF Specialist" },
            new RoleDescription { Role = RoleTypeId.NtfCaptain, Name = "MTF Captain" },
            new RoleDescription { Role = RoleTypeId.ChaosConscript, Name = "CI Conscript" },
            new RoleDescription { Role = RoleTypeId.ChaosRifleman, Name = "CI Rifleman" },
            new RoleDescription { Role = RoleTypeId.ChaosRepressor, Name = "CI Repressor" },
            new RoleDescription { Role = RoleTypeId.ChaosMarauder, Name = "CI Marauder" },
            new RoleDescription { Role = RoleTypeId.Scp049, Name = "SCP-049" },
            new RoleDescription { Role = RoleTypeId.Scp0492, Name = "SCP-049-2" },
            new RoleDescription { Role = RoleTypeId.Scp079, Name = "SCP-079" },
            new RoleDescription { Role = RoleTypeId.Scp096, Name = "SCP-096" },
            new RoleDescription { Role = RoleTypeId.Scp106, Name = "SCP-106" },
            new RoleDescription { Role = RoleTypeId.Scp173, Name = "SCP-173" },
            new RoleDescription { Role = RoleTypeId.Scp939, Name = "SCP-939" }
        };
    }

    public class RoleDescription
    {
        public RoleTypeId Role { get; set; }
        public string Name { get; internal set; }
    }
}