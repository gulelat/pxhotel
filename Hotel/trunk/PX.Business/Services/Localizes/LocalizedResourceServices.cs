using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Caching;
using AutoMapper;
using PX.Business.Models.LocalizedResources;
using PX.Business.Models.Localizes;
using PX.Business.Mvc.Constants;
using PX.Business.Services.Settings;
using PX.Core.Configurations;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Helpers;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.Localizes
{
    public class LocalizedResourceServices : ILocalizedResourceServices
    {
        private const string LocalizedResourceDictionary = "LocalizedResourceDictionary";

        #region Base
        public IQueryable<LocalizedResource> GetAll()
        {
            return LocalizedResourceRepository.GetAll();
        }
        public LocalizedResource GetById(object id)
        {
            return LocalizedResourceRepository.GetById(id);
        }
        public ResponseModel Insert(LocalizedResource localizedResource)
        {
            return LocalizedResourceRepository.Insert(localizedResource);
        }
        public ResponseModel Update(LocalizedResource localizedResource)
        {
            return LocalizedResourceRepository.Update(localizedResource);
        }
        public ResponseModel Delete(LocalizedResource localizedResource)
        {
            return LocalizedResourceRepository.Delete(localizedResource);
        }
        public ResponseModel Delete(object id)
        {
            return LocalizedResourceRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return LocalizedResourceRepository.InactiveRecord(id);
        }
        #endregion

        /// <summary>
        /// search the LocalizedResources.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchLocalizedResources(JqSearchIn si, string language)
        {
            var localizedResources = GetAll().Where(l => l.LanguageId.Equals(language)).Select(u => new LocalizedResourceModel
            {
                Id = u.Id,
                TextKey = u.TextKey,
                Language = u.LanguageId,
                DefaultValue = u.DefaultValue,
                TranslatedValue = u.TranslatedValue,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(localizedResources);
        }

        /// <summary>
        /// Manage LocalizedResource
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel ManageLocalizedResource(GridOperationEnums operation, LocalizedResourceModel model)
        {
            Mapper.CreateMap<LocalizedResourceModel, LocalizedResource>();
            LocalizedResource localizedResource;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    localizedResource = GetById(model.Id);
                    localizedResource.TranslatedValue = model.TranslatedValue;
                    localizedResource.RecordActive = model.RecordActive;
                    return Update(localizedResource);
                case GridOperationEnums.Add:
                    localizedResource = Mapper.Map<LocalizedResourceModel, LocalizedResource>(model);
                    return Insert(localizedResource);
                case GridOperationEnums.Del:
                    return Delete(model.Id);
            }
            return new ResponseModel
            {
                Success = false,
                Message = "Object not founded"
            };
        }

        #region Get localize resources
        public string GetLocalizedResource(string textKey, string defaultValue = null, params object[] parameters)
        {
            if (string.IsNullOrEmpty(textKey))
                throw new ArgumentNullException("textKey", "textKey cannot be null");
            var langKey = Thread.CurrentThread.CurrentCulture.Name;
            var key = DictionaryHelper.BuildKey(langKey, textKey);
            var dictionary = HttpContext.Current.Application[LocalizedResourceDictionary] as List<LocalizeDictionaryItem>;
            if (dictionary == null || !dictionary.Any(l => l.Key.Equals(key)))
            {
                return GetDefaultValue(langKey, textKey, defaultValue, parameters);
            }
            var localizeResource = dictionary.FirstOrDefault(d => d.Key.Equals(key));
            if (localizeResource != null)
                return localizeResource.Value;

            return string.Empty;
        }

        /// <summary>
        /// Gets the default value of a localized text in case this is not available in dictionary
        /// </summary>
        /// <param name="langKey">Language key of the localized to get</param>
        /// <param name="textKey">Key of the string/text to get localized</param>
        /// <param name="defaultValue">Default value for the key</param>
        /// <param name="parameters">Parameters for passing to the output string</param>
        /// <returns></returns>
        private string GetDefaultValue(string langKey, string textKey, string defaultValue = null, params object[] parameters)
        {
            var localizeResource = LocalizedResourceRepository.Get(langKey, textKey);

            if (localizeResource != null)
            {
                return localizeResource.TranslatedValue;
            }

            UpdateDictionaryToDb(textKey, defaultValue);

            return defaultValue;
        }

        private void UpdateDictionaryToDb(string textKey, string defaultValue = null)
        {
            var languages = LanguageRepository.GetAll().Select(l => l.Id).ToList();
            foreach (var language in languages)
            {
                var localizeResource = new LocalizedResource
                {
                    TextKey = textKey,
                    DefaultValue = defaultValue,
                    TranslatedValue = defaultValue,
                    LanguageId = language
                };
                Insert(localizeResource);
            }
            RefreshDictionary();
        }

        /// <summary>
        /// Refresh localize resource dictionaries
        /// </summary>
        public void RefreshDictionary()
        {
            var data = GetAll().Select(l => new LocalizedResourceModel
            {
                TextKey = l.TextKey,
                DefaultValue = l.DefaultValue,
                Language = l.LanguageId,
                TranslatedValue = l.TranslatedValue
            }).ToList();
            HttpContext.Current.Application[LocalizedResourceDictionary] =
                data.Select(
                    l => new LocalizeDictionaryItem
                    {
                        Key = DictionaryHelper.BuildKey(l.Language, l.TextKey),
                        Value = l.TranslatedValue
                    }).ToList();
        }
        #endregion
    }
}
