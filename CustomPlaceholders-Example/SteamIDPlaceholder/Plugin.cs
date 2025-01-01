using CustomHint.API;
using Exiled.API.Features;
using System;
using System.Linq;

namespace SteamIDPlaceholder
{
    public class SteamIDPlaceholderPlugin : Plugin<Config>
    {
        public override string Name => "SteamIDPlaceholder";
        public override string Author => "Narin";
        public override Version Version => new Version(1, 0, 0);
        public override Version RequiredExiledVersion => new Version(9, 0, 0);

        public override void OnEnabled()
        {
            PlaceholderManager.RegisterPlaceholder("{steamid64}", GetSteamID64Placeholder);
            Log.Debug("Placeholder {steamid64} registered successfully.");
            base.OnEnabled();
        }

        private string GetSteamID64Placeholder()
        {
            Player player = Player.List.FirstOrDefault();
            if (player == null)
                return "No player found";

            return player.DoNotTrack ? "Hidden" : $"{player.UserId.Split('@')[0]}@steam";
        }

        public override void OnDisabled()
        {
            base.OnDisabled();
        }
    }
}