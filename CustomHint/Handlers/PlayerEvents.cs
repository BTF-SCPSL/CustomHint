using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;

namespace CustomHint.Handlers
{
    public class PlayerEvents
    {
        public void OnPlayerVerified(VerifiedEventArgs ev)
        {
            Player player = ev.Player;

            if (player.DoNotTrack)
            {
                if (!Plugin.Instance.HiddenHudPlayers.Remove(player.UserId))
                    return;

                Plugin.Instance.SaveHiddenHudPlayers();
                Log.Debug($"Player {player.Nickname} ({player.UserId}) has DNT enabled and was removed from HiddenHudPlayers.");
            }
        }
    }
}