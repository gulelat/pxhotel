using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using PX.Business.Models.Settings;
using PX.Business.Models.Settings.SettingTypes.Base;
using PX.Business.Services.SettingTypes;
using PX.Core.Configurations;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.Settings
{
    public class SettingServices : ISettingServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly ISettingTypeServices _settingTypeServices;
        private readonly SiteSettingRepository _siteSettingRepository;
        public SettingServices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _settingTypeServices = HostContainer.GetInstance<ISettingTypeServices>();
            _siteSettingRepository = new SiteSettingRepository(entities);
        }

        #region Base
        public IQueryable<SiteSetting> GetAll()
        {
            return _siteSettingRepository.GetAll();
        }
        public IQueryable<SiteSetting> Fetch(Expression<Func<SiteSetting, bool>> expression)
        {
            return _siteSettingRepository.Fetch(expression);
        }
        public SiteSetting GetById(object id)
        {
            return _siteSettingRepository.GetById(id);
        }
        public ResponseModel Insert(SiteSetting siteSetting)
        {
            return _siteSettingRepository.Insert(siteSetting);
        }
        public ResponseModel Update(SiteSetting siteSetting)
        {
            return _siteSettingRepository.Update(siteSetting);
        }
        public ResponseModel Delete(SiteSetting siteSetting)
        {
            return _siteSettingRepository.Delete(siteSetting);
        }
        public ResponseModel Delete(object id)
        {
            return _siteSettingRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _siteSettingRepository.InactiveRecord(id);
        }
        #endregion

        #region Initialize

        /// <summary>
        /// Initialize default settings
        /// </summary>
        public void Initialize()
        {
            var initializeSettings = new List<SiteSetting>
            {
                new SiteSetting { Name = SettingNames.CurlyBracketMaxLoop, Value = "5", SettingTypeId = (int)SettingEnums.TypeEnums.System },
                new SiteSetting { Name = SettingNames.MaxSizeUploaded, Value = "10485760", SettingTypeId = (int)SettingEnums.TypeEnums.System },
                new SiteSetting { Name = SettingNames.DefaultAddress, Value = "Viet Nam", SettingTypeId = (int)SettingEnums.TypeEnums.System },
                new SiteSetting { Name = SettingNames.LogsPageSize, Value = "10", SettingTypeId = (int)SettingEnums.TypeEnums.BackEnd },
                new SiteSetting { Name = SettingNames.DefaultHistoryLength, Value = "5", SettingTypeId = (int)SettingEnums.TypeEnums.BackEnd },
                new SiteSetting { Name = SettingNames.DefaultHistoryStart, Value = "0", SettingTypeId = (int)SettingEnums.TypeEnums.BackEnd },
            };

            foreach (var setting in initializeSettings)
            {
                if (!GetAll().Any(s => s.Name.Equals(setting.Name)))
                {
                    Insert(setting);
                }
            }
        }
        #endregion

        #region Grid Search
        /// <summary>
        /// search the SiteSettings.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchSiteSettings(JqSearchIn si)
        {
            var siteSettings = GetAll().Select(u => new SiteSettingModel
            {
                Id = u.Id,
                Name = u.Name,
                Value = u.Value,
                SettingTypeId = u.SettingTypeId,
                SettingTypeName = u.SettingType.Name,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(siteSettings);
        }

        #endregion

        #region Manage Grid
        /// <summary>
        /// Manage Site Setting
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the setting model</param>
        /// <returns></returns>
        public ResponseModel ManageSiteSetting(GridOperationEnums operation, SiteSettingModel model)
        {
            int settingTypeId;
            ResponseModel response;
            Mapper.CreateMap<SiteSettingModel, SiteSetting>();
            SiteSetting siteSetting;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    siteSetting = GetById(model.Id);
                    siteSetting.Value = model.Value;
                    if (int.TryParse(model.SettingTypeName, out settingTypeId))
                    {
                        siteSetting.SettingTypeId = settingTypeId;
                    }

                    response = Update(siteSetting);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Settings:::Messages:::UpdateSuccessfully:::Update setting successfully.")
                        : _localizedResourceServices.T("AdminModule:::Settings:::Messages:::UpdateFailure:::Update setting failed. Please try again later."));

                case GridOperationEnums.Add:
                    siteSetting = Mapper.Map<SiteSettingModel, SiteSetting>(model);
                    if (int.TryParse(model.SettingTypeName, out settingTypeId))
                    {
                        siteSetting.SettingTypeId = settingTypeId;
                    }

                    response = Insert(siteSetting);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Settings:::Messages:::CreateSuccessfully:::Create setting successfully.")
                        : _localizedResourceServices.T("AdminModule:::Settings:::Messages:::CreateFailure:::Insert setting failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Settings:::Messages:::DeleteSuccessfully:::Delete setting successfully.")
                        : _localizedResourceServices.T("AdminModule:::Settings:::Messages:::DeleteFailure:::Delete setting failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Settings:::Messages:::ObjectNotFounded:::Setting is not founded.")
            };
        }

        #endregion

        #region Manage

        public SiteSettingManageModel GetSettingManageModel(int id)
        {
            var setting = GetById(id);
            if (setting != null)
            {
                var settingManageModel = new SiteSettingManageModel
                {
                    SettingTypeId = setting.SettingTypeId,
                    SettingTypes = _settingTypeServices.GetSettingTypes(setting.SettingTypeId),
                    SettingName = setting.Name
                };
                var settingParsers = ReflectionUtilities.GetAllImplementTypesOfInterface(typeof(ISettingModel));
                foreach (var parser in settingParsers)
                {
                    var instance = (ISettingModel)Activator.CreateInstance(parser);
                    if (instance.SettingName.Equals(setting.Name))
                    {
                        settingManageModel.Setting = instance.LoadSetting();
                        return settingManageModel;
                    }
                }
                settingManageModel.Setting = setting;
                return settingManageModel;
            }
            return null;
        }

        public ResponseModel SaveSettingManageModel(SiteSettingManageModel model, NameValueCollection data)
        {
            var setting = _siteSettingRepository.GetByKey(model.SettingName);
            if (setting != null)
            {
                setting.SettingTypeId = model.SettingTypeId;
                var settingParsers = ReflectionUtilities.GetAllImplementTypesOfInterface(typeof(ISettingModel));
                ResponseModel response;
                foreach (var parser in settingParsers)
                {
                    var instance = (ISettingModel)Activator.CreateInstance(parser);
                    if (instance.SettingName.Equals(setting.Name))
                    {
                        setting.Value = instance.GetSettingValue(data);
                        response = Update(setting);
                        return response.SetMessage(response.Success ?
                            _localizedResourceServices.T("AdminModule:::Settings:::Messages:::UpdateSuccessfully:::Update setting successfully.")
                            : _localizedResourceServices.T("AdminModule:::Settings:::Messages:::UpdateFailure:::Update setting failed. Please try again later."));
                    }
                }
                setting.Value = data["Value"];
                response = Update(setting);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::Settings:::Messages:::UpdateSuccessfully:::Update setting successfully.")
                    : _localizedResourceServices.T("AdminModule:::Settings:::Messages:::UpdateFailure:::Update setting failed. Please try again later."));

            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Settings:::Messages:::ObjectNotFounded:::Setting is not founded.")
            };
        }

        #endregion

        /// <summary>
        /// Get setting with key, if not found, insert setting to database
        /// </summary>
        /// <typeparam name="T">the return object type</typeparam>
        /// <param name="key">key to search</param>
        /// <param name="defaultValue">default value</param>
        /// <returns></returns>
        public T GetSetting<T>(string key, T defaultValue)
        {
            var setting = _siteSettingRepository.GetByKey(key);
            if (setting == null)
            {
                setting = new SiteSetting
                    {
                        Name = key,
                        Value = key
                    };
                Insert(setting);
                return defaultValue;
            }
            return setting.Value.ToType<T>();
        }

        /// <summary>
        /// Get setting by key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"> </param>
        /// <returns></returns>
        public T GetSetting<T>(int id)
        {
            var setting = _siteSettingRepository.GetById(id);
            if (setting == null)
            {
                return default(T);
            }
            return setting.Value.ToType<T>();
        }

        /// <summary>
        /// Get setting by key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetSetting<T>(string key)
        {
            var setting = _siteSettingRepository.GetByKey(key);
            if (setting == null)
            {
                return default(T);
            }
            return setting.Value.ToType<T>();
        }

        /// <summary>
        /// Load object setting
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="parameterArray"></param>
        /// <returns></returns>
        public dynamic LoadSetting<T>(object[] parameterArray = null)
        {
            var type = typeof(T);
            var methodInfo = type.GetMethod("LoadSetting");
            object result = null;
            if (methodInfo != null)
            {
                var parameters = methodInfo.GetParameters();
                var classInstance = Activator.CreateInstance(type, null);
                result = methodInfo.Invoke(classInstance, parameters.Length == 0 ? null : parameterArray);
            }
            return result;
        }
    }
}
