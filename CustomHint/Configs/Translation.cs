using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;

namespace CustomHint.Configs
{
    public class Translation : ITranslation
    {
        [Description("Settings header text.")]
        public string HeaderText { get; set; } = "CustomHint";

        [Description("Name of the item in the settings.")]
        public string ButtonName { get; set; } = "Server HUD display";

        [Description("Buttom hint.")]
        public string ButtonHint { get; set; } = "Enable or disable server HUD display.";

        [Description("Enable button.")]
        public string ButtonEnable { get; set; } = "Enable";

        [Description("Disable button.")]
        public string ButtonDisable { get; set; } = "Disable";

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
            new RoleName { Role = RoleTypeId.Scp939, Name = "SCP-939" },
            new RoleName { Role = RoleTypeId.Scp3114, Name = "SCP-3114" }
        };
    }

    public class RoleName
    {
        public RoleTypeId Role { get; set; }
        public string Name { get; internal set; }
    }
}