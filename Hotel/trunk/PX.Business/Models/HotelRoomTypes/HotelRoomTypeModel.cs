using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.HotelRoomTypes
{
    public class HotelRoomTypeModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public double Price { get; set; }

    }
}
