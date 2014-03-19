using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Settings;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.Settings;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class SiteSettingsController : Controller
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
            return Json(_settingServices.ManageSiteSetting(manageModel.Operation, model));
        }
    }
}
