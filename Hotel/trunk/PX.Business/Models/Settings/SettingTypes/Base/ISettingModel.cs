using System;
using System.Collections.Specialized;

namespace PX.Business.Models.Settings.SettingTypes.Base
{
    public interface ISettingModel
    {
        Type SettingType { get; }

        string SettingName { get; }

        dynamic LoadSetting();

        string GetSettingValue(NameValueCollection data);
    }
}
