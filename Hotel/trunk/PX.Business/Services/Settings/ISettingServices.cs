using System;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.Settings;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.Settings
{
    public interface ISettingServices
    {
        #region Base

        IQueryable<SiteSetting> GetAll();
        IQueryable<SiteSetting> Fetch(Expression<Func<SiteSetting, bool>> expression);
        SiteSetting FetchFirst(Expression<Func<SiteSetting, bool>> expression);
        SiteSetting GetById(object id);
        ResponseModel Insert(SiteSetting siteSetting);
        ResponseModel Update(SiteSetting siteSetting);
        ResponseModel Delete(SiteSetting siteSetting);
        ResponseModel Delete(object id);

        #endregion

        #region Initialize

        void Initialize();
        #endregion

        #region Grid Search
        JqGridSearchOut SearchSiteSettings(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageSiteSetting(GridOperationEnums operation, SiteSettingModel model);

        #endregion

        #region Manage

        SiteSettingManageModel GetSettingManageModel(int id);

        ResponseModel SaveSettingManageModel(SiteSettingManageModel model, NameValueCollection data);

        #endregion

        T GetSetting<T>(int id);

        T GetSetting<T>(string key);

        T GetSetting<T>(string key, T defaultValue);

        dynamic LoadSetting<T>(object[] parameterArray = null);
    }
}
