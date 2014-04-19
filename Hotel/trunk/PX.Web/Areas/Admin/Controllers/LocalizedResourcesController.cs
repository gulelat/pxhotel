using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.LocalizedResources;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.Languages;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class LocalizedResourcesController : AdminController
    {
        private readonly ILanguageServices _languageServices;
        public LocalizedResourcesController(ILanguageServices languageServices)
        {
            _languageServices = languageServices;
        }

        public ActionResult Index(string language)
        {
            var model = _languageServices.GetById(language);
            return View(model);
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si, string language)
        {
            return JsonConvert.SerializeObject(LocalizedResourceServices.SearchLocalizedResources(si, language));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(LocalizedResourceModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(LocalizedResourceServices.ManageLocalizedResource(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }
    }
}
