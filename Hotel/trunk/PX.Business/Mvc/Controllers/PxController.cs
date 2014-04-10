using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PX.Business.Mvc.Attributes;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Configurations.Constants;
using PX.Core.Framework.Mvc.Models;

namespace PX.Business.Mvc.Controllers
{
    [Internationalization]
    public class PxController : Controller
    {
        public readonly ILocalizedResourceServices LocalizedResourceServices;
        public PxController()
        {
            LocalizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Protected Methods

        /// <summary>
        /// Creates the temp data provider. Push current controller to Temp data
        /// </summary>
        /// <returns></returns>
        protected override ITempDataProvider CreateTempDataProvider()
        {
            ITempDataProvider iTempDataProvider = base.CreateTempDataProvider();
            HttpContext.Items[DefaultConstants.PxHotelCurrentController] = this;
            return iTempDataProvider;
        }
        #endregion

        #region Validation
        public List<ValidationResultModel> GetAllValidationResults(ModelStateDictionary modelState)
        {
            if (modelState.IsValid) return null;
            var result = new List<ValidationResultModel>();
            foreach (var key in modelState.Keys)
            {
                if(modelState[key].Errors.Any())
                {
                    result.Add(new ValidationResultModel
                    {
                        PropertyName = key,
                        Message = string.Join(",", modelState[key].Errors.Select(e => e.ErrorMessage))
                    });
                }
            }
            return result;
        }

        public ValidationResultModel GetFirstValidationResults(ModelStateDictionary modelState)
        {
            return GetAllValidationResults(modelState).FirstOrDefault();
        }
        #endregion

        #region Message
        public void SetErrorMessage(string message)
        {
            TempData[DefaultConstants.ErrorMessage] = message;
        }
        public void SetSuccessMessage(string message)
        {
            TempData[DefaultConstants.SuccessMessage] = message;
        }
        public void SetWarningMessage(string message)
        {
            TempData[DefaultConstants.WarningMessage] = message;
        }
        #endregion
    }
}
