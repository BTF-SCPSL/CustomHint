using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;

namespace CustomHint.Configs
{
    public class Config : IConfig
    {
        [Description("Plugin enabled (bool)?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Debug mode?")]
        public bool Debug { get; set; } = false;

        [Description("Provide data for plugin statistics (data will not be shared with third parties).")]
        public bool SendAnonInfo { get; set; } = true;

        [Description("Enable or disable the HUD settings in the game menu.")]
        public bool HudSettings { get; set; } = true;

        [Description("Menu Id in Server Specific Settings.")]
        public int SettingsMenuId { get; set; } = -77;

        [Description("Setting Id in Menu.")]
        public int SettingId { get; set; } = 77;

        [Description("Enable or disable automatic plugin updates.")]
        public bool AutoUpdater { get; set; } = true;

        [Description("The interval for changing {hints} placeholder (in seconds).")]
        public float HintMessageTime { get; set; } = 5f;

        [Description("Default role name for players without a role.")]
        public string DefaultRoleName { get; set; } = "Player";

        [Description("Default role color (for players without roles).")]
        public string DefaultRoleColor { get; set; } = "white";

        [Description("Server timezone for placeholder. Use 'UTC' by default or a valid timezone ID (e.g., 'Europe/Kyiv').")]
        public string ServerTimeZone { get; set; } = "UTC";

        [Description("Enable counting Overwatch players in placeholder {spectators_num}.")]
        public bool EnableOverwatchCounting { get; set; } = true;

        [Description("Sync speed for hints. Available values: UnSync, Slowest, Slow, Normal, Fast, Fastest.")]
        public string SyncSpeed { get; set; } = "Fastest";

        [Description("List of hints.")]
        public List<HintConfig> Hints { get; set; } = new()
        {
            new HintConfig
            {
                Id = "firsthint",
                Text = "Hello World!",
                FontSize = 15,
                PositionX = 500,
                PositionY = 500,
                CanBeHidden = true,
                Roles = new List<RoleTypeId> { RoleTypeId.ClassD }
            }
        };
    }

    public class HintConfig
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public float FontSize { get; set; }
        public float PositionX { get; set; }
        public float PositionY { get; set; }
        public bool CanBeHidden { get; set; }
        public List<RoleTypeId> Roles { get; set; } = new();
    }
}