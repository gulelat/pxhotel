using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.NewsCategories;
using PX.Business.Models.NewsCategories.CurlyBrackets;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.NewsCategories
{
    public interface INewsCategoryServices
    {
        #region Base

        IQueryable<NewsCategory> GetAll();
        IQueryable<NewsCategory> Fetch(Expression<Func<NewsCategory, bool>> expression);
        NewsCategory FetchFirst(Expression<Func<NewsCategory, bool>> expression);
        NewsCategory GetById(object id);
        ResponseModel Insert(NewsCategory newsCategory);
        ResponseModel Update(NewsCategory newsCategory);
        ResponseModel Delete(NewsCategory newsCategory);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchNewsCategories(JqSearchIn si);
        
        #endregion

        #region Grid Manage
        ResponseModel ManageNewsCategory(GridOperationEnums operation, NewsCategoryModel model);
        
        #endregion

        bool IsNameExisted(int? newsCategoryId, string name);

        IEnumerable<SelectListItem> GetPossibleParents(int? id = null);

        IEnumerable<SelectListItem> GetNewsCategories(int? newsId = null);

        CategoriesModel GetCategoryListing();

        CategoryItemModel GetCategoryModel(int categoryId);

    }
}
