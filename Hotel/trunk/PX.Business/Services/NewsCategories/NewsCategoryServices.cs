using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using PX.Business.Models.Testimonials;
using PX.Business.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Configurations.Constants;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Repositories;
using PX.EntityModel.Repositories.RepositoryBase.Extensions;
using PX.EntityModel.Repositories.RepositoryBase.Models;

namespace PX.Business.Services.NewsCategories
{
    public class NewsCategorieservices : INewsCategoryServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public NewsCategorieservices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base
        public IQueryable<NewsCategory> GetAll()
        {
            return NewsCategoryRepository.GetAll();
        }
        public IQueryable<NewsCategory> Fetch(Expression<Func<NewsCategory, bool>> expression)
        {
            return NewsCategoryRepository.Fetch(expression);
        }
        public NewsCategory GetById(object id)
        {
            return NewsCategoryRepository.GetById(id);
        }
        public ResponseModel Insert(NewsCategory newsCategory)
        {
            return NewsCategoryRepository.Insert(newsCategory);
        }
        public ResponseModel Update(NewsCategory newsCategory)
        {
            return NewsCategoryRepository.Update(newsCategory);
        }
        public ResponseModel HierarchyUpdate(NewsCategory newsCategory)
        {
            return NewsCategoryRepository.HierarchyUpdate(newsCategory);
        }
        public ResponseModel HierarchyInsert(NewsCategory newsCategory)
        {
            return NewsCategoryRepository.HierarchyInsert(newsCategory);
        }
        public ResponseModel Delete(NewsCategory newsCategory)
        {
            return NewsCategoryRepository.Delete(newsCategory);
        }
        public ResponseModel Delete(object id)
        {
            return NewsCategoryRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return NewsCategoryRepository.InactiveRecord(id);
        }
        #endregion

        #region Manage NewsCategory

        /// <summary>
        /// Manage Site Setting
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the setting model</param>
        /// <returns></returns>
        public ResponseModel ManageNewsCategory(GridOperationEnums operation, NewsCategoryModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<NewsCategoryModel, NewsCategory>();
            NewsCategory newsCategory;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    newsCategory = GetById(model.Id);
                    newsCategory.Name = model.Name;
                    newsCategory.Description = model.Description;
                    newsCategory.RecordOrder = model.RecordOrder;

                    response = HierarchyUpdate(newsCategory);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::NewsCategories:::Update news category successfully")
                        : _localizedResourceServices.T("AdminModule:::NewsCategories:::Update news category failure"));

                case GridOperationEnums.Add:
                    newsCategory = Mapper.Map<NewsCategoryModel, NewsCategory>(model);

                    response = HierarchyInsert(newsCategory);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::NewsCategories:::Insert news category successfully")
                        : _localizedResourceServices.T("AdminModule:::NewsCategories:::Insert news category failure"));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::NewsCategories:::Delete news category successfully")
                        : _localizedResourceServices.T("AdminModule:::NewsCategories:::Delete news category failure"));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::NewsCategories:::news category not founded")
            };
        }

        #endregion

        /// <summary>
        /// search the NewsCategories.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchNewsCategories(JqSearchIn si)
        {
            var newsCategories = GetAll().Select(u => new NewsCategoryModel
            {
                Id = u.Id,
                Name = u.Name,
                Description = u.Description,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(newsCategories);
        }

        /// <summary>
        /// Get possible parent menu
        /// </summary>
        /// <param name="id">the current menu id</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPossibleParents(int? id = null)
        {
            var newsCategories = GetAll();
            int? parentId = null;
            var category = GetById(id);
            if (category != null)
            {
                parentId = category.ParentId;
                newsCategories = NewsCategoryRepository.GetPossibleParents(category);
            }
            var data = newsCategories.Select(m => new HierarchyModel
            {
                Id = m.Id,
                Name = m.Name,
                Hierarchy = m.Hierarchy,
                RecordOrder = m.RecordOrder,
                Selected = parentId.HasValue && parentId.Value == m.Id
            }).ToList();
            return NewsCategoryRepository.BuildSelectList(data, DefaultConstants.HierarchyLevelPrefix);
        }

        /// <summary>
        /// Get NewsCategory by parent id
        /// </summary>
        /// <param name="parentId">the parent id</param>
        /// <returns></returns>
        public List<NewsCategory> GetNewsCategories(int? parentId = null)
        {
            return Fetch(p => !parentId.HasValue || p.ParentId == parentId).OrderBy(p => p.RecordOrder).ToList();
        }

        /// <summary>
        /// Check if name is existed
        /// </summary>
        /// <param name="newsCategoryId">the category id</param>
        /// <param name="name">the category title</param>
        /// <returns></returns>
        public bool IsNameExisted(int? newsCategoryId, string name)
        {
            return Fetch(u => u.Name.Equals(name) && u.Id != newsCategoryId).Any();
        }
    }
}
