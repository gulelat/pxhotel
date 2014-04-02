using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.RotatingImageGroups
{
    public class RotatingImageGroupModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Settings { get; set; }
    }
}
