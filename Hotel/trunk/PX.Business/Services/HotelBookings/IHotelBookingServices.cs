using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.HotelBookings;
using PX.Business.Models.HotelBookings.ViewModels;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.HotelBookings
{
    public interface IHotelBookingServices
    {
        #region Base

        IQueryable<HotelBooking> GetAll();
        IQueryable<HotelBooking> Fetch(Expression<Func<HotelBooking, bool>> expression);
        HotelBooking FetchFirst(Expression<Func<HotelBooking, bool>> expression);
        HotelBooking GetById(object id);
        ResponseModel Insert(HotelBooking hotelBooking);
        ResponseModel Update(HotelBooking hotelBooking);
        ResponseModel Delete(HotelBooking hotelBooking);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchHotelBookings(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageHotelBooking(GridOperationEnums operation, HotelBookingModel model);

        #endregion

        #region Bookings

        #endregion

        IEnumerable<SelectListItem> GetStatus();

        HotelBookingViewModel GetBooking(DateTime from, DateTime to);

        CalendarViewModel GetBookingCalendar();
    }
}
