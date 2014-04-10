using System;
using System.Collections.Specialized;
using PX.Business.Models.Settings.SettingTypes.Base;
using PX.Business.Services.Settings;
using PX.Core.Framework.Mvc.Environments;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Models.Settings.SettingTypes
{
    public class ImageUploadSettingResolver : SettingParser<ImageUploadSetting>, ISettingModel
    {
        public Type SettingType
        {
            get { return typeof(ImageUploadSetting); }
        }

        public string SettingName
        {
            get { return "ImageUploadSetting"; }
        }

        public dynamic LoadSetting()
        {
            var settingServices = HostContainer.GetInstance<ISettingServices>();
            var settingString = settingServices.GetSetting<string>(SettingName);
            if (string.IsNullOrEmpty(settingString))
            {
                var setting = new ImageUploadSetting();
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

        public string GetSettingValue(NameValueCollection data)
        {
            var imageUploadSetting = new ImageUploadSetting
            {
                MinWidth = Convert.ToInt32(data["MinWidth"]),
                MinHeight =  Convert.ToInt32(data["MinHeight"]),
                MaxWidth =  Convert.ToInt32(data["MaxWidth"]),
                MaxHeight =  Convert.ToInt32(data["MaxHeight"])
            };
            return SerializeSetting(imageUploadSetting);
        }
    }

    public class ImageUploadSetting
    {
        public ImageUploadSetting()
        {
            MinWidth = null;
            MinHeight = null;
            MaxWidth = null;
            MaxHeight = null;
        }

        public int? MinWidth { get; set; }
        public int? MinHeight { get; set; }
        public int? MaxWidth { get; set; }
        public int? MaxHeight { get; set; }
    }
}
