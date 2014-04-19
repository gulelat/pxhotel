using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.NewsCategories;
using PX.Business.Models.Testimonials;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.NewsCategories;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class NewsCategoriesController : AdminController
    {
        private readonly INewsCategoryServices _newsCategorieservices;
        public NewsCategoriesController(INewsCategoryServices newsCategorieservices)
        {
            _newsCategorieservices = newsCategorieservices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_newsCategorieservices.SearchNewsCategories(si));
        }

        [HttpGet]
        public JsonResult GetParents(int? id)
        {
            return Json(_newsCategorieservices.GetPossibleParents(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetNewsCategories(int? id)
        {
            return Json(_newsCategorieservices.GetNewsCategories(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(NewsCategoryModel model, GridManagingModel manageModel)
        {
            return Json(_newsCategorieservices.ManageNewsCategory(manageModel.Operation, model));
        }
    }
}
