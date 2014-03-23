using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Pages;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Controllers;
using PX.Business.Mvc.Enums;
using PX.Business.Services.PageTemplates;
using PX.Business.Services.Pages;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class PagesController : PxController
    {
        private readonly IPageServices _pageServices;
        private readonly IPageTemplateServices _pageTemplateServices;
        public PagesController(IPageServices pageServices, IPageTemplateServices pageTemplateServices)
        {
            _pageServices = pageServices;
            _pageTemplateServices = pageTemplateServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_pageServices.SearchPages(si));
        }

        #region Ajax Methods

        public JsonResult GetPageTemplates(int? id)
        {
            return Json(_pageTemplateServices.GetPageTemplateSelectList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParents(int? id)
        {
            return Json(_pageServices.GetPossibleParents(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStatus(int? id)
        {
            return Json(_pageServices.GetStatus(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(PageModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_pageServices.ManagePage(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }
    }
}
