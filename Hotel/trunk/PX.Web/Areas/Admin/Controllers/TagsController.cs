using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Tags;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.Tags;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class TagsController : PxController
    {
        private readonly ITagServices _tagServices;
        public TagsController(ITagServices tagServices)
        {
            _tagServices = tagServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_tagServices.SearchTags(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(TagModel model, GridManagingModel manageModel)
        {
            return Json(_tagServices.ManageTag(manageModel.Operation, model));
        }
    }
}
