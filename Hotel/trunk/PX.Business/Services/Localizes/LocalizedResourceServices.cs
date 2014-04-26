using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using PX.Business.Models.LocalizedResources;
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
        private readonly LocalizedResourceRepository _localizedResourceRepository;

        public LocalizedResourceServices()
        {
            _localizedResourceRepository = new LocalizedResourceRepository();
        }

        #region Base
        public IQueryable<LocalizedResource> GetAll()
        {
            return _localizedResourceRepository.GetAll();
        }
        public IQueryable<LocalizedResource> Fetch(Expression<Func<LocalizedResource, bool>> expression)
        {
            return _localizedResourceRepository.Fetch(expression);
        }
        public LocalizedResource GetById(object id)
        {
            return _localizedResourceRepository.GetById(id);
        }
        public ResponseModel Insert(LocalizedResource localizedResource)
        {
            return _localizedResourceRepository.Insert(localizedResource);
        }
        public ResponseModel Update(LocalizedResource localizedResource)
        {
            return _localizedResourceRepository.Update(localizedResource);
        }
        public ResponseModel Delete(LocalizedResource localizedResource)
        {
            return _localizedResourceRepository.Delete(localizedResource);
        }
        public ResponseModel Delete(object id)
        {
            return _localizedResourceRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _localizedResourceRepository.InactiveRecord(id);
        }
        #endregion

        #region Initialize

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
            WorkContext.LocalizedResourceDictionary =
                data.Select(
                    l => new LocalizeDictionaryItem
                    {
                        Language = l.LanguageId,
                        Key = DictionaryHelper.BuildKey(l.LanguageId, l.TextKey),
                        Value = l.TranslatedValue
                    }).ToList();
        }
        #endregion

        #region Grid Search

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

        #endregion

        #region Manage Grid

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
                    if (response.Success)
                        RefreshDictionary();
                    return response.SetMessage(response.Success ?
                        T("AdminModule:::LocalizedResources:::Messages:::UpdateSuccessfully:::Update localized resource successfully.")
                        : T("AdminModule:::LocalizedResources:::Messages:::UpdateFailure:::Update localized resource failed. Please try again later."));

                case GridOperationEnums.Add:
                    localizedResource = Mapper.Map<LocalizedResourceModel, LocalizedResource>(model);
                    localizedResource.DefaultValue = model.TranslatedValue;
                    response = Insert(localizedResource);
                    if (response.Success)
                        RefreshDictionary();
                    return response.SetMessage(response.Success ?
                        T("AdminModule:::LocalizedResources:::Messages:::CreateSuccessfully:::Create localized resource successfully.")
                        : T("AdminModule:::LocalizedResources:::Messages:::CreateFailure:::Create localized resource failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    if (response.Success)
                        RefreshDictionary();
                    return response.SetMessage(response.Success ?
                        T("AdminModule:::LocalizedResources:::Messages:::DeleteSuccessfully:::Delete localized resource successfully.")
                        : T("AdminModule:::LocalizedResources:::Messages:::DeleteFailure:::Delete localized resource failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = T("AdminModule:::LocalizedResources:::Messages:::ObjectNotFounded:::Localized resource is not founded.")
            };
        }

        #endregion

        #region Get localize resources

        /// <summary>
        /// Get text by key
        /// </summary>
        /// <param name="textKey"></param>
        /// <returns></returns>
        public string T(string textKey)
        {
            var values = textKey.Split(new[] { LocalizedSerperator }, StringSplitOptions.RemoveEmptyEntries);
            if (values.Count() < 2)
            {
                return textKey;
            }
            var defaultValue = values.Last();
            var newTextKeys = values.Take(values.Count() - 1);
            return GetLocalizedResource(string.Join(LocalizedSerperator, newTextKeys), defaultValue);
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
            var dictionary = WorkContext.LocalizedResourceDictionary;
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
            var localizeResource = _localizedResourceRepository.Get(langKey, textKey);

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
        private string UpdateDictionaryToDb(string textKey, string defaultValue, params object[] parameters)
        {
            var languageRepository = new LanguageRepository();
            var existedResourceIds = Fetch(l => l.TextKey.Equals(textKey)).Select(l => l.LanguageId).ToList();
            var languages = languageRepository.Fetch(l => !existedResourceIds.Contains(l.Id)).Select(l => l.Id).ToList();
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
            if (parameters != null && parameters.Any())
            {
                return string.Format(defaultValue, parameters);
            }
            return defaultValue;
        }
        #endregion
    }
}
