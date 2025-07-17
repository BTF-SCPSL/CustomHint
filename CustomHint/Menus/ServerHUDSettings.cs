using Exiled.API.Features;
using Exiled.API.Features.Core.UserSettings;

namespace CustomHint.Menus
{
    public static class ServerHUDSettings
    {
        public static HeaderSetting HudHeaderText { get; private set; }
        public static TwoButtonsSetting ShowHudSetting { get; private set; }

        public static void Register()
        {
            HudHeaderText = new HeaderSetting(
                name: Plugin.Instance.Translation.HeaderText
            );

            ShowHudSetting = new TwoButtonsSetting(
                id: Plugin.Instance.Config.SettingId,
                label: Plugin.Instance.Translation.ButtonName,
                firstOption: Plugin.Instance.Translation.ButtonEnable,
                secondOption: Plugin.Instance.Translation.ButtonDisable,
                defaultIsSecond: false,
                hintDescription: Plugin.Instance.Translation.ButtonHint,
                onChanged: (player, setting) =>
                {
                    bool hudEnabled = (setting as TwoButtonsSetting)?.IsFirst ?? true;
                    player.SessionVariables["HudEnabled"] = hudEnabled;

                    Log.Debug($"{player.Nickname} changed HUD: {(hudEnabled ? "ENABLE" : "DISABLE")}");

                    if (hudEnabled)
                    {
                        Plugin.Instance.Hints.RemoveHints(player);
                        Plugin.Instance.Hints.AssignHints(player);
                    }
                    else
                    {
                        Plugin.Instance.Hints.RemoveHints(player);
                    }
                });

            SettingBase.Register(new[] { ShowHudSetting });
        }

        public static void Unregister()
        {
            if (ShowHudSetting != null)
                SettingBase.Unregister(player => true, new[] { ShowHudSetting });
        }

        public static bool IsHudEnabled(Player player) =>
            !(player.SessionVariables.TryGetValue("HudEnabled", out var value) && value is bool enabled && !enabled);
    }
}