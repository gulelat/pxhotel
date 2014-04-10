using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.SettingTypes;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.SettingTypes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class SettingTypesController : PxController
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

        public JsonResult GetSettingTypes()
        {
            return Json(_settingTypeServices.GetSettingTypes(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(SettingTypeModel model, GridManagingModel manageModel)
        {
            return Json(_settingTypeServices.ManageSettingType(manageModel.Operation, model));
        }
    }
}
