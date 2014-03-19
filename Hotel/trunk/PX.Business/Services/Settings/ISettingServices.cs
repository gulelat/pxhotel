using System.Linq;
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
        SiteSetting GetById(object id);
        ResponseModel Insert(SiteSetting siteSetting);
        ResponseModel Update(SiteSetting siteSetting);
        ResponseModel Delete(SiteSetting siteSetting);
        ResponseModel Delete(object id);

        #endregion

        ResponseModel ManageSiteSetting(GridOperationEnums operation, SiteSettingModel model);

        JqGridSearchOut SearchSiteSettings(JqSearchIn si);
    }
}
