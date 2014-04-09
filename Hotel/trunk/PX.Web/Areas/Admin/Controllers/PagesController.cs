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
using PX.Core.Ultilities;
using PX.EntityModel;

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
            model.Positions = EnumUtilities.GetAllItemsFromEnum<PageEnums.PositionEnums>();
            model.RelativePages = _pageServices.GetRelativePages(model.Id, model.ParentId);
            model.StatusList = _pageServices.GetStatus();
            model.PageTemplates = _pageTemplateServices.GetPageTemplateSelectList(model.PageTemplateId);
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            var model = _pageServices.GetPageManageModel(id);
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
            model.Positions = EnumUtilities.GetAllItemsFromEnum<PageEnums.PositionEnums>();
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
    }
}
