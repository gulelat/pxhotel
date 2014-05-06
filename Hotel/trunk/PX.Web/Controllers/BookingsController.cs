using System;
using System.Web.Mvc;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.HotelBookings;

namespace PX.Web.Controllers
{
    public class BookingsController : ClientController
    {
        private readonly IHotelBookingServices _hotelBookingServices;
        public BookingsController(IHotelBookingServices hotelBookingServices)
        {
            _hotelBookingServices = hotelBookingServices;
        }

        //
        // GET: /Booking/
        public ActionResult Index(DateTime from, DateTime to)
        {
            var model = _hotelBookingServices.GetBooking(from, to);
            return View(model);
        }

        public ActionResult BookingDetails()
        {
            return View();
        }
    }
}
