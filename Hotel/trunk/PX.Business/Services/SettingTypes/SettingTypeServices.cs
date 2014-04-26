﻿using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.SettingTypes;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using AutoMapper;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.SettingTypes
{
    public class SettingTypeServices : ISettingTypeServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly SettingTypeRepository _settingTypeRepository;
        public SettingTypeServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _settingTypeRepository = new SettingTypeRepository();
        }

        #region Base
        public IQueryable<SettingType> GetAll()
        {
            return _settingTypeRepository.GetAll();
        }
        public IQueryable<SettingType> Fetch(Expression<Func<SettingType, bool>> expression)
        {
            return _settingTypeRepository.Fetch(expression);
        }
        public SettingType GetById(object id)
        {
            return _settingTypeRepository.GetById(id);
        }
        public ResponseModel Insert(SettingType settingType)
        {
            return _settingTypeRepository.Insert(settingType);
        }
        public ResponseModel Update(SettingType settingType)
        {
            return _settingTypeRepository.Update(settingType);
        }
        public ResponseModel Delete(SettingType settingType)
        {
            return _settingTypeRepository.Delete(settingType);
        }
        public ResponseModel Delete(object id)
        {
            return _settingTypeRepository.Delete(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the user groups.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchSettingTypes(JqSearchIn si)
        {
            var settingTypes = GetAll().Select(u => new SettingTypeModel
            {
                Id = u.Id,
                Name = u.Name,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(settingTypes);
        }

        #endregion

        #region Grid Manage

        /// <summary>
        /// Manage user group
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the user group model</param>
        /// <returns></returns>
        public ResponseModel ManageSettingType(GridOperationEnums operation, SettingTypeModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<SettingTypeModel, SettingType>();
            SettingType settingType;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    settingType = _settingTypeRepository.GetById(model.Id);
                    settingType.Name = model.Name;
                    settingType.RecordOrder = model.RecordOrder;
                    settingType.RecordActive = model.RecordActive;
                    response = Update(settingType);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::SettingTypes:::Messages:::UpdateSuccessfully:::Update setting type successfully.")
                        : _localizedResourceServices.T("AdminModule:::SettingTypes:::Messages:::UpdateFailure:::Update setting type failed. Please try again later."));
                
                case GridOperationEnums.Add:
                    settingType = Mapper.Map<SettingTypeModel, SettingType>(model);
                    response = Insert(settingType);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::SettingTypes:::Messages:::CreateSuccessfully:::Create setting type successfully.")
                        : _localizedResourceServices.T("AdminModule:::SettingTypes:::Messages:::CreateFailure:::Create setting type failed. Please try again later."));
                
                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::SettingTypes:::Messages:::DeleteSuccessfully:::Delete setting type successfully.")
                        : _localizedResourceServices.T("AdminModule:::SettingTypes:::Messages:::DeleteFailure:::Delete setting type failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::SettingTypes:::Messages:::ObjectNotFounded:::Setting type is not founded.")
            };
        }

        #endregion

        /// <summary>
        /// Gets the setting types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetSettingTypes(int? typeId)
        {
            return GetAll().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = SqlFunctions.StringConvert((double)r.Id).Trim(),
                Selected = typeId.HasValue && r.Id == typeId
            });
        }
    }
}
