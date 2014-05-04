using System;

namespace PX.Business.Models.HotelBookings.ViewModels
{
    public class BookingViewModel
    {
        #region Public Properties

        public int Id { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public int RoomTypeId { get; set; }

        public int TotalBookingRooms { get; set; }

        public string Note { get; set; }

        public int Status { get; set; }

        #endregion
    }
}
