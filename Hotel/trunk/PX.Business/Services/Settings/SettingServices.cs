using System;
using System.Data;
using System.Linq;
using AutoMapper;
using PX.Business.Models.Settings;
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
        #region Base
        public IQueryable<SiteSetting> GetAll()
        {
            return SiteSettingRepository.GetAll();
        }
        public SiteSetting GetById(object id)
        {
            return SiteSettingRepository.GetById(id);
        }
        public ResponseModel Insert(SiteSetting siteSetting)
        {
            return SiteSettingRepository.Insert(siteSetting);
        }
        public ResponseModel Update(SiteSetting siteSetting)
        {
            return SiteSettingRepository.Update(siteSetting);
        }
        public ResponseModel HierarchyUpdate(SiteSetting siteSetting)
        {
            return SiteSettingRepository.HierarchyUpdate(siteSetting);
        }
        public ResponseModel HierarchyInsert(SiteSetting siteSetting)
        {
            return SiteSettingRepository.HierarchyInsert(siteSetting);
        }
        public ResponseModel Delete(SiteSetting siteSetting)
        {
            return SiteSettingRepository.Delete(siteSetting);
        }
        public ResponseModel Delete(object id)
        {
            return SiteSettingRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return SiteSettingRepository.InactiveRecord(id);
        }
        #endregion

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
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(siteSettings);
        }

        /// <summary>
        /// Manage SiteSetting
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel ManageSiteSetting(GridOperationEnums operation, SiteSettingModel model)
        {
            Mapper.CreateMap<SiteSettingModel, SiteSetting>();
            SiteSetting siteSetting;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    siteSetting = GetById(model.Id);
                    siteSetting.Name = model.Name;
                    siteSetting.Value = model.Value;
                    siteSetting.RecordActive = model.RecordActive;
                    siteSetting.RecordOrder = model.RecordOrder;
                    return Update(siteSetting);
                case GridOperationEnums.Add:
                    siteSetting = Mapper.Map<SiteSettingModel, SiteSetting>(model);
                    return Insert(siteSetting);
                case GridOperationEnums.Del:
                    return Delete(model.Id);
            }
            return new ResponseModel
            {
                Success = false,
                Message = "Object not founded"
            };
        }

        /// <summary>
        /// Get setting with key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public T GetSetting<T>(string key, T defaultValue)
        {
            var setting = SiteSettingRepository.GetByKey(key);
            if(setting == null)
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

        public T GetSetting<T>(string key)
        {
            var setting = SiteSettingRepository.GetByKey(key);
            if(setting == null)
            {
                throw new ObjectNotFoundException();
            }
            return setting.Value.ToType<T>();
        }
    }
}
