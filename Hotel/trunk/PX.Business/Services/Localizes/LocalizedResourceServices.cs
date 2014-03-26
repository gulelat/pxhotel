using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Web;
using AutoMapper;
using PX.Business.Models.LocalizedResources;
using PX.Business.Mvc.Environments;
using PX.Business.Mvc.WorkContext;
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
        private const string LocalizedSerperator = ":::";
        private const string LocalizedResourceDictionary = "LocalizedResourceDictionary";

        #region Base
        public IQueryable<LocalizedResource> GetAll()
        {
            return LocalizedResourceRepository.GetAll();
        }
        public IQueryable<LocalizedResource> Fetch(Expression<Func<LocalizedResource, bool>> expression)
        {
            return LocalizedResourceRepository.Fetch(expression);
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
            var localizedResources = Fetch(l => l.LanguageId.Equals(language)).Select(u => new LocalizedResourceModel
            {
                Id = u.Id,
                TextKey = u.TextKey,
                LanguageId = u.LanguageId,
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
            ResponseModel response;
            Mapper.CreateMap<LocalizedResourceModel, LocalizedResource>();
            LocalizedResource localizedResource;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    localizedResource = GetById(model.Id);
                    localizedResource.TranslatedValue = model.TranslatedValue;
                    localizedResource.RecordActive = model.RecordActive;

                    response = Update(localizedResource);
                    return response.SetMessage(response.Success ?
                        T("AdminModule:::LocalizedResources:::Update localized resource successfully")
                        : T("AdminModule:::LocalizedResources:::Update localized resource failure"));

                case GridOperationEnums.Add:
                    localizedResource = Mapper.Map<LocalizedResourceModel, LocalizedResource>(model);
                    localizedResource.DefaultValue = model.TranslatedValue;
                    response = Insert(localizedResource);
                    return response.SetMessage(response.Success ?
                        T("AdminModule:::LocalizedResources:::Insert localized resource successfully")
                        : T("AdminModule:::LocalizedResources:::Insert localized resource failure"));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        T("AdminModule:::LocalizedResources:::Delete localized resource successfully")
                        : T("AdminModule:::LocalizedResources:::Delete localized resource failure"));
            }
            return new ResponseModel
            {
                Success = false,
                Message = T("AdminModule:::LocalizedResources:::Localized resource not founded")
            };
        }

        #region Get localize resources

        /// <summary>
        /// Get text by key
        /// </summary>
        /// <param name="textKey"></param>
        /// <returns></returns>
        public string T(string textKey)
        {
            return GetLocalizedResource(textKey, textKey);
        }

        /// <summary>
        /// Get text by key
        /// </summary>
        /// <param name="textKey"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public string T(string textKey, string defaultValue)
        {
            return GetLocalizedResource(textKey, defaultValue);
        }

        public string GetLocalizedResource(string textKey, string defaultValue = null, params object[] parameters)
        {
            if (string.IsNullOrEmpty(textKey))
                throw new ArgumentNullException("textKey", "text key cannot be null");
            var langKey = WorkContext.CurrentCuture;
            var key = DictionaryHelper.BuildKey(langKey, textKey);
            var dictionary = HttpContext.Current.Application[LocalizedResourceDictionary] as List<LocalizeDictionaryItem>;
            if (dictionary == null || !dictionary.Any(l => l.Key.Equals(key) && l.Language.Equals(langKey)))
            {
                return GetDefaultValue(langKey, textKey, defaultValue, parameters);
            }
            var localizeResource = dictionary.FirstOrDefault(d => d.Key.Equals(key));
            if (localizeResource != null)
            {
                if (parameters != null && parameters.Any())
                {
                    return string.Format(localizeResource.Value, parameters);
                }
                return localizeResource.Value;
            }

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
                return string.Format(localizeResource.TranslatedValue, parameters);
            }

            return UpdateDictionaryToDb(textKey, defaultValue, parameters);
        }

        /// <summary>
        /// Update new value to database
        /// </summary>
        /// <param name="textKey"></param>
        /// <param name="defaultValue"></param>
        /// <param name="parameters"> </param>
        private string UpdateDictionaryToDb(string textKey, string defaultValue = "", params object[] parameters)
        {
            var values = defaultValue.Split(new[] { LocalizedSerperator }, StringSplitOptions.RemoveEmptyEntries).Last();
            var existedResourceIds = Fetch(l => l.TextKey.Equals(textKey)).Select(l => l.LanguageId);
            var languages = LanguageRepository.Fetch(l => !existedResourceIds.Contains(l.Id)).Select(l => l.Id).ToList();
            foreach (var language in languages)
            {
                var localizeResource = new LocalizedResource
                {
                    TextKey = textKey,
                    DefaultValue = values,
                    TranslatedValue = values,
                    LanguageId = language
                };
                Insert(localizeResource);
            }
            RefreshDictionary();
            if (parameters != null && parameters.Any())
            {
                return string.Format(values, parameters);
            }
            return values;
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
                LanguageId = l.LanguageId,
                TranslatedValue = l.TranslatedValue
            }).ToList();
            HttpContext.Current.Application[LocalizedResourceDictionary] =
                data.Select(
                    l => new LocalizeDictionaryItem
                    {
                        Language = l.LanguageId,
                        Key = DictionaryHelper.BuildKey(l.LanguageId, l.TextKey),
                        Value = l.TranslatedValue
                    }).ToList();
        }
        #endregion
    }
}
