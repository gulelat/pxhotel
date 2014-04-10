using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Templates;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.CurlyBrackets;
using PX.Business.Services.Templates;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class TemplatesController : PxController
    {
        private readonly ITemplateServices _templateServices;
        private readonly ICurlyBracketServices _curlyBracketServices;
        public TemplatesController(ITemplateServices templateServices, ICurlyBracketServices curlyBracketServices)
        {
            _templateServices = templateServices;
            _curlyBracketServices = curlyBracketServices;
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
            return JsonConvert.SerializeObject(_templateServices.SearchTemplates(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(TemplateModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_templateServices.ManageTemplate(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }
        #endregion
        
        #endregion

        [HttpGet]
        public string _AjaxCurlyBracketBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_templateServices.SearchTemplates(si));
        }

        #region Create
        public ActionResult Create(string type)
        {
            var template = _templateServices.GetTemplateManageModel(type);
            return View(template);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(TemplateManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _templateServices.SaveTemplate(model);
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
            return View(model);
        }

        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            var template = _templateServices.GetTemplateManageModel(id);
            if (!template.Id.HasValue)
            {
                return RedirectToAction("Index");
            }
            return View(template);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TemplateManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _templateServices.SaveTemplate(model);
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
            return View(model);
        }

        #endregion
    }
}
