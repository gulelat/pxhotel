using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using PX.Business.Models.HotelRooms;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.HotelRooms
{
    public class HotelRoomServices : IHotelRoomServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly HotelRoomRepository _hotelRoomRepository;
        public HotelRoomServices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _hotelRoomRepository = new HotelRoomRepository(entities);
        }

        #region Base
        public IQueryable<HotelRoom> GetAll()
        {
            return _hotelRoomRepository.GetAll();
        }
        public IQueryable<HotelRoom> Fetch(Expression<Func<HotelRoom, bool>> expression)
        {
            return _hotelRoomRepository.Fetch(expression);
        }
        public HotelRoom FetchFirst(Expression<Func<HotelRoom, bool>> expression)
        {
            return _hotelRoomRepository.FetchFirst(expression);
        }
        public HotelRoom GetById(object id)
        {
            return _hotelRoomRepository.GetById(id);
        }
        public ResponseModel Insert(HotelRoom hotelRoom)
        {
            return _hotelRoomRepository.Insert(hotelRoom);
        }
        public ResponseModel Update(HotelRoom hotelRoom)
        {
            return _hotelRoomRepository.Update(hotelRoom);
        }
        public ResponseModel Delete(HotelRoom hotelRoom)
        {
            return _hotelRoomRepository.Delete(hotelRoom);
        }
        public ResponseModel Delete(object id)
        {
            return _hotelRoomRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _hotelRoomRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search
        /// <summary>
        /// search the HotelRooms.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchHotelRooms(JqSearchIn si)
        {
            var hotelRooms = GetAll().Select(u => new HotelRoomModel
            {
                Id = u.Id,
                Name = u.Name,
                Status = u.Status,
                HotelRoomTypeId = u.HotelRoomTypeId,
                HotelRoomTypeName = u.HotelRoomType.Name,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(hotelRooms);
        }

        #endregion

        #region Manage Grid

        /// <summary>
        /// Manage Site HotelRoom
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the HotelRoom model</param>
        /// <returns></returns>
        public ResponseModel ManageHotelRoom(GridOperationEnums operation, HotelRoomModel model)
        {
            int hotelRoomTypeId;
            ResponseModel response;
            Mapper.CreateMap<HotelRoomModel, HotelRoom>();
            HotelRoom hotelRoom;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    hotelRoom = GetById(model.Id);
                    hotelRoom.Name = model.Name;
                    hotelRoom.Note = model.Note;
                    hotelRoom.Status = model.Status;
                    if (int.TryParse(model.HotelRoomTypeName, out hotelRoomTypeId))
                    {
                        hotelRoom.HotelRoomTypeId = hotelRoomTypeId;
                    }

                    response = Update(hotelRoom);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelRooms:::Messages:::UpdateSuccessfully:::Update room successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelRooms:::Messages:::UpdateFailure:::Update room failed. Please try again later."));

                case GridOperationEnums.Add:
                    hotelRoom = Mapper.Map<HotelRoomModel, HotelRoom>(model);
                    hotelRoom.Status = model.Status;
                    if (int.TryParse(model.HotelRoomTypeName, out hotelRoomTypeId))
                    {
                        hotelRoom.HotelRoomTypeId = hotelRoomTypeId;
                    }

                    response = Insert(hotelRoom);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelRooms:::Messages:::CreateSuccessfully:::Create room successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelRooms:::Messages:::CreateFailure:::Insert room failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelRooms:::Messages:::DeleteSuccessfully:::Delete room successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelRooms:::Messages:::DeleteFailure:::Delete room failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::HotelRooms:::Messages:::ObjectNotFounded:::Room is not founded.")
            };
        }

        #endregion

        /// <summary>
        /// Get news status
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetStatus()
        {
            return EnumUtilities.GetSelectListFromEnum<HotelRoomEnums.StatusEnums>();
        }
    }
}
