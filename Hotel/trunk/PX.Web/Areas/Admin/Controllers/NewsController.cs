using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Testimonials;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Controllers;
using PX.Business.Mvc.Enums;
using PX.Business.Services.News;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class NewsController : PxController
    {
        private readonly INewsServices _NewsServices;
        public NewsController(INewsServices NewsServices)
        {
            _NewsServices = NewsServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_NewsServices.SearchNews(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(NewsModel model, GridManagingModel manageModel)
        {
            return Json(_NewsServices.ManageNews(manageModel.Operation, model));
        }
    }
}
