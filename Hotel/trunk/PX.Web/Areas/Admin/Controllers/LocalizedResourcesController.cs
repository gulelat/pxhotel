using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Localizes;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class LocalizedResourcesController : Controller
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public LocalizedResourcesController(ILocalizedResourceServices localizedResourceServices)
        {
            _localizedResourceServices = localizedResourceServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si, string language)
        {
            return JsonConvert.SerializeObject(_localizedResourceServices.SearchLocalizedResources(si, language));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(LocalizedResourceModel model, GridManagingModel manageModel)
        {
            return Json(_localizedResourceServices.ManageLocalizedResource(manageModel.Operation, model));
        }
    }
}
