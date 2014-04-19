using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.LocalizedResources;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.Localizes
{
    public interface ILocalizedResourceServices
    {
        #region Base

        IQueryable<LocalizedResource> GetAll();
        IQueryable<LocalizedResource> Fetch(Expression<Func<LocalizedResource, bool>> expression);
        LocalizedResource GetById(object id);
        ResponseModel Insert(LocalizedResource localizedResource);
        ResponseModel Update(LocalizedResource localizedResource);
        ResponseModel Delete(LocalizedResource localizedResource);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchLocalizedResources(JqSearchIn si, string language);

        #endregion

        #region Manage Grid
        ResponseModel ManageLocalizedResource(GridOperationEnums operation, LocalizedResourceModel model);

        #endregion

        #region Initialize

        /// <summary>
        /// Refresh dictionary
        /// </summary>
        void RefreshDictionary();

        #endregion

        /// <summary>
        /// Gets the localized string from a text key, if the value is not available then add the default value to the dictionary
        /// </summary>
        /// <param name="textKey">Key of the string to get localized</param>
        /// <param name="defaultValue">Default value of the string mapped to key</param>
        /// <param name="parameters">Parameters for passing to the default value string</param>
        /// <returns></returns>
        string GetLocalizedResource(string textKey, string defaultValue = null, params object[] parameters);

        string T(string textKey);

        string T(string textKey, string defaultValue);
    }
}
