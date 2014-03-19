using System.Linq;
using AutoMapper;
using PX.Business.Models.Languages;
using PX.Business.Services.Settings;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.Languages
{
    public class LanguageServices : ILanguageServices
    {
        #region Base
        public IQueryable<Language> GetAll()
        {
            return LanguageRepository.GetAll();
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
        public ResponseModel HierarchyUpdate(Language language)
        {
            return LanguageRepository.HierarchyUpdate(language);
        }
        public ResponseModel HierarchyInsert(Language language)
        {
            return LanguageRepository.HierarchyInsert(language);
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
            var Languages = GetAll().Select(u => new LanguageModel
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

            return si.Search(Languages);
        }

        /// <summary>
        /// Manage Language
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel ManageLanguage(GridOperationEnums operation, LanguageModel model)
        {
            Mapper.CreateMap<LanguageModel, Language>();
            Language Language;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    Language = GetById(model.Id);
                    Language.Name = model.Name;
                    Language.ShortName = model.ShortName;
                    Language.RecordActive = model.RecordActive;
                    Language.RecordOrder = model.RecordOrder;
                    return Update(Language);
                case GridOperationEnums.Add:
                    Language = Mapper.Map<LanguageModel, Language>(model);
                    return Insert(Language);
                case GridOperationEnums.Del:
                    return Delete(model.Id);
            }
            return new ResponseModel
            {
                Success = false,
                Message = "Object not founded"
            };
        }
    }
}
