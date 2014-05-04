using System.Collections.Generic;
using PX.Business.Models.HotelServices.ViewModels;

namespace PX.Business.Models.HotelRoomTypes.ViewModels
{
    public class HotelRoomTypeViewModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public int TotalRooms { get; set; }

        public int AvailableRooms { get; set; }

        public string Description { get; set; }

        public string MoreInformation { get; set; }

        public int RecordOrder { get; set; }

        public List<HotelServiceViewModel> HotelServices { get; set; } 
    }
}
