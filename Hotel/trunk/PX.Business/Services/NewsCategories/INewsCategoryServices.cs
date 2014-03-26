using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.Testimonials;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.NewsCategories
{
    public interface INewsCategoryServices
    {
        #region Base

        IQueryable<EntityModel.NewsCategory> GetAll();
        IQueryable<EntityModel.NewsCategory> Fetch(Expression<Func<EntityModel.NewsCategory, bool>> expression);
        EntityModel.NewsCategory GetById(object id);
        ResponseModel Insert(EntityModel.NewsCategory newsCategory);
        ResponseModel Update(EntityModel.NewsCategory newsCategory);
        ResponseModel Delete(EntityModel.NewsCategory newsCategory);
        ResponseModel Delete(object id);

        #endregion

        ResponseModel ManageNewsCategory(GridOperationEnums operation, NewsCategoryModel model);

        JqGridSearchOut SearchNewsCategories(JqSearchIn si);

        bool IsNameExisted(int? newsCategoryId, string name);

        IEnumerable<SelectListItem> GetPossibleParents(int? id = null);

        List<NewsCategory> GetNewsCategories(int? parentId = null);
    }
}
