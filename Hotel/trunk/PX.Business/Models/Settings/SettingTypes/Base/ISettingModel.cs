namespace PX.Business.Models.Settings.SettingTypes.Base
{
    public interface ISettingModel<out T> where T: class
    {
        string SettingName { get; }

        T LoadSetting();
    }
}
