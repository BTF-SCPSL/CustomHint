using CommandSystem;
using Exiled.API.Features;
using System;

namespace CustomHintPlugin
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    public class ChToggleCommand : ICommand
    {
        public string Command => "ch toggle";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Toggles the hint system and related commands on/off for the current round.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (sender is CommandSender)
            {
                var plugin = CustomHintPlugin.Instance;

                plugin.IsHintSystemEnabled = !plugin.IsHintSystemEnabled;

                response = plugin.IsHintSystemEnabled
                    ? plugin.Config.ToggleOnMessage
                    : plugin.Config.ToggleOffMessage;

                return true;
            }

            response = "This command can only be executed by RemoteAdmin or the server console.";
            return false;
        }
    }
}
