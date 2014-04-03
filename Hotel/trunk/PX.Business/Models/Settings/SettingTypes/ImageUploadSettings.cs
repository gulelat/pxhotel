using PX.Business.Models.Settings.SettingTypes.Base;
using PX.Business.Mvc.Environments;
using PX.Business.Services.Settings;
using PX.EntityModel;

namespace PX.Business.Models.Settings.SettingTypes
{
    public class ImageUploadSettingResolver : SettingParser<ImageUploadSetting>, ISettingModel<ImageUploadSetting>
    {
        public string GetSettingName()
        {
            return "ImageUploadSetting";
        }

        public ImageUploadSetting LoadSetting()
        {
            var settingServices = HostContainer.GetInstance<ISettingServices>();
            var settingString = settingServices.GetSetting<string>(GetSettingName());
            if (string.IsNullOrEmpty(settingString))
            {
                var setting = new ImageUploadSetting();
                settingString = SerializeSetting(setting);
                settingServices.Insert(new SiteSetting
                {
                    Name = GetSettingName(),
                    Value = settingString,
                    RecordOrder = 0,
                    RecordActive = true
                });
                return setting;
            }
            return DeserializeSetting(settingString);
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
