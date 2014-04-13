using System;
using System.Collections.Generic;
using System.Globalization;
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
        public NewsServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _newsCategoryServices = HostContainer.GetInstance<INewsCategoryServices>();
        }

        #region Base
        public IQueryable<EntityModel.News> GetAll()
        {
            return NewsRepository.GetAll();
        }
        public IQueryable<EntityModel.News> Fetch(Expression<Func<EntityModel.News, bool>> expression)
        {
            return NewsRepository.Fetch(expression);
        }
        public EntityModel.News GetById(object id)
        {
            return NewsRepository.GetById(id);
        }
        public ResponseModel Insert(EntityModel.News news)
        {
            return NewsRepository.Insert(news);
        }
        public ResponseModel Update(EntityModel.News news)
        {
            return NewsRepository.Update(news);
        }
        public ResponseModel Delete(EntityModel.News news)
        {
            return NewsRepository.Delete(news);
        }
        public ResponseModel Delete(object id)
        {
            return NewsRepository.Delete(id);
        }
        #endregion

        #region Search Methods

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

        #region Manage Methods

        /// <summary>
        /// Manage user group
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the user group model</param>
        /// <returns></returns>
        public ResponseModel ManageNews(GridOperationEnums operation, NewsModel model)
        {
            IEnumerable<int> categoryIds;
            ResponseModel response;
            Mapper.CreateMap<NewsModel, EntityModel.News>();
            EntityModel.News news;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    news = NewsRepository.GetById(model.Id);
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
                            NewsNewsCategoryRepository.Delete(id);
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
                            NewsNewsCategoryRepository.Insert(newsNewsCategory);
                        }
                    }
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::News:::Update news successfully")
                        : _localizedResourceServices.T("AdminModule:::News:::Update news failure. Please try again later."));

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
                        NewsNewsCategoryRepository.Insert(newsNewsCategory);
                    }
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::News:::Insert news successfully")
                        : _localizedResourceServices.T("AdminModule:::News:::Insert news failure. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::News:::Delete news successfully")
                        : _localizedResourceServices.T("AdminModule:::News:::Delete news failure. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::News:::News not founded")
            };
        }

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
                var currentCategories = news.NewsNewsCategories.Select(nc => nc.Id).ToList();
                foreach (var id in currentCategories)
                {
                    if (!model.NewsCategoryIds.Contains(id))
                    {
                        NewsNewsCategoryRepository.Delete(id);
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
                        NewsNewsCategoryRepository.Insert(newsNewsCategory);
                    }
                }

                //Get page record order
                response = Update(news);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::News:::Update news successfully")
                    : _localizedResourceServices.T("AdminModule:::News:::Update news failure. Please try again later."));
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
                NewsNewsCategoryRepository.Insert(newsNewsCategory);
            }
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::News:::Create news successfully")
                : _localizedResourceServices.T("AdminModule:::News:::Create news failure. Please try again later."));
        }

        #endregion

        /// <summary>
        /// Get news status
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetStatus()
        {
            return EnumUtilities.GetAllItemsFromEnum<NewsEnums.NewsStatusEnums>();
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
