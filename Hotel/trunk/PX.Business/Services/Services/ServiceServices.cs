using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using PX.Business.Models.Services;
using PX.Business.Models.Services.CurlyBrackets;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using AutoMapper;
using PX.Core.Ultilities;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.Services
{
    public class ServiceServices : IServiceServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly ServiceRepository _serviceRepository;
        public ServiceServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _serviceRepository = new ServiceRepository();
        }

        #region Base
        public IQueryable<EntityModel.Service> GetAll()
        {
            return _serviceRepository.GetAll();
        }
        public IQueryable<EntityModel.Service> Fetch(Expression<Func<EntityModel.Service, bool>> expression)
        {
            return _serviceRepository.Fetch(expression);
        }
        public EntityModel.Service GetById(object id)
        {
            return _serviceRepository.GetById(id);
        }
        public ResponseModel Insert(EntityModel.Service service)
        {
            return _serviceRepository.Insert(service);
        }
        public ResponseModel Update(EntityModel.Service service)
        {
            return _serviceRepository.Update(service);
        }
        public ResponseModel Delete(EntityModel.Service service)
        {
            return _serviceRepository.Delete(service);
        }
        public ResponseModel Delete(object id)
        {
            return _serviceRepository.Delete(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the user groups.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchService(JqSearchIn si)
        {
            var services = GetAll().ToList().Select(u => new ServiceModel
            {
                Id = u.Id,
                Title = u.Title,
                Description = u.Description,
                ImageUrl = u.ImageUrl,
                Content = u.Content,
                Status = u.Status,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            }).AsQueryable();

            return si.Search(services);
        }

        #endregion

        #region Grid Manage

        /// <summary>
        /// Manage user group
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the user group model</param>
        /// <returns></returns>
        public ResponseModel ManageService(GridOperationEnums operation, ServiceModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<ServiceModel, EntityModel.Service>();
            EntityModel.Service service;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    service = _serviceRepository.GetById(model.Id);
                    service.Title = model.Title;
                    service.Status = model.Status;
                    service.RecordOrder = model.RecordOrder;
                    service.RecordActive = model.RecordActive;
                    response = Update(service);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Services:::Messages:::UpdateSuccessfully:::Update service successfully.")
                        : _localizedResourceServices.T("AdminModule:::Services:::Messages:::UpdateFailure:::Update service failed. Please try again later."));

                case GridOperationEnums.Add:
                    service = Mapper.Map<ServiceModel, EntityModel.Service>(model);
                    service.Status = model.Status;
                    service.Content = string.Empty;
                    service.Description = string.Empty;
                    response = Insert(service);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Services:::Messages:::CreateSuccessfully:::Create service successfully.")
                        : _localizedResourceServices.T("AdminModule:::Services:::Messages:::CreateFailure:::Insert service failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Services:::Messages:::DeleteSuccessfully:::Delete service successfully.")
                        : _localizedResourceServices.T("AdminModule:::Services:::Messages:::DeleteFailure:::Delete service failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Services:::Messages:::ObjectNotFounded:::Service is not founded.")
            };
        }

        #endregion

        #region Manage

        /// <summary>
        /// Get Service manage model by id
        /// </summary>
        /// <param name="id">the Service id</param>
        /// <returns></returns>
        public ServiceManageModel GetServiceManageModel(int? id = null)
        {
            var service = GetById(id);
            if (service != null)
            {
                return new ServiceManageModel
                {
                    Id = service.Id,
                    Description = service.Description,
                    Content = service.Content,
                    ImageUrl = service.ImageUrl,
                    Title = service.Title,
                    Status = service.Status,
                    StatusList = GetStatus(),
                    RecordOrder = service.RecordOrder,
                    RecordActive = service.RecordActive
                };
            }
            return new ServiceManageModel
            {
                StatusList = GetStatus(),
            };
        }

        /// <summary>
        /// Save Service manage model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SaveServiceManageModel(ServiceManageModel model)
        {
            ResponseModel response;
            var service = GetById(model.Id);

            #region Edit Service
            if (service != null)
            {
                service.Title = model.Title;

                service.Status = model.Status;
                service.Description = model.Description;
                service.Content = model.Content;
                service.ImageUrl = model.ImageUrl;

                //Get page record order
                response = Update(service);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::Services:::Messages:::UpdateSuccessfully:::Update service successfully.")
                    : _localizedResourceServices.T("AdminModule:::Services:::Messages:::UpdateFailure:::Update service failed. Please try again later."));
            }
            #endregion

            service = new EntityModel.Service
            {
                Title = model.Title,
                Status = model.Status,
                Description = model.Description,
                Content = model.Content,
                ImageUrl = model.ImageUrl
            };
            response = Insert(service);
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::Services:::Messages:::CreateSuccessfully:::Create service successfully.")
                : _localizedResourceServices.T("AdminModule:::Services:::Messages:::CreateFailure:::Create service failed. Please try again later."));
        }
        #endregion

        /// <summary>
        /// Get service status
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetStatus()
        {
            return EnumUtilities.GetSelectListFromEnum<ServiceEnums.StatusEnums>();
        }

        /// <summary>
        /// Check if title is existed
        /// </summary>
        /// <param name="serviceId">the Service id</param>
        /// <param name="title">the Service title</param>
        /// <returns></returns>
        public bool IsTitleExisted(int? serviceId, string title)
        {
            return Fetch(u => u.Title.Equals(title) && u.Id != serviceId).Any();
        }

        public List<ServiceCurlyBracket> GetServices(int count)
        {
            return Fetch(s => s.Status == (int) ServiceEnums.StatusEnums.Active)
                .OrderBy(m => m.RecordOrder)
                .Take(count)
                .ToList().Select(s => new ServiceCurlyBracket
                    {
                        Id = s.Id,
                        Title = s.Title,
                        Description = s.Description,
                        Content = s.Content,
                        ImageUrl = s.ImageUrl,
                        DetailsUrl = UrlUtilities.GenerateUrl(HttpContext.Current.Request.RequestContext, "Services", "Details",
                                               new
                                                   {
                                                       area = "Admin",
                                                       id = s.Id
                                                   }),
                        RecordOrder = s.RecordOrder,
                        Created = s.Created,
                        CreatedBy = s.CreatedBy,
                        Updated = s.Updated,
                        UpdatedBy = s.UpdatedBy
                    }).ToList();
        }
    }
}
