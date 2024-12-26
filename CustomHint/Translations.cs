using System.ComponentModel;
using Exiled.API.Interfaces;

namespace CustomHint
{
    public class Translations : ITranslation
    {
        [Description("Hint message for spectators.")]
        public string HintMessageForSpectators { get; set; } = "<size=75%>{servername}\n{ip}:{port}\n\n{player_nickname}, spec, duration: {round_duration_hours}:{round_duration_minutes}:{round_duration_seconds}.\nRole: {player_role}\nTPS: {tps}/60\n\nInformation:\nClass-D personnel: {classd_num} || Scientists: {scientist_num} || Facility Guards: {facilityguard_num} || MTF: {mtf_num} || CI: {ci_num} || SCPs: {scp_num} || Spectators: {spectators_num}\nGenerators activated: {generators_activated}/{generators_max}\n\n{hints}</size>";

        [Description("Hint message for rounds lasting up to 59 seconds.")]
        public string HintMessageUnderMinute { get; set; } = "<size=75%>{servername}\n{ip}:{port}\n\nQuick start! {player_nickname}, round time: {round_duration_seconds}s.\nRole: {player_role}\nTPS: {tps}/60</size>";

        [Description("Hint message for rounds lasting from 1 to 59 min & 59 sec")]
        public string HintMessageUnderHour { get; set; } = "<size=75%>{servername}\n{ip}:{port}\n\nStill going, {player_nickname}! Time: {round_duration_minutes}:{round_duration_seconds}.\nRole: {player_role}\nTPS: {tps}/60</size>";

        [Description("Hint message for rounds lasting 1 hour or more.")]
        public string HintMessageOverHour { get; set; } = "<size=75%>{servername}\n{ip}:{port}\n\nLong run, {player_nickname}! Duration: {round_duration_hours}:{round_duration_minutes}:{round_duration_seconds}.\nRole: {player_role}\nTPS: {tps}/60</size>";

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

        [Description("Game Role of a player, {player_GameRole} is placeholder.")]
        public string GameRoleClassD { get; set; } = "Class-D";
        public string GameRoleFacilityGuard { get; set; } = "Facility Guard";
        public string GameRoleScientist {get; set; } = "Scientist";
        public string GameRoleTutorial {get; set; } = "Tutorial";
        public string GameRoleFilmmaker {get; set; } = "Film Maker";
        public string GameRoleOverwatch {get; set; } = "Overwatch";
        
        public string GameRoleNtfPrivate {get; set; } = "MTF Private";
        public string GameRoleNtfSergeant {get; set; } = "MTF Sergeant";
        public string GameRoleNtfSpecialist {get; set; } = "MTF Specialist";
        public string GameRoleNtfCaptain {get; set; } = "MTF Captain";
        
        public string GameRoleChaosConscript {get; set; } = "CI Conscript";
        public string GameRoleChaosRifleman {get; set; } = "CI Rifleman";
        public string GameRoleChaosRepressor {get; set; } = "CI Repressor";
        public string GameRoleChaosMarauder {get; set; } = "CI Marauder";
        
        public string GameRoleScp049 {get; set; } = "SCP-049";
        public string GameRoleScp0492 {get; set; } = "SCP-049-2";
        public string GameRoleScp079 {get; set; } = "SCP-079";
        public string GameRoleScp096 {get; set; } = "SCP-096";
        public string GameRoleScp106 {get; set; } = "SCP-106";
        public string GameRoleScp173 {get; set; } = "SCP-173";
        public string GameRoleScp939 {get; set; } = "SCP-939";
    }
}
