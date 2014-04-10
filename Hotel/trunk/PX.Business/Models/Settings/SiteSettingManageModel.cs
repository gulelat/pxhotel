namespace PX.Business.Models.Settings
{
    public class SiteSettingManageModel
    {
        public int Id { get; set; }

        public string SettingName { get; set; }

        public dynamic Setting { get; set; }
    }
}
