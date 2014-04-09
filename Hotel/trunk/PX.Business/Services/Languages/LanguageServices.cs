using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using PX.Business.Models.Languages;
using PX.Business.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.Languages
{
    public class LanguageServices : ILanguageServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public LanguageServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base
        public IQueryable<Language> GetAll()
        {
            return LanguageRepository.GetAll();
        }
        public IQueryable<Language> Fetch(Expression<Func<Language, bool>> expression)
        {
            return LanguageRepository.Fetch(expression);
        }
        public Language GetById(object id)
        {
            return LanguageRepository.GetById(id);
        }
        public ResponseModel Insert(Language language)
        {
            return LanguageRepository.Insert(language);
        }
        public ResponseModel Update(Language language)
        {
            return LanguageRepository.Update(language);
        }
        public ResponseModel Delete(Language language)
        {
            return LanguageRepository.Delete(language);
        }
        public ResponseModel Delete(object id)
        {
            return LanguageRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return LanguageRepository.InactiveRecord(id);
        }
        #endregion

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
                        _localizedResourceServices.T("AdminModule:::Languages:::Update language successfully")
                        : _localizedResourceServices.T("AdminModule:::Languages:::Update language failure. Please try again later."));

                case GridOperationEnums.Add:
                    language = Mapper.Map<LanguageModel, Language>(model);
                    response = Insert(language);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Languages:::Create language successfully")
                        : _localizedResourceServices.T("AdminModule:::Languages:::Create language failure. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Languages:::Delete language successfully")
                        : _localizedResourceServices.T("AdminModule:::Languages:::Delete language failure. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Languages:::Object not founded")
            };
        }

    }
}
