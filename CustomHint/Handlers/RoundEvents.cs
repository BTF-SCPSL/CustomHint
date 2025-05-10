using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;
using System.Threading.Tasks;

namespace CustomHint.Handlers
{
    public class RoundEvents
    {
        private bool _isRoundActive;

        public bool IsRoundActive
        {
            get { return _isRoundActive; }
        }

        public void OnWaitingForPlayers()
        {
            Log.Debug("Waiting for players...");
            _isRoundActive = false;

            Task.Run(() => Plugin.Instance.autoUpdater.CheckForUpdates());
        }

        public void OnRoundStarted()
        {
            Log.Debug("Round started.");
            _isRoundActive = true;

            Plugin.Instance.Hints.LoadHints();
            Plugin.Instance.Hints.StartHintUpdater();

            foreach (var player in Player.List)
            {
                Plugin.Instance.Hints.RemoveHints(player);
                Plugin.Instance.Hints.AssignHints(player);
            }
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Log.Debug("Round ended.");
            _isRoundActive = false;

            Plugin.Instance.Hints.StopHintUpdater();

            foreach (var player in Player.List)
            {
                Plugin.Instance.Hints.RemoveHints(player);
            }
        }
    }
}