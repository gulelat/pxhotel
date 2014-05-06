using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Pages;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.PageTemplates;
using PX.Business.Services.Pages;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class PagesController : AdminController
    {
        private readonly IPageServices _pageServices;
        private readonly IPageTemplateServices _pageTemplateServices;
        public PagesController(IPageServices pageServices, IPageTemplateServices pageTemplateServices)
        {
            _pageServices = pageServices;
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
            return JsonConvert.SerializeObject(_pageServices.SearchPages(si));
        }

        public JsonResult GetPageTemplates(int? id)
        {
            return Json(_pageTemplateServices.GetPageTemplateSelectList(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetParents(int? id)
        {
            return Json(_pageServices.GetPossibleParents(id), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetStatus(int? id)
        {
            return Json(_pageServices.GetStatus(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangeHomePage(int id)
        {
            return Json(_pageServices.ChangeHomePage(id), JsonRequestBehavior.AllowGet);
        }

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
        #endregion

        #endregion

        #region Create

        public ActionResult Create()
        {
            var model = _pageServices.GetPageManageModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(PageManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _pageServices.SavePageManageModel(model);
                if (response.Success)
                {
                    var pageId = (int)response.Data;
                    SetSuccessMessage(response.Message);
                    switch (submit)
                    {
                        case SubmitTypeEnums.Save:
                            return RedirectToAction("Index");
                        default:
                            return RedirectToAction("Edit", new { id = pageId });
                    }
                }
                SetErrorMessage(response.Message);
            }
            model.Parents = _pageTemplateServices.GetPossibleParents();
            model.Positions = EnumUtilities.GetSelectListFromEnum<PageEnums.PositionEnums>();
            model.RelativePages = _pageServices.GetRelativePages(model.Id, model.ParentId);
            model.StatusList = _pageServices.GetStatus();
            model.PageTemplates = _pageTemplateServices.GetPageTemplateSelectList(model.PageTemplateId);
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int? id, int? logId)
        {
            PageManageModel model = null;
            if (id.HasValue)
            {
                model = _pageServices.GetPageManageModel(id.Value);
            }
            else if(logId.HasValue)
            {
                model = _pageServices.GetPageManageModelByLogId(logId.Value);
            }
            if (model == null)
            {
                SetErrorMessage(LocalizedResourceServices.T("AdminModule:::Pages:::Messages:::ObjectNotFounded:::Page is not founded."));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(PageManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _pageServices.SavePageManageModel(model);
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
            model.Parents = _pageTemplateServices.GetPossibleParents();
            model.Positions = EnumUtilities.GetSelectListFromEnum<PageEnums.PositionEnums>();
            //TODO: check position to use here
            model.RelativePages = _pageServices.GetRelativePages(model.Id, model.ParentId);
            model.StatusList = _pageServices.GetStatus();
            model.PageTemplates = _pageTemplateServices.GetPageTemplateSelectList(model.PageTemplateId);
            return View(model);
        }

        #endregion

        public JsonResult GetRelativePages(int? id, int? parentId)
        {
            return Json(_pageServices.GetRelativePages(id, parentId));
        }

        #region Logs
        /// <summary>
        /// Manage page logs
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Logs(int id)
        {
            var model = _pageServices.GetLogs(id);
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
            var model = _pageServices.GetLogs(id, total, index);
            var content = RenderPartialViewToString("_GetLogs", model);
            var response = new ResponseModel
            {
                Success = true,
                Data = new
                {
                    model.LoadComplete,
                    content = content
                }
            };
            return Json(response);
        }

        #endregion
    }
}