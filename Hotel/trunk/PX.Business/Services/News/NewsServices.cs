using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.News;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.NewsCategories;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using AutoMapper;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.News
{
    public class NewsServices : INewsServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly INewsCategoryServices _newsCategoryServices;
        private readonly NewsRepository _newsRepository;
        public NewsServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _newsCategoryServices = HostContainer.GetInstance<INewsCategoryServices>();
            _newsRepository = new NewsRepository();
        }

        #region Base
        public IQueryable<EntityModel.News> GetAll()
        {
            return _newsRepository.GetAll();
        }
        public IQueryable<EntityModel.News> Fetch(Expression<Func<EntityModel.News, bool>> expression)
        {
            return _newsRepository.Fetch(expression);
        }
        public EntityModel.News GetById(object id)
        {
            return _newsRepository.GetById(id);
        }
        public ResponseModel Insert(EntityModel.News news)
        {
            return _newsRepository.Insert(news);
        }
        public ResponseModel Update(EntityModel.News news)
        {
            return _newsRepository.Update(news);
        }
        public ResponseModel Delete(EntityModel.News news)
        {
            return _newsRepository.Delete(news);
        }
        public ResponseModel Delete(object id)
        {
            return _newsRepository.Delete(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the user groups.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchNews(JqSearchIn si)
        {
            var news = GetAll().ToList().Select(u => new NewsModel
            {
                Id = u.Id,
                Title = u.Title,
                Description = u.Description,
                ImageUrl = u.ImageUrl,
                Content = u.Content,
                Status = u.Status,
                Categories = string.Join(",", u.NewsNewsCategories.Select(c => c.NewsCategory.Name)),
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            }).AsQueryable();

            return si.Search(news);
        }

        #endregion

        #region Grid Manage

        /// <summary>
        /// Manage user group
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the user group model</param>
        /// <returns></returns>
        public ResponseModel ManageNews(GridOperationEnums operation, NewsModel model)
        {
            var newsNewsCategoryRepository = new NewsNewsCategoryRepository();
            IEnumerable<int> categoryIds;
            ResponseModel response;
            Mapper.CreateMap<NewsModel, EntityModel.News>();
            EntityModel.News news;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    news = _newsRepository.GetById(model.Id);
                    news.Title = model.Title;
                    news.Status = model.Status;
                    news.RecordOrder = model.RecordOrder;
                    news.RecordActive = model.RecordActive;
                    response = Update(news);
                    categoryIds = model.Categories.Split(',').Select(int.Parse).ToList();
                    var currentCategories = news.NewsNewsCategories.Select(nc => nc.Id).ToList();
                    foreach (var id in currentCategories)
                    {
                        if(!categoryIds.Contains(id))
                        {
                            newsNewsCategoryRepository.Delete(id);
                        }
                    }
                    foreach (var categoryId in categoryIds)
                    {
                        if (currentCategories.All(n => n != categoryId))
                        {
                            var newsNewsCategory = new NewsNewsCategory
                            {
                                NewsId = news.Id,
                                NewsCategoryId = categoryId
                            };
                            newsNewsCategoryRepository.Insert(newsNewsCategory);
                        }
                    }
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::News:::Messages:::UpdateSuccessfully:::Update news successfully.")
                        : _localizedResourceServices.T("AdminModule:::News:::Messages:::UpdateFailure:::Update news failed. Please try again later."));

                case GridOperationEnums.Add:
                    news = Mapper.Map<NewsModel, EntityModel.News>(model);
                    categoryIds = model.Categories.Split(',').Select(int.Parse);
                    news.Status = model.Status;
                    news.Content = string.Empty;
                    news.Description = string.Empty;
                    response = Insert(news);
                    foreach (var categoryId in categoryIds)
                    {
                        var newsNewsCategory = new NewsNewsCategory
                            {
                                NewsId = news.Id,
                                NewsCategoryId = categoryId
                            };
                        newsNewsCategoryRepository.Insert(newsNewsCategory);
                    }
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::News:::Messages:::CreateSuccessfully:::Create news successfully.")
                        : _localizedResourceServices.T("AdminModule:::News:::Messages:::CreateFailure:::Insert news failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::News:::Messages:::DeleteSuccessfully:::Delete news successfully.")
                        : _localizedResourceServices.T("AdminModule:::News:::Messages:::DeleteFailure:::Delete news failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::News:::Messages:::ObjectNotFounded:::News is not founded.")
            };
        }

        #endregion

        #region Manage

        /// <summary>
        /// Get news manage model by id
        /// </summary>
        /// <param name="id">the news id</param>
        /// <returns></returns>
        public NewsManageModel GetNewsManageModel(int? id = null)
        {
            var news = GetById(id);
            if (news != null)
            {
                return new NewsManageModel
                {
                    Id = news.Id,
                    Description = news.Description,
                    Content = news.Content,
                    ImageUrl = news.ImageUrl,
                    Title = news.Title,
                    Status = news.Status,
                    StatusList = GetStatus(),
                    NewsCategories = _newsCategoryServices.GetNewsCategories(news.Id),
                    RecordOrder = news.RecordOrder,
                    RecordActive = news.RecordActive
                };
            }
            return new NewsManageModel
            {
                StatusList = GetStatus(),
                NewsCategories = _newsCategoryServices.GetNewsCategories(),
            };
        }

        /// <summary>
        /// Save news manage model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SaveNewsManageModel(NewsManageModel model)
        {
            var newsNewsCategoryRepository = new NewsNewsCategoryRepository();
            ResponseModel response;
            var news = GetById(model.Id);

            #region Edit News
            if (news != null)
            {
                news.Title = model.Title;

                news.Status = model.Status;
                news.Description = model.Description;
                news.Content = model.Content;
                news.ImageUrl = model.ImageUrl;
                var currentCategories = news.NewsNewsCategories.Select(nc => nc.NewsCategoryId).ToList();
                foreach (var id in currentCategories)
                {
                    if (!model.NewsCategoryIds.Contains(id))
                    {
                        newsNewsCategoryRepository.Delete(id);
                    }
                }
                foreach (var categoryId in model.NewsCategoryIds)
                {
                    if (currentCategories.All(n => n != categoryId))
                    {
                        var newsNewsCategory = new NewsNewsCategory
                        {
                            NewsId = news.Id,
                            NewsCategoryId = categoryId
                        };
                        newsNewsCategoryRepository.Insert(newsNewsCategory);
                    }
                }

                //Get page record order
                response = Update(news);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::News:::Messages:::UpdateSuccessfully:::Update news successfully.")
                    : _localizedResourceServices.T("AdminModule:::News:::Messages:::UpdateFailure:::Update news failed. Please try again later."));
            }
            #endregion

            news = new EntityModel.News
            {
                Title = model.Title,
                Status = model.Status,
                Description = model.Description,
                Content = model.Content,
                ImageUrl = model.ImageUrl
            };

            response = Insert(news);
            foreach (var categoryId in model.NewsCategoryIds)
            {
                var newsNewsCategory = new NewsNewsCategory
                {
                    NewsId = news.Id,
                    NewsCategoryId = categoryId
                };
                newsNewsCategoryRepository.Insert(newsNewsCategory);
            }
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::News:::Messages:::CreateSuccessfully:::Create news successfully.")
                : _localizedResourceServices.T("AdminModule:::News:::Messages:::CreateFailure:::Create news failed. Please try again later."));
        }
        #endregion

        /// <summary>
        /// Get news status
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetStatus()
        {
            return EnumUtilities.GetSelectListFromEnum<NewsEnums.NewsStatusEnums>();
        }

        /// <summary>
        /// Check if title is existed
        /// </summary>
        /// <param name="newsId">the news id</param>
        /// <param name="title">the news title</param>
        /// <returns></returns>
        public bool IsTitleExisted(int? newsId, string title)
        {
            return Fetch(u => u.Title.Equals(title) && u.Id != newsId).Any();
        }
    }
}
