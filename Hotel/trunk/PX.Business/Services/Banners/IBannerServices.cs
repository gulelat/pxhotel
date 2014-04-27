using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.Banners;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.Banners
{
    public interface IBannerServices
    {
        #region Base

        IQueryable<Banner> GetAll();
        IQueryable<Banner> Fetch(Expression<Func<Banner, bool>> expression);
        Banner FetchFirst(Expression<Func<Banner, bool>> expression);
        Banner GetById(object id);
        ResponseModel Insert(Banner banner);
        ResponseModel Update(Banner banner);
        ResponseModel Delete(Banner banner);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchBanners(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageBanner(GridOperationEnums operation, BannerModel model);

        #endregion

        BannerManageModel GetBannerManageModel(int? id = null);

        ResponseModel SaveBanner(BannerManageModel model);

        ResponseModel UpdateBannerUrl(int id, string url);
    }
}
