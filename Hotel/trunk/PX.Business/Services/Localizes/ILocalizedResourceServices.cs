using System.Linq;
using PX.Business.Models.Localizes;
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
        LocalizedResource GetById(object id);
        ResponseModel Insert(LocalizedResource localizedResource);
        ResponseModel Update(LocalizedResource localizedResource);
        ResponseModel Delete(LocalizedResource localizedResource);
        ResponseModel Delete(object id);

        #endregion

        ResponseModel ManageLocalizedResource(GridOperationEnums operation, LocalizedResourceModel model);

        JqGridSearchOut SearchLocalizedResources(JqSearchIn si, string language);

        /// <summary>
        /// Gets the localized string from a text key, if the value is not available then add the default value to the dictionary
        /// </summary>
        /// <param name="textKey">Key of the string to get localized</param>
        /// <param name="defaultValue">Default value of the string mapped to key</param>
        /// <param name="parameters">Parameters for passing to the default value string</param>
        /// <returns></returns>
        string GetLocalizedResource(string textKey, string defaultValue = null, params object[] parameters);

        /// <summary>
        /// Refresh dictionary
        /// </summary>
        void RefreshDictionary();
    }
}
