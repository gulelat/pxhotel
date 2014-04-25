using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.SettingTypes;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.SettingTypes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class SettingTypesController : AdminController
    {
        private readonly ISettingTypeServices _settingTypeServices;
        public SettingTypesController(ISettingTypeServices settingTypeServices)
        {
            _settingTypeServices = settingTypeServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_settingTypeServices.SearchSettingTypes(si));
        }

        public JsonResult GetSettingTypes(int? id)
        {
            return Json(_settingTypeServices.GetSettingTypes(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(SettingTypeModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_settingTypeServices.ManageSettingType(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }
    }
}
