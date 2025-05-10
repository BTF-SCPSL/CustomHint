using System.Collections.Generic;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using MEC;
using SSMenuSystem.Features;
using UserSettings.ServerSpecific;

namespace CustomHint.Handlers
{
    public class PlayerEvents
    {
        private readonly Dictionary<string, bool> _playerHudState = new();

        public void OnPlayerVerified(VerifiedEventArgs ev)
        {
            Player player = ev.Player;
            Plugin.Instance.Hints.AssignHints(player);
        }

        public void OnPlayerSpawned(SpawnedEventArgs ev)
        {
            Plugin.Instance.Hints.AssignHints(ev.Player);
        }

        public void OnChangingRole(ChangingRoleEventArgs ev)
        {
            Player player = ev.Player;

            if (player == null)
            {
                Log.Warn("OnChangingRole: Player is null, skipping hint update.");
                return;
            }

            Log.Debug($"Player {player.Nickname} ({player.UserId}) is changing role to {ev.NewRole}. Removing old hints...");

            Plugin.Instance.Hints.RemoveHints(player);

            Timing.CallDelayed(0.5f, () =>
            {
                if (player.Role.Type == ev.NewRole)
                {
                    Log.Debug($"Assigning new hints to {player.Nickname} ({player.UserId}) after role change...");
                    Plugin.Instance.Hints.AssignHints(player);
                }
                else
                {
                    Log.Warn($"Role mismatch detected for {player.Nickname}. Expected {ev.NewRole}, but got {player.Role.Type}. Skipping hint assignment.");
                }
            });
        }

        public void OnUpdate()
        {
            foreach (var player in Player.List)
            {
                if (!player.IsVerified)
                    continue;

                var setting = player.ReferenceHub.GetParameter<ServerHUDSettings, SSTwoButtonsSetting>(1);
                if (setting == null) continue;

                bool isHudEnabled = setting.SyncIsA;

                if (!_playerHudState.TryGetValue(player.UserId, out bool lastState) || lastState != isHudEnabled)
                {
                    _playerHudState[player.UserId] = isHudEnabled;
                    Log.Debug($"{player.Nickname} changed HUD: {(isHudEnabled ? "ENABLE" : "DISABLE")}");

                    if (isHudEnabled)
                    {
                        Plugin.Instance.Hints.RemoveHints(player);
                        Plugin.Instance.Hints.AssignHints(player);
                    }
                    else
                    {
                        Plugin.Instance.Hints.RemoveHints(player);
                    }
                }
            }
        }
    }
}