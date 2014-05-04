using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using PX.Business.Models.HotelCustomers;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.HotelCustomers
{
    public class HotelCustomerServices : IHotelCustomerServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly HotelCustomerRepository _hotelCustomerRepository;
        public HotelCustomerServices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _hotelCustomerRepository = new HotelCustomerRepository(entities);
        }

        #region Base
        public IQueryable<HotelCustomer> GetAll()
        {
            return _hotelCustomerRepository.GetAll();
        }
        public IQueryable<HotelCustomer> Fetch(Expression<Func<HotelCustomer, bool>> expression)
        {
            return _hotelCustomerRepository.Fetch(expression);
        }
        public HotelCustomer FetchFirst(Expression<Func<HotelCustomer, bool>> expression)
        {
            return _hotelCustomerRepository.FetchFirst(expression);
        }
        public HotelCustomer GetById(object id)
        {
            return _hotelCustomerRepository.GetById(id);
        }
        public ResponseModel Insert(HotelCustomer hotelCustomer)
        {
            return _hotelCustomerRepository.Insert(hotelCustomer);
        }
        public ResponseModel Update(HotelCustomer hotelCustomer)
        {
            return _hotelCustomerRepository.Update(hotelCustomer);
        }
        public ResponseModel Delete(HotelCustomer hotelCustomer)
        {
            return _hotelCustomerRepository.Delete(hotelCustomer);
        }
        public ResponseModel Delete(object id)
        {
            return _hotelCustomerRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _hotelCustomerRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search
        /// <summary>
        /// search the HotelCustomers.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchHotelCustomers(JqSearchIn si)
        {
            var hotelCustomers = GetAll().Select(u => new HotelCustomerModel
            {
                Id = u.Id,
                FullName = u.FullName,
                Email = u.Email,
                IndentityNumber = u.IndentityNumber,
                Address = u.Address,
                Country = u.Country,
                Phone = u.Phone,
                Note = u.Note,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(hotelCustomers);
        }

        #endregion

        #region Manage Grid

        /// <summary>
        /// Manage Site HotelCustomer
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the HotelCustomer model</param>
        /// <returns></returns>
        public ResponseModel ManageHotelCustomer(GridOperationEnums operation, HotelCustomerModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<HotelCustomerModel, HotelCustomer>();
            HotelCustomer hotelCustomer;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    hotelCustomer = GetById(model.Id);
                    hotelCustomer.FullName = model.FullName;
                    hotelCustomer.Email = model.Email;
                    hotelCustomer.IndentityNumber = model.IndentityNumber;
                    hotelCustomer.Address = model.Address;
                    hotelCustomer.Country = model.Country;
                    hotelCustomer.Phone = model.Phone;
                    hotelCustomer.Note = model.Note;

                    response = Update(hotelCustomer);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelCustomers:::Messages:::UpdateSuccessfully:::Update customer successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelCustomers:::Messages:::UpdateFailure:::Update customer failed. Please try again later."));

                case GridOperationEnums.Add:
                    hotelCustomer = Mapper.Map<HotelCustomerModel, HotelCustomer>(model);

                    response = Insert(hotelCustomer);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelCustomers:::Messages:::CreateSuccessfully:::Create customer successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelCustomers:::Messages:::CreateFailure:::Insert customer failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelCustomers:::Messages:::DeleteSuccessfully:::Delete customer successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelCustomers:::Messages:::DeleteFailure:::Delete customer failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::HotelCustomers:::Messages:::ObjectNotFounded:::Customer is not founded.")
            };
        }

        #endregion
    }
}
