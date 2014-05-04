using PX.Core.Framework.Enums;
using PX.Core.Ultilities;

namespace PX.Business.Models.HotelBookings
{
    public class HotelBookingModel : BaseModel
    {
        public int Id { get; set; }

        public double TotalMoney { get; set; }

        public int Status { get; set; }

        public string StatusName
        {
            get
            {
                return EnumUtilities.GetName((HotelBookingEnums.StatusEnums)Status);
            }
            set
            {
                Status = value.ToInt();
            }
        }

        public string Note { get; set; }

        public int HotelCustomerId { get; set; }

        public string HotelCustomerName { get; set; }
    }
}
