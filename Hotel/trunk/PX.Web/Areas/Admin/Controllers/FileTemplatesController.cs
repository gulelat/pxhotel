using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.FileTemplates;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.FileTemplates;
using PX.Business.Services.PageTemplates;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class FileTemplatesController : AdminController
    {
        private readonly IFileTemplateServices _fileTemplateServices;
        private readonly IPageTemplateServices _pageTemplateServices;
        public FileTemplatesController(IFileTemplateServices fileTemplateServices, IPageTemplateServices pageTemplateServices)
        {
            _fileTemplateServices = fileTemplateServices;
            _pageTemplateServices = pageTemplateServices;
        }

        #region Listing File
        public ActionResult Index()
        {
            return View();
        }

        #region Ajax Methods

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_fileTemplateServices.SearchFileTemplates(si));
        }

        public JsonResult GetParents(int? id)
        {
            return Json(_fileTemplateServices.GetPossibleParents(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetPageTemplates(int? id)
        {
            return Json(_pageTemplateServices.GetPageTemplateSelectListForFileTemplate(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(FileTemplateModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_fileTemplateServices.ManageFileTemplate(manageModel.Operation, model));
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
            var template = _fileTemplateServices.GetTemplateManageModel();
            return View(template);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FileTemplateManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _fileTemplateServices.SaveFileTemplate(model);
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
            model.PageTemplates = _pageTemplateServices.GetPageTemplateSelectListForFileTemplate();
            model.Parents = _fileTemplateServices.GetPossibleParents();
            return View(model);
        }

        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            var model = _fileTemplateServices.GetTemplateManageModel(id);
            if (!model.Id.HasValue)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(FileTemplateManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _fileTemplateServices.SaveFileTemplate(model);
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
            model.PageTemplates = _pageTemplateServices.GetPageTemplateSelectListForFileTemplate(model.Id);
            model.Parents = _fileTemplateServices.GetPossibleParents(model.Id);
            return View(model);
        }

        #endregion
    }
}
