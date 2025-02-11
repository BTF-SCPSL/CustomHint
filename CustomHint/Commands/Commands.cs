using System;
using System.Linq;
using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;

namespace CustomHint.Commands
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class HideHudCommand : ICommand
    {
        public string Command => "hidehud";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Hides the server HUD.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var player = Player.Get(sender);

            if (!Plugin.Instance.Config.EnableHudCommands)
            {
                response = "This command is disabled.";
                return false;
            }

            if (player == null)
            {
                response = "This command is for players only.";
                return false;
            }

            if (player.DoNotTrack)
            {
                response = "Disable DNT mode to use this command.";
                return false;
            }

            if (Plugin.Instance.HiddenHudPlayers.Contains(player.UserId))
            {
                response = "HUD is already hidden.";
                return false;
            }

            Plugin.Instance.HiddenHudPlayers.Add(player.UserId);
            Plugin.Instance.SaveHiddenHudPlayers();

            Plugin.Instance.Hints.RemoveHints(player);
            Plugin.Instance.Hints.AssignHints(player);

            response = "HUD successfully hidden.";
            return true;
        }
    }

    [CommandHandler(typeof(ClientCommandHandler))]
    public class ShowHudCommand : ICommand
    {
        public string Command => "showhud";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Shows the server HUD.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var player = Player.Get(sender);

            if (!Plugin.Instance.Config.EnableHudCommands)
            {
                response = "This command is disabled.";
                return false;
            }

            if (player == null)
            {
                response = "This command is for players only.";
                return false;
            }

            if (player.DoNotTrack)
            {
                response = "Disable DNT mode to use this command.";
                return false;
            }

            if (!Plugin.Instance.HiddenHudPlayers.Contains(player.UserId))
            {
                response = "HUD is already visible.";
                return false;
            }

            Plugin.Instance.HiddenHudPlayers.Remove(player.UserId);
            Plugin.Instance.SaveHiddenHudPlayers();

            Plugin.Instance.Hints.RemoveHints(player);
            Plugin.Instance.Hints.AssignHints(player);

            response = "HUD successfully restored.";
            return true;
        }
    }
}
