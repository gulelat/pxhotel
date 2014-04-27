using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.News;
using PX.Business.Models.News.CurlyBrackets;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Business.Services.News
{
    public interface INewsServices
    {
        #region Base

        IQueryable<EntityModel.News> GetAll();
        IQueryable<EntityModel.News> Fetch(Expression<Func<EntityModel.News, bool>> expression);
        EntityModel.News GetById(object id);
        ResponseModel Insert(EntityModel.News news);
        ResponseModel Update(EntityModel.News news);
        ResponseModel Delete(EntityModel.News news);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchNews(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageNews(GridOperationEnums operation, NewsModel model);

        #endregion

        #region Manage

        NewsManageModel GetNewsManageModel(int? id = null);

        ResponseModel SaveNewsManageModel(NewsManageModel model);

        #endregion

        IEnumerable<SelectListItem> GetStatus();

        bool IsTitleExisted(int? newsId, string title);

        NewsCurlyBracket GetNews(int id);

        List<NewsCurlyBracket> GetNewsListing(int count);

        NewsListingModel GetNewsListing(int index, int pageSize);
    }
}
