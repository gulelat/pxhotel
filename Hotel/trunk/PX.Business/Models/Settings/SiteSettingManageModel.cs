using System.Collections.Generic;
using System.Web.Mvc;

namespace PX.Business.Models.Settings
{
    public class SiteSettingManageModel
    {
        public int Id { get; set; }

        public string SettingName { get; set; }

        public dynamic Setting { get; set; }

        public int SettingTypeId { get; set; }

        public IEnumerable<SelectListItem> SettingTypes { get; set; }
    }
}
