using System;
using System.Collections.Generic;
using PX.Business.Models.HotelRoomTypes.ViewModels;

namespace PX.Business.Models.HotelBookings.ViewModels
{
    public class HotelBookingViewModel
    {
        #region Public Properties

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public int TotalDays { get; set; }

        public List<HotelRoomTypeViewModel>  HotelRoomTypes { get; set; }

        #endregion
    }
}
