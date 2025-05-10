using CommandSystem;
using Exiled.API.Features;
using RemoteAdmin;
using System;

namespace CustomHintPlugin
{
    [CommandHandler(typeof(ClientCommandHandler))]
    public class HideHudCommand : ICommand
    {
        public string Command => "hidehud";
        public string[] Aliases => Array.Empty<string>();
        public string Description => "Hides the server HUD.";

        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (!CustomHintPlugin.Instance.Config.EnableHudCommands)
            {
                response = CustomHintPlugin.Instance.Config.CommandDisabledMessage;
                return false;
            }

            if (sender is PlayerCommandSender playerSender)
            {
                var player = Player.Get(playerSender.ReferenceHub);

                if (player == null || player.DoNotTrack)
                {
                    response = CustomHintPlugin.Instance.Config.DntEnabledMessage;
                    return false;
                }

                if (CustomHintPlugin.Instance.HiddenHudPlayers.Contains(player.UserId))
                {
                    response = CustomHintPlugin.Instance.Config.HideHudAlreadyHiddenMessage;
                    return false;
                }

                CustomHintPlugin.Instance.HiddenHudPlayers.Add(player.UserId);
                CustomHintPlugin.Instance.SaveHiddenHudPlayers();
                response = CustomHintPlugin.Instance.Config.HideHudSuccessMessage;
                return true;
            }

            response = "This command is for players only.";
            return false;
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
            if (!CustomHintPlugin.Instance.Config.EnableHudCommands)
            {
                response = CustomHintPlugin.Instance.Config.CommandDisabledMessage;
                return false;
            }

            if (sender is PlayerCommandSender playerSender)
            {
                var player = Player.Get(playerSender.ReferenceHub);

                if (player == null || player.DoNotTrack)
                {
                    response = CustomHintPlugin.Instance.Config.DntEnabledMessage;
                    return false;
                }

                if (!CustomHintPlugin.Instance.HiddenHudPlayers.Contains(player.UserId))
                {
                    response = CustomHintPlugin.Instance.Config.ShowHudAlreadyShownMessage;
                    return false;
                }

                CustomHintPlugin.Instance.HiddenHudPlayers.Remove(player.UserId);
                CustomHintPlugin.Instance.SaveHiddenHudPlayers();
                response = CustomHintPlugin.Instance.Config.ShowHudSuccessMessage;
                return true;
            }

            response = "This command is for players only.";
            return false;
        }
    }
}
