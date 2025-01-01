using MEC;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Server;

namespace CustomHint.Handlers
{
    public class RoundEvents
    {
        private CoroutineHandle _hintCoroutine;
        private bool _isRoundActive;

        public bool IsRoundActive
        {
            get { return _isRoundActive; }
        }

        public void OnWaitingForPlayers()
        {
            Log.Debug("Waiting for players, enabling DisplayHintWFP.");
            _isRoundActive = false;

            Timing.KillCoroutines(_hintCoroutine);
            Plugin.Instance.SaveHiddenHudPlayers();
        }

        public void OnRoundStarted()
        {
            Log.Debug("Round started, enabling hints.");
            _isRoundActive = true;

            Plugin.Instance.Hints.LoadHints();
            Plugin.Instance.Hints.StartHintUpdater();
            _hintCoroutine = Timing.RunCoroutine(Plugin.Instance.Hints.ContinuousHintDisplay());
        }

        public void OnRoundEnded(RoundEndedEventArgs ev)
        {
            Log.Debug("Round ended, disabling hints.");
            _isRoundActive = false;

            Timing.KillCoroutines(_hintCoroutine);
            Plugin.Instance.Hints.StopHintUpdater();
        }
    }
}