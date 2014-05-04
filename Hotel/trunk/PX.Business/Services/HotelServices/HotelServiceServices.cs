using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using PX.Business.Models.HotelServices;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.HotelServices
{
    public class HotelServiceServices : IHotelServiceServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly HotelServiceRepository _hotelServiceRepository;
        public HotelServiceServices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _hotelServiceRepository = new HotelServiceRepository(entities);
        }

        #region Base
        public IQueryable<HotelService> GetAll()
        {
            return _hotelServiceRepository.GetAll();
        }
        public IQueryable<HotelService> Fetch(Expression<Func<HotelService, bool>> expression)
        {
            return _hotelServiceRepository.Fetch(expression);
        }
        public HotelService FetchFirst(Expression<Func<HotelService, bool>> expression)
        {
            return _hotelServiceRepository.FetchFirst(expression);
        }
        public HotelService GetById(object id)
        {
            return _hotelServiceRepository.GetById(id);
        }
        public ResponseModel Insert(HotelService hotelService)
        {
            return _hotelServiceRepository.Insert(hotelService);
        }
        public ResponseModel Update(HotelService hotelService)
        {
            return _hotelServiceRepository.Update(hotelService);
        }
        public ResponseModel Delete(HotelService hotelService)
        {
            return _hotelServiceRepository.Delete(hotelService);
        }
        public ResponseModel Delete(object id)
        {
            return _hotelServiceRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _hotelServiceRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search
        /// <summary>
        /// search the HotelServices.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchHotelServices(JqSearchIn si)
        {
            var hotelServices = GetAll().Select(u => new HotelServiceModel
            {
                Id = u.Id,
                Name = u.Name,
                ServiceIcon = u.ServiceIcon,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(hotelServices);
        }

        #endregion

        #region Manage Grid

        /// <summary>
        /// Manage Site HotelService
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the HotelService model</param>
        /// <returns></returns>
        public ResponseModel ManageHotelService(GridOperationEnums operation, HotelServiceModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<HotelServiceModel, HotelService>();
            HotelService hotelService;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    hotelService = GetById(model.Id);
                    hotelService.Name = model.Name;
                    hotelService.ServiceIcon = model.ServiceIcon;
                    hotelService.RecordOrder = model.RecordOrder;

                    response = Update(hotelService);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelServices:::Messages:::UpdateSuccessfully:::Update service successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelServices:::Messages:::UpdateFailure:::Update service failed. Please try again later."));

                case GridOperationEnums.Add:
                    hotelService = Mapper.Map<HotelServiceModel, HotelService>(model);

                    response = Insert(hotelService);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelServices:::Messages:::CreateSuccessfully:::Create service successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelServices:::Messages:::CreateFailure:::Insert service failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelServices:::Messages:::DeleteSuccessfully:::Delete service successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelServices:::Messages:::DeleteFailure:::Delete service failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::HotelServices:::Messages:::ObjectNotFounded:::Service is not founded.")
            };
        }

        /// <summary>
        /// Get NewsCategory by parent id
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetHotelRoomServices(int? roomTypeId = null)
        {
            return GetAll().Select(s => new SelectListItem
                {
                    Text = s.Name,
                    Value = SqlFunctions.StringConvert((double)s.Id),
                    Selected = s.HotelRoomServices.Any(hs => hs.RoomTypeId == roomTypeId)
                });
        }

        #endregion
    }
}
