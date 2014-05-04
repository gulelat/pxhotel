using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.HotelServices
{
    public class HotelServiceModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string ServiceIcon { get; set; }
    }
}
