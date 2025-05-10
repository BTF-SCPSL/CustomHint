using CustomHint;
using SSMenuSystem.Features;
using UserSettings.ServerSpecific;

public class ServerHUDSettings : Menu
{
    public override string Name { get; set; } = Plugin.Instance.Translation.HeaderText;
    public override int Id { get; set; } = -20;

    public override ServerSpecificSettingBase[] Settings => new ServerSpecificSettingBase[]
    {
        new SSTwoButtonsSetting(
            id: 1,
            label: Plugin.Instance.Translation.ButtonName,
            hint: Plugin.Instance.Translation.ButtonHint,
            optionA: Plugin.Instance.Translation.ButtonEnable,
            optionB: Plugin.Instance.Translation.ButtonDisable
         )
    };
}