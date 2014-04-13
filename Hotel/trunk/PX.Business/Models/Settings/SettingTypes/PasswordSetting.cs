using System;
using System.Collections.Specialized;
using PX.Business.Models.Settings.SettingTypes.Base;
using PX.Business.Services.Settings;
using PX.Core.Framework.Mvc.Environments;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Models.Settings.SettingTypes
{
    public class PasswordSettingResolver : SettingParser<PasswordSetting>, ISettingModel
    {
        public Type SettingType
        {
            get { return typeof(PasswordSetting); }
        }

        public string SettingName
        {
            get { return "PasswordSetting"; }
        }

        public dynamic LoadSetting()
        {
            var settingServices = HostContainer.GetInstance<ISettingServices>();
            var settingString = settingServices.GetSetting<string>(SettingName);
            if (string.IsNullOrEmpty(settingString))
            {
                var setting = new PasswordSetting();
                settingString = SerializeSetting(setting);
                settingServices.Insert(new SiteSetting
                    {
                        Name = SettingName,
                        Value = settingString,
                        RecordOrder = 0,
                        RecordActive = true
                    });
                return setting;
            }
            return DeserializeSetting(settingString);
        }
    }

    public class PasswordSetting
    {
        public PasswordSetting()
        {
            PasswordMinLengthRequired = 8;
            PasswordMustHaveDigit = false;
            PasswordMustHaveSymbol = false;
            PasswordMustHaveUpperAndLowerCaseLetters = false;
        }

        /// <summary>
        /// Specifies the minimum length of password string
        /// </summary>
        public int PasswordMinLengthRequired { get; set; }

        public bool PasswordMustHaveUpperAndLowerCaseLetters { get; set; }

        public bool PasswordMustHaveDigit { get; set; }

        public bool PasswordMustHaveSymbol { get; set; }
    }
}
