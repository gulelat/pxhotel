using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Enums;
using PX.Core.Ultilities;

namespace PX.Business.Models.HotelRooms
{
    public class HotelRoomModel : BaseModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Note { get; set; }

        public int Status { get; set; }

        public string StatusName
        {
            get
            {
                return EnumUtilities.GetName((HotelRoomEnums.StatusEnums)Status);
            }
            set
            {
                Status = value.ToInt();
            }
        }

        public int HotelRoomTypeId { get; set; }

        public string HotelRoomTypeName { get; set; }
    }
}
