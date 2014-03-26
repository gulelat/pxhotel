using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.Settings
{
    public class SiteSettingModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        public string SettingTypeName { get; set; }

        public int SettingTypeId { get; set; }
    }
}
