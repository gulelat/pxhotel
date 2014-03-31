using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.News;
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

        ResponseModel ManageNews(GridOperationEnums operation, NewsModel model);

        JqGridSearchOut SearchNews(JqSearchIn si);

        NewsManageModel GetNewsManageModel(int? id = null);

        ResponseModel SaveNewsManageModel(NewsManageModel model);

        IEnumerable<SelectListItem> GetStatus();

        bool IsTitleExisted(int? newsId, string title);

    }
}
