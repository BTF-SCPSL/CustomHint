using System;
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
                response = Plugin.Instance.Translation.CommandDisabledMessage;
                return false;
            }

            if (player == null)
            {
                response = "This command is for players only.";
                return false;
            }

            if (player.DoNotTrack)
            {
                response = Plugin.Instance.Translation.DntEnabledMessage;
                return false;
            }

            if (Plugin.Instance.HiddenHudPlayers.Contains(player.UserId))
            {
                response = Plugin.Instance.Translation.HideHudAlreadyHiddenMessage;
                return false;
            }

            Plugin.Instance.HiddenHudPlayers.Add(player.UserId);
            Plugin.Instance.SaveHiddenHudPlayers();
            response = Plugin.Instance.Translation.HideHudSuccessMessage;
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
            if (!Plugin.Instance.Config.EnableHudCommands)
            {
                response = Plugin.Instance.Translation.CommandDisabledMessage;
                return false;
            }

            if (sender is PlayerCommandSender playerSender)
            {
                var player = Player.Get(playerSender.ReferenceHub);

                if (player == null || player.DoNotTrack)
                {
                    response = Plugin.Instance.Translation.DntEnabledMessage;
                    return false;
                }

                if (!Plugin.Instance.HiddenHudPlayers.Contains(player.UserId))
                {
                    response = Plugin.Instance.Translation.ShowHudAlreadyShownMessage;
                    return false;
                }

                Plugin.Instance.HiddenHudPlayers.Remove(player.UserId);
                Plugin.Instance.SaveHiddenHudPlayers();
                response = Plugin.Instance.Translation.ShowHudSuccessMessage;
                return true;
            }

            response = "This command is for players only.";
            return false;
        }
    }
}