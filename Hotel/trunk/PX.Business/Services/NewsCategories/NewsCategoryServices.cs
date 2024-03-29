﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using PX.Business.Models.News.CurlyBrackets;
using PX.Business.Models.NewsCategories;
using PX.Business.Models.NewsCategories.CurlyBrackets;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;
using PX.EntityModel.Repositories.RepositoryBase.Models;

namespace PX.Business.Services.NewsCategories
{
    public class NewsCategorieservices : INewsCategoryServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly NewsCategoryRepository _newsCategoryRepository;
        public NewsCategorieservices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _newsCategoryRepository = new NewsCategoryRepository(entities);
        }

        #region Base
        public IQueryable<NewsCategory> GetAll()
        {
            return _newsCategoryRepository.GetAll();
        }
        public IQueryable<NewsCategory> Fetch(Expression<Func<NewsCategory, bool>> expression)
        {
            return _newsCategoryRepository.Fetch(expression);
        }
        public NewsCategory FetchFirst(Expression<Func<NewsCategory, bool>> expression)
        {
            return _newsCategoryRepository.FetchFirst(expression);
        }
        public NewsCategory GetById(object id)
        {
            return _newsCategoryRepository.GetById(id);
        }
        public ResponseModel Insert(NewsCategory newsCategory)
        {
            return _newsCategoryRepository.Insert(newsCategory);
        }
        public ResponseModel Update(NewsCategory newsCategory)
        {
            return _newsCategoryRepository.Update(newsCategory);
        }
        public ResponseModel HierarchyUpdate(NewsCategory newsCategory)
        {
            return _newsCategoryRepository.HierarchyUpdate(newsCategory);
        }
        public ResponseModel HierarchyInsert(NewsCategory newsCategory)
        {
            return _newsCategoryRepository.HierarchyInsert(newsCategory);
        }
        public ResponseModel Delete(NewsCategory newsCategory)
        {
            return _newsCategoryRepository.Delete(newsCategory);
        }
        public ResponseModel Delete(object id)
        {
            return _newsCategoryRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _newsCategoryRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search
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
                ParentId = u.ParentId,
                ParentName = u.NewsCategory1.Name,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(newsCategories);
        }

        #endregion

        #region Grid Manage

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
                    newsCategory.ParentId = model.ParentName.ToNullableInt();
                    newsCategory.Description = model.Description;
                    newsCategory.RecordOrder = model.RecordOrder;

                    response = HierarchyUpdate(newsCategory);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::NewsCategories:::Messages:::UpdateSuccessfully:::Update news category successfully.")
                        : _localizedResourceServices.T("AdminModule:::NewsCategories:::Messages:::UpdateFailure:::Update news category failed. Please try again later."));

                case GridOperationEnums.Add:
                    newsCategory = Mapper.Map<NewsCategoryModel, NewsCategory>(model);

                    response = HierarchyInsert(newsCategory);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::NewsCategories:::Messages:::CreateSuccessfully:::Create news category successfully.")
                        : _localizedResourceServices.T("AdminModule:::NewsCategories:::Messages:::CreateFailure:::Insert news category failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::NewsCategories:::Messages:::DeleteSuccessfully:::Delete news category successfully.")
                        : _localizedResourceServices.T("AdminModule:::NewsCategories:::Messages:::DeleteFailure:::Delete news category failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::NewsCategories:::Messages:::ObjectNotFounded:::News category is not founded.")
            };
        }

        #endregion

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
                newsCategories = _newsCategoryRepository.GetPossibleParents(category);
            }
            var data = newsCategories.Select(m => new HierarchyModel
            {
                Id = m.Id,
                Name = m.Name,
                Hierarchy = m.Hierarchy,
                RecordOrder = m.RecordOrder,
                Selected = parentId.HasValue && parentId.Value == m.Id
            }).ToList();
            return _newsCategoryRepository.BuildSelectList(data);
        }

        /// <summary>
        /// Get category model
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public CategoryItemModel GetCategoryModel(int categoryId)
        {
            var category = GetById(categoryId);
            if (category != null)
            {
                return new CategoryItemModel(category)
                {
                    NewsListing = category.NewsNewsCategories.Select(n => n.News)
                        .ToList()
                        .Select(n => new NewsCurlyBracket(n)).ToList()
                };
            }
            return null;
        }

        /// <summary>
        /// Get NewsCategory by parent id
        /// </summary>
        /// <param name="newsId"> the new id </param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetNewsCategories(int? newsId)
        {
            var data = GetAll().Select(c => new HierarchyModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Hierarchy = c.Hierarchy,
                    RecordOrder = c.RecordOrder,
                    Selected = newsId.HasValue && c.NewsNewsCategories.Any(nc => nc.NewsId == newsId)
                }).ToList();
            return _newsCategoryRepository.BuildSelectList(data);
        }

        /// <summary>
        /// Get news category listing
        /// </summary>
        /// <returns></returns>
        public CategoriesModel GetCategoryListing()
        {
            return new CategoriesModel
                {
                    Categories = GetAll().ToList().Select(c => new CategoryItemModel(c)).ToList()
                };
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
