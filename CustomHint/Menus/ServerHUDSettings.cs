using CustomHint;
using SSMenuSystem.Features;
using UserSettings.ServerSpecific;

public class ServerHUDSettings : Menu
{
    public override string Name { get; set; } = Plugin.Instance.Translation.HeaderText;
    public override int Id { get; set; } = Plugin.Instance.Config.SettingsMenuId;

    public override ServerSpecificSettingBase[] Settings => new ServerSpecificSettingBase[]
    {
        new SSTwoButtonsSetting(
            id: Plugin.Instance.Config.SettingId,
            label: Plugin.Instance.Translation.ButtonName,
            hint: Plugin.Instance.Translation.ButtonHint,
            optionA: Plugin.Instance.Translation.ButtonEnable,
            optionB: Plugin.Instance.Translation.ButtonDisable
         )
    };
}