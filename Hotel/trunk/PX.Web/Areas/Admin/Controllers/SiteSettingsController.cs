using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using PX.Business.Models.Settings;
using PX.Business.Models.Settings.SettingTypes;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.Settings;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class SiteSettingsController : PxController
    {
        private readonly ISettingServices _settingServices;
        public SiteSettingsController(ISettingServices settingServices)
        {
            _settingServices = settingServices;
        }

        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_settingServices.SearchSiteSettings(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(SiteSettingModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_settingServices.ManageSiteSetting(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        #region Edit Setting
        public ActionResult Edit(int id)
        {
            var model = _settingServices.GetSettingManageModel(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(SiteSettingManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _settingServices.SaveSettingManageModel(model, Request.Form);
                if (response.Success)
                {
                    var templateId = model.Id;
                    SetSuccessMessage(response.Message);
                    switch (submit)
                    {
                        case SubmitTypeEnums.Save:
                            return RedirectToAction("Index");
                        default:
                            return RedirectToAction("Edit", new { id = templateId });
                    }
                }
                SetErrorMessage(response.Message);
            }
            return View(model);
        }
        
        #endregion
    }
}
