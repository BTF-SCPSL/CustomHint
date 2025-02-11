using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;

namespace CustomHint.Configs
{
    public class Translation : ITranslation
    {
        [Description("Message displayed when the HUD is successfully hidden.")]
        public string HideHudSuccessMessage { get; set; } = "<color=green>You have successfully hidden the server HUD! To get the HUD back, use .showhud.</color>";

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

        [Description("Round time.")]
        public Dictionary<string, string> RoundTimeFormats { get; set; } = new()
        {
            { "seconds", "{round_duration_seconds} seconds" },
            { "minutes", "{round_duration_minutes} minutes {round_duration_seconds} seconds" },
            { "hours", "{round_duration_hours} hours {round_duration_minutes} minutes {round_duration_seconds} seconds" }
        };

        [Description("Game Role of a player, {player_gamerole} is placeholder.")]
        public List<RoleName> GameRoles { get; set; } = new()
        {
            new RoleName { Role = RoleTypeId.Tutorial, Name = "Tutorial" },
            new RoleName { Role = RoleTypeId.ClassD, Name = "Class-D" },
            new RoleName { Role = RoleTypeId.Scientist, Name = "Scientist" },
            new RoleName { Role = RoleTypeId.FacilityGuard, Name = "Facility Guard" },
            new RoleName { Role = RoleTypeId.Filmmaker, Name = "Film Maker" },
            new RoleName { Role = RoleTypeId.Overwatch, Name = "Overwatch" },
            new RoleName { Role = RoleTypeId.NtfPrivate, Name = "MTF Private" },
            new RoleName { Role = RoleTypeId.NtfSergeant, Name = "MTF Sergeant" },
            new RoleName { Role = RoleTypeId.NtfSpecialist, Name = "MTF Specialist" },
            new RoleName { Role = RoleTypeId.NtfCaptain, Name = "MTF Captain" },
            new RoleName { Role = RoleTypeId.ChaosConscript, Name = "CI Conscript" },
            new RoleName { Role = RoleTypeId.ChaosRifleman, Name = "CI Rifleman" },
            new RoleName { Role = RoleTypeId.ChaosRepressor, Name = "CI Repressor" },
            new RoleName { Role = RoleTypeId.ChaosMarauder, Name = "CI Marauder" },
            new RoleName { Role = RoleTypeId.Scp049, Name = "SCP-049" },
            new RoleName { Role = RoleTypeId.Scp0492, Name = "SCP-049-2" },
            new RoleName { Role = RoleTypeId.Scp079, Name = "SCP-079" },
            new RoleName { Role = RoleTypeId.Scp096, Name = "SCP-096" },
            new RoleName { Role = RoleTypeId.Scp106, Name = "SCP-106" },
            new RoleName { Role = RoleTypeId.Scp173, Name = "SCP-173" },
            new RoleName { Role = RoleTypeId.Scp939, Name = "SCP-939" }
        };
    }

    public class RoleName
    {
        public RoleTypeId Role { get; set; }
        public string Name { get; internal set; }
    }
}
