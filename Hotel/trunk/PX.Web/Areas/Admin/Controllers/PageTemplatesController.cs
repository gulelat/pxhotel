using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.PageTemplates;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.PageTemplates;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class PageTemplatesController : AdminController
    {
        private readonly IPageTemplateServices _pageTemplateServices;
        public PageTemplatesController(IPageTemplateServices pageTemplateServices)
        {
            _pageTemplateServices = pageTemplateServices;
        }

        #region Listing Page
        public ActionResult Index()
        {
            return View();
        }

        #region Ajax Methods

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_pageTemplateServices.SearchPageTemplates(si));
        }

        public JsonResult GetParents(int? id)
        {
            return Json(_pageTemplateServices.GetPossibleParents(id), JsonRequestBehavior.AllowGet);
        }

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
        #endregion
        
        #endregion

        #region Create
        public ActionResult Create()
        {
            var template = _pageTemplateServices.GetTemplateManageModel();
            return View(template);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PageTemplateManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _pageTemplateServices.SavePageTemplate(model);
                if (response.Success)
                {
                    var templateId = (int)response.Data;
                    SetSuccessMessage(response.Message);
                    switch (submit)
                    {
                        case SubmitTypeEnums.Save:
                            return RedirectToAction("Index");
                        default:
                            return RedirectToAction("Edit", new { id = templateId });
                    }
                }
                SetErrorMessage(response.Message);
            }
            model.Parents = _pageTemplateServices.GetPossibleParents();
            return View(model);
        }

        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            var template = _pageTemplateServices.GetTemplateManageModel(id);
            if (!template.Id.HasValue)
            {
                return RedirectToAction("Index");
            }
            return View(template);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PageTemplateManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _pageTemplateServices.SavePageTemplate(model);
                if (response.Success)
                {
                    SetSuccessMessage(response.Message);
                    switch (submit)
                    {
                        case SubmitTypeEnums.Save:
                            return RedirectToAction("Index");
                        default:
                            return RedirectToAction("Edit", new { id = model.Id });
                    }
                }
                SetErrorMessage(response.Message);
            }
            model.Parents = _pageTemplateServices.GetPossibleParents(model.Id);
            return View(model);
        }

        #endregion

        public ActionResult Logs(int id)
        {
            var model = _pageTemplateServices.GetLogs(id);
            if (model == null)
            {
                SetErrorMessage(LocalizedResourceServices.T("AdminModule:::Pages:::Messages:::ObjectNotFounded:::Page is not founded."));
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
