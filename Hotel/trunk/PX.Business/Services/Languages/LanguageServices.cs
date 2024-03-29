﻿using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using PX.Business.Models.Languages;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Environments;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.Languages
{
    public class LanguageServices : ILanguageServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly LanguageRepository _languageRepository;
        public LanguageServices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _languageRepository = new LanguageRepository(entities);
        }

        #region Base
        public IQueryable<Language> GetAll()
        {
            return _languageRepository.GetAll();
        }
        public IQueryable<Language> Fetch(Expression<Func<Language, bool>> expression)
        {
            return _languageRepository.Fetch(expression);
        }
        public Language FetchFirst(Expression<Func<Language, bool>> expression)
        {
            return _languageRepository.FetchFirst(expression);
        }
        public Language GetById(object id)
        {
            return _languageRepository.GetById(id);
        }
        public ResponseModel Insert(Language language)
        {
            return _languageRepository.Insert(language);
        }
        public ResponseModel Update(Language language)
        {
            return _languageRepository.Update(language);
        }
        public ResponseModel Delete(Language language)
        {
            return _languageRepository.Delete(language);
        }
        public ResponseModel Delete(object id)
        {
            return _languageRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _languageRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search
        /// <summary>
        /// search the Languages.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchLanguages(JqSearchIn si)
        {
            var languages = GetAll().Select(u => new LanguageModel
            {
                Id = u.Id,
                Name = u.Name,
                ShortName = u.ShortName,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(languages);
        }

        #endregion

        #region Manage Grid

        /// <summary>
        /// Manage Language
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel ManageLanguage(GridOperationEnums operation, LanguageModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<LanguageModel, Language>();
            Language language;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    language = GetById(model.Id);
                    language.Name = model.Name;
                    language.ShortName = model.ShortName;
                    language.RecordActive = model.RecordActive;
                    language.RecordOrder = model.RecordOrder;
                    response = Update(language);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Languages:::Messages::UpdateSuccessfully:::Update language successfully.")
                        : _localizedResourceServices.T("AdminModule:::Languages:::Messages:::UpdateFailure:::Update language failed. Please try again later."));

                case GridOperationEnums.Add:
                    language = Mapper.Map<LanguageModel, Language>(model);
                    response = Insert(language);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Languages:::Messages:::CreateSuccessfully:::Create language successfully.")
                        : _localizedResourceServices.T("AdminModule:::Languages:::Messages:::CreateFailure:::Create language failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Languages:::Messages:::DeleteSuccessfully:::Delete language successfully.")
                        : _localizedResourceServices.T("AdminModule:::Languages:::Messages:::DeleteFailure:::Delete language failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Languages:::Messages:::ObjectNotFounded:::Language is not founded.")
            };
        }

        #endregion
    }
}
