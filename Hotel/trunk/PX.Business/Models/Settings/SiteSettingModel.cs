using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.Settings
{
    public class SiteSettingModel : BaseModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
