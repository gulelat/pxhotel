using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.Testimonials;
using PX.Business.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using AutoMapper;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.News
{
    public class NewsServices : INewsServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public NewsServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
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
            var news = GetAll().Select(u => new NewsModel
            {
                Id = u.Id,
                Title = u.Title,
                Description = u.Description,
                ImageFileName = u.ImageFileName,
                Content = u.Content,
                Status = u.Status,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

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
            ResponseModel response;
            Mapper.CreateMap<NewsModel, EntityModel.News>();
            EntityModel.News news;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    news = NewsRepository.GetById(model.Id);
                    news.Title = model.Title;
                    news.Status = model.Status;
                    news.Description = model.Description;
                    news.Content = model.Content;
                    news.RecordOrder = model.RecordOrder;
                    news.RecordActive = model.RecordActive;
                    response = Update(news);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::News:::Update news successfully")
                        : _localizedResourceServices.T("AdminModule:::News:::Update news failure"));
                
                case GridOperationEnums.Add:
                    news = Mapper.Map<NewsModel, EntityModel.News>(model);
                    response = Insert(news);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::News:::Insert news successfully")
                        : _localizedResourceServices.T("AdminModule:::News:::Insert news failure"));
                
                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::News:::Delete news successfully")
                        : _localizedResourceServices.T("AdminModule:::News:::Delete news failure"));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::News:::News not founded")
            };
        }

        #endregion

        /// <summary>
        /// Gets the News.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetNews()
        {
            return GetAll().ToList().Select(r => new SelectListItem
            {
                Text = r.Title,
                Value = r.Id.ToString(CultureInfo.InvariantCulture)
            });
        }
    }
}
