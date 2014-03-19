using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Languages;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.Languages;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class LanguagesController : Controller
    {
        private readonly ILanguageServices _languageServices;
        public LanguagesController(ILanguageServices languageServices)
        {
            _languageServices = languageServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_languageServices.SearchLanguages(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(LanguageModel model, GridManagingModel manageModel)
        {
            return Json(_languageServices.ManageLanguage(manageModel.Operation, model));
        }
    }
}
