using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.SettingTypes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.SettingTypes
{
    public interface ISettingTypeServices
    {
        #region Base

        IQueryable<SettingType> GetAll();
        IQueryable<SettingType> Fetch(Expression<Func<SettingType, bool>> expression);
        SettingType FetchFirst(Expression<Func<SettingType, bool>> expression);
        SettingType GetById(object id);
        ResponseModel Insert(SettingType settingType);
        ResponseModel Update(SettingType settingType);
        ResponseModel Delete(SettingType settingType);
        ResponseModel Delete(object id);

        #endregion

        #region Initialize

        void Initialize();
        #endregion

        #region Grid Search
        JqGridSearchOut SearchSettingTypes(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageSettingType(GridOperationEnums operation, SettingTypeModel model);

        #endregion
        
        IEnumerable<SelectListItem> GetSettingTypes(int? typeId);
    }
}
