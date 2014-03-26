using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.SettingTypes
{
    public class SettingTypeModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

    }
}
