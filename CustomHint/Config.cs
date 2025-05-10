using System.Collections.Generic;
using System.ComponentModel;
using Exiled.API.Interfaces;
using PlayerRoles;

namespace CustomHintPlugin
{
    public class Config : IConfig
    {
        [Description("Plugin enabled (bool)?")]
        public bool IsEnabled { get; set; } = true;

        [Description("Debug mode (bool)?")]
        public bool Debug { get; set; } = true;

        [Description("Hint message for rounds lasting up to 59 seconds.")]
        public string HintMessageUnderMinute { get; set; } = "Quick start! {player_nickname}, round time: {round_duration_seconds}s.\nRole: {player_role}\nTPS: {tps}/60";

        [Description("Hint message for rounds lasting from 1 minute to 59 minutes and 59 seconds.")]
        public string HintMessageUnderHour { get; set; } = "Still going, {player_nickname}! Time: {round_duration_minutes}:{round_duration_seconds}.\nRole: {player_role}\nTPS: {tps}/60";

        [Description("Hint message for rounds lasting 1 hour or more.")]
        public string HintMessageOverHour { get; set; } = "Long run, {player_nickname}! Duration: {round_duration_hours}:{round_duration_minutes}:{round_duration_seconds}.\nRole: {player_role}\nTPS: {tps}/60";

        [Description("Default role name for players without a custom role.")]
        public string DefaultRoleName { get; set; } = "Player";

        [Description("Default role color (for players without custom roles).")]
        public string DefaultRoleColor { get; set; } = "white";

        [Description("Ignored roles.")]
        public List<RoleTypeId> ExcludedRoles { get; set; } = new List<RoleTypeId>
        {
            RoleTypeId.Overwatch,
            RoleTypeId.Spectator,
            RoleTypeId.Filmmaker,
            RoleTypeId.Scp079
        };

        [Description("Enable or disable HUD-related commands.")]
        public bool EnableHudCommands { get; set; } = true;

        [Description("Message displayed when the HUD is successfully hidden.")]
        public string HideHudSuccessMessage { get; set; } = "<color=green>You have successfully hidden the server HUD! To get the HUD back, use .showhud</color>";

        [Description("Message displayed when the HUD is already hidden.")]
        public string HideHudAlreadyHiddenMessage { get; set; } = "<color=red>You've already hidden the HUD server.</color>";

        [Description("Message displayed when the HUD is successfully shown.")]
        public string ShowHudSuccessMessage { get; set; } = "<color=green>You have successfully returned the server HUD! To hide again, use .hidehud</color>";

        [Description("Message displayed when the HUD is already shown.")]
        public string ShowHudAlreadyShownMessage { get; set; } = "<color=red>You already have the server HUD displayed.</color>";

        [Description("Message displayed when DNT (Do Not Track) mode is enabled.")]
        public string DntEnabledMessage { get; set; } = "<color=red>Disable DNT (Do Not Track) mode.</color>";

        [Description("Message displayed when commands are disabled on the server.")]
        public string CommandDisabledMessage { get; set; } = "<color=red>This command is disabled on the server.</color>";

        [Description("Message displayed when the hint system is toggled on.")]
        public string ToggleOnMessage { get; set; } = "<color=green>Hint system and commands are now enabled for this round.</color>";

        [Description("Message displayed when the hint system is toggled off.")]
        public string ToggleOffMessage { get; set; } = "<color=red>Hint system and commands are now disabled for this round.</color>";

        [Description("Message displayed when commands are disabled for the round.")]
        public string CommandsDisabledMessage { get; set; } = "<color=red>Commands .showhud and .hidehud are disabled for this round.</color>";
    }
}
