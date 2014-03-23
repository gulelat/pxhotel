using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.PageTemplates;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Controllers;
using PX.Business.Mvc.Enums;
using PX.Business.Services.PageTemplates;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class PageTemplatesController : PxController
    {
        private readonly IPageTemplateServices _pageTemplateServices;
        public PageTemplatesController(IPageTemplateServices pageTemplateServices)
        {
            _pageTemplateServices = pageTemplateServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_pageTemplateServices.SearchPageTemplates(si));
        }

        #region Ajax Methods
        public JsonResult GetParents(int? id)
        {
            return Json(_pageTemplateServices.GetPossibleParents(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(PageTemplateModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_pageTemplateServices.ManagePageTemplate(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        public ActionResult Edit(int id)
        {
            var template = _pageTemplateServices.GetTemplate(id);
            if (!template.Id.HasValue)
            {
                return RedirectToAction("Index");
            }
            return View(template);
        }

        [HttpPost]
        public ActionResult Edit(PageTemplateManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _pageTemplateServices.SaveTemplates(model);
                if (response.Success)
                {
                    SetSuccessMessage(response.Message);
                    switch (submit)
                    {
                        case SubmitTypeEnums.Save:
                            return RedirectToAction("Edit", new { id = model.Id });
                        default:
                            return RedirectToAction("Edit", new { id = model.Id });
                    }
                }
                SetErrorMessage(response.Message);
            }
            model.Parents = _pageTemplateServices.GetPossibleParents(model.Id);
            return View(model);
        }
    }
}
