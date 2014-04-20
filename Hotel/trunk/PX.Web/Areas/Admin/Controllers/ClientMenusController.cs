using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.ClientMenus;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.ClientMenus;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class ClientMenusController : AdminController
    {
        private readonly IClientMenuServices _clientMenuServices;
        public ClientMenusController(IClientMenuServices clientMenuServices)
        {
            _clientMenuServices = clientMenuServices;
        }

        #region Listing
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_clientMenuServices.SearchClientMenus(si));
        }

        #region Ajax Methods
        public JsonResult GetParents(int? id)
        {
            return Json(_clientMenuServices.GetPossibleParents(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #endregion

        #region Grid Manage
        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(ClientMenuModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_clientMenuServices.ManageClientMenu(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        #endregion

        #region Create

        public ActionResult Create()
        {
            var model = _clientMenuServices.GetClientMenuManageModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ClientMenuManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _clientMenuServices.SaveClientMenuManageModel(model);
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
            model.Parents = _clientMenuServices.GetPossibleParents();
            return View(model);
        }
        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            var model = _clientMenuServices.GetClientMenuManageModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ClientMenuManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _clientMenuServices.SaveClientMenuManageModel(model);
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
            model.Parents = _clientMenuServices.GetPossibleParents();
            return View(model);
        }

        #endregion

        public JsonResult GetRelativeMenus(int? id, int? parentId)
        {
            return Json(_clientMenuServices.GetRelativeMenus(id, parentId));
        }
    }
}
