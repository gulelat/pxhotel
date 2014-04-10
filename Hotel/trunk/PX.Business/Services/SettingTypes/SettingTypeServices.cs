using System;
using System.Collections.Generic;
using System.Globalization;
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
using PX.Core.Ultilities;

namespace PX.Business.Services.SettingTypes
{
    public class SettingTypeServices : ISettingTypeServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public SettingTypeServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base
        public IQueryable<SettingType> GetAll()
        {
            return SettingTypeRepository.GetAll();
        }
        public IQueryable<SettingType> Fetch(Expression<Func<SettingType, bool>> expression)
        {
            return SettingTypeRepository.Fetch(expression);
        }
        public SettingType GetById(object id)
        {
            return SettingTypeRepository.GetById(id);
        }
        public ResponseModel Insert(SettingType settingType)
        {
            return SettingTypeRepository.Insert(settingType);
        }
        public ResponseModel Update(SettingType settingType)
        {
            return SettingTypeRepository.Update(settingType);
        }
        public ResponseModel Delete(SettingType settingType)
        {
            return SettingTypeRepository.Delete(settingType);
        }
        public ResponseModel Delete(object id)
        {
            return SettingTypeRepository.Delete(id);
        }
        #endregion

        #region Search Methods

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

        #region Manage Methods

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
                    settingType = SettingTypeRepository.GetById(model.Id);
                    settingType.Name = model.Name;
                    settingType.RecordOrder = model.RecordOrder;
                    settingType.RecordActive = model.RecordActive;
                    response = Update(settingType);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::SettingTypes:::Update setting type successfully")
                        : _localizedResourceServices.T("AdminModule:::SettingTypes:::Update setting type failure. Please try again later."));
                
                case GridOperationEnums.Add:
                    settingType = Mapper.Map<SettingTypeModel, SettingType>(model);
                    response = Insert(settingType);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::SettingTypes:::Insert setting type successfully")
                        : _localizedResourceServices.T("AdminModule:::SettingTypes:::Insert setting type failure. Please try again later."));
                
                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::SettingTypes:::Delete setting type successfully")
                        : _localizedResourceServices.T("AdminModule:::SettingTypes:::Delete setting type failure. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::SettingTypes:::Setting type not founded")
            };
        }

        #endregion

        /// <summary>
        /// Gets the setting types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetSettingTypes()
        {
            return GetAll().ToList().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString(CultureInfo.InvariantCulture)
            });
        }
    }
}
