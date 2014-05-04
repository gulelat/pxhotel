using System.Collections.Generic;
using PX.Business.Models.HotelRoomTypes.ViewModels;

namespace PX.Business.Models.HotelBookings.ViewModels
{
    public class CalendarViewModel
    {
        #region Public Properties

        public List<BookingViewModel> Bookings { get; set; }

        public List<RoomEventViewModel> HotelRoomTypes { get; set; }

        #endregion
    }
}
