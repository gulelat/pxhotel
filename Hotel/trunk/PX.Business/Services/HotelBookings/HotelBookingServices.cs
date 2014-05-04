using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using PX.Business.Models.HotelBookings;
using PX.Business.Models.HotelBookings.ViewModels;
using PX.Business.Models.HotelRoomTypes.ViewModels;
using PX.Business.Models.HotelServices.ViewModels;
using PX.Business.Services.HotelRoomTypes;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.HotelBookings
{
    public class HotelBookingServices : IHotelBookingServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly IHotelRoomTypeServices _hotelRoomTypeServices;
        private readonly HotelBookingRepository _hotelBookingRepository;
        private readonly HotelBookingRoomRepository _hotelBookingroomRepository;
        public HotelBookingServices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _hotelRoomTypeServices = HostContainer.GetInstance<IHotelRoomTypeServices>();
            _hotelBookingRepository = new HotelBookingRepository(entities);
            _hotelBookingroomRepository = new HotelBookingRoomRepository(entities);
        }

        #region Base
        public IQueryable<HotelBooking> GetAll()
        {
            return _hotelBookingRepository.GetAll();
        }
        public IQueryable<HotelBooking> Fetch(Expression<Func<HotelBooking, bool>> expression)
        {
            return _hotelBookingRepository.Fetch(expression);
        }
        public HotelBooking FetchFirst(Expression<Func<HotelBooking, bool>> expression)
        {
            return _hotelBookingRepository.FetchFirst(expression);
        }
        public HotelBooking GetById(object id)
        {
            return _hotelBookingRepository.GetById(id);
        }
        public ResponseModel Insert(HotelBooking hotelBooking)
        {
            return _hotelBookingRepository.Insert(hotelBooking);
        }
        public ResponseModel Update(HotelBooking hotelBooking)
        {
            return _hotelBookingRepository.Update(hotelBooking);
        }
        public ResponseModel Delete(HotelBooking hotelBooking)
        {
            return _hotelBookingRepository.Delete(hotelBooking);
        }
        public ResponseModel Delete(object id)
        {
            return _hotelBookingRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _hotelBookingRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search
        /// <summary>
        /// search the HotelBookings.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchHotelBookings(JqSearchIn si)
        {
            var hotelBookings = GetAll().Select(u => new HotelBookingModel
            {
                Id = u.Id,
                TotalMoney = u.TotalMoney,
                Note = u.Note,
                Status = u.Status,
                HotelCustomerId = u.HotelCustomerId,
                HotelCustomerName = u.HotelCustomer.FullName,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(hotelBookings);
        }

        #endregion

        #region Manage Grid

        /// <summary>
        /// Manage Site HotelBooking
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the HotelBooking model</param>
        /// <returns></returns>
        public ResponseModel ManageHotelBooking(GridOperationEnums operation, HotelBookingModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<HotelBookingModel, HotelBooking>();
            HotelBooking hotelBooking;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    hotelBooking = GetById(model.Id);
                    hotelBooking.TotalMoney = model.TotalMoney;
                    hotelBooking.Note = model.Note;
                    hotelBooking.Status = model.Status;

                    response = Update(hotelBooking);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelBookings:::Messages:::UpdateSuccessfully:::Update booking successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelBookings:::Messages:::UpdateFailure:::Update booking failed. Please try again later."));

                case GridOperationEnums.Add:
                    hotelBooking = Mapper.Map<HotelBookingModel, HotelBooking>(model);
                    hotelBooking.Status = model.Status;

                    response = Insert(hotelBooking);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelBookings:::Messages:::CreateSuccessfully:::Create booking successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelBookings:::Messages:::CreateFailure:::Insert booking failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelBookings:::Messages:::DeleteSuccessfully:::Delete booking successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelBookings:::Messages:::DeleteFailure:::Delete booking failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::HotelBookings:::Messages:::ObjectNotFounded:::Booking is not founded.")
            };
        }

        #endregion

        /// <summary>
        /// Get news status
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetStatus()
        {
            return EnumUtilities.GetSelectListFromEnum<HotelBookingEnums.StatusEnums>();
        }

        /// <summary>
        /// Get booking view model from date
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public HotelBookingViewModel GetBooking(DateTime from, DateTime to)
        {
            return new HotelBookingViewModel
                {
                    DateFrom = from,
                    DateTo = to,
                    TotalDays = (int)(to - from).TotalDays,
                    HotelRoomTypes = _hotelRoomTypeServices
                        .Fetch(r => r.RecordActive)
                        .OrderBy(r => r.RecordOrder).ToList()
                        .Select(r => new HotelRoomTypeViewModel
                            {
                                Id = r.Id,
                                ImageUrl =
                                    r.HotelRoomImages.Any()
                                        ? (r.HotelRoomImages.Any(i => i.IsDefaultImage)
                                               ? r.HotelRoomImages.First(i => i.IsDefaultImage).ImageUrl
                                               : r.HotelRoomImages.First().ImageUrl)
                                        : string.Empty,
                                Description = r.Description,
                                MoreInformation = r.MoreInformation,
                                Price = r.Price,
                                Name = r.Name,
                                RecordOrder = r.RecordOrder,
                                TotalRooms = r.TotalRooms,
                                AvailableRooms =
                                    r.TotalRooms -
                                    r.HotelBookingRooms.Count(
                                        b =>
                                        (b.DateFrom >= from || b.DateTo <= to) &&
                                        b.Status == (int)HotelBookingEnums.StatusEnums.Accepted),
                                HotelServices =
                                    r.HotelRoomServices.OrderBy(s => s.RecordOrder).Select(
                                        s => new HotelServiceViewModel
                                            {
                                                Id = s.HotelService.Id,
                                                Name = s.HotelService.Name,
                                                RecordOrder = s.HotelService.RecordOrder,
                                                ServiceIcon = s.HotelService.ServiceIcon
                                            }).ToList()
                            }).ToList()
                };
        }

        public List<BookingViewModel> GetBookings()
        {
            return _hotelBookingroomRepository.GetAll().Select(b => new BookingViewModel
                {
                    Id = b.Id,
                    DateFrom = b.DateFrom,
                    DateTo = b.DateTo,
                    Note = b.HotelBooking.Note,
                    Status = b.Status,
                    RoomTypeId = b.HotelRoomTypeId,
                    TotalBookingRooms = b.TotalBookingRooms
                }).ToList();
        }

        /// <summary>
        /// Get booking model for calendar
        /// </summary>
        /// <returns></returns>
        public CalendarViewModel GetBookingCalendar()
        {
            var bookings = GetBookings();
            return new CalendarViewModel
                {
                    Bookings = bookings,
                    HotelRoomTypes = _hotelRoomTypeServices.GetRoomEvents()
                };
        }
    }
}
