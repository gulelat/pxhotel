using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Templates;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.Templates;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class TemplatesController : AdminController
    {
        private readonly ITemplateServices _templateServices;
        public TemplatesController(ITemplateServices templateServices)
        {
            _templateServices = templateServices;
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
                var response = _templateServices.SaveTemplateManageModel(model);
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
        public ActionResult Edit(int? id, int? logId)
        {
            TemplateManageModel model = null;
            if (id.HasValue)
            {
                model = _templateServices.GetTemplateManageModel(id.Value);
            }
            else if (logId.HasValue)
            {
                model = _templateServices.GetTemplateManageModelByLogId(logId.Value);
            }
            if (model == null)
            {
                SetErrorMessage(LocalizedResourceServices.T("AdminModule:::Templates:::Messages:::ObjectNotFounded:::Template is not founded."));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(TemplateManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _templateServices.SaveTemplateManageModel(model);
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

        [HttpPost]
        public JsonResult Delete(int id)
        {
            return Json(_templateServices.DeleteTemplate(id));
        }

        #region Logs
        public ActionResult Logs(int id)
        {
            var model = _templateServices.GetLogs(id);
            if (model == null)
            {
                SetErrorMessage(LocalizedResourceServices.T("AdminModule:::Pages:::Messages:::ObjectNotFounded:::Page is not founded."));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult GetLogs(int id, int total, int index)
        {
            var model = _templateServices.GetLogs(id, total, index);
            var content = RenderPartialViewToString("_GetLogs", model);
            var response = new ResponseModel
            {
                Success = true,
                Data = new
                {
                    model.LoadComplete,
                    model.Total,
                    content
                }
            };
            return Json(response);
        }

        #endregion
    }
}
