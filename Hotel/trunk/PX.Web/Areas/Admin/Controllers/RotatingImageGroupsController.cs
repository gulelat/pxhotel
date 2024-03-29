﻿using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.RotatingImageGroups;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.RotatingImageGroups;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class RotatingImageGroupsController : AdminController
    {
        private readonly IRotatingImageGroupServices _rotatingImageGroupServices;

        public RotatingImageGroupsController(IRotatingImageGroupServices rotatingImageGroupServices)
        {
            _rotatingImageGroupServices = rotatingImageGroupServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_rotatingImageGroupServices.SearchRotatingImageGroups(si));
        }

        public JsonResult GetGroups()
        {
            return Json(_rotatingImageGroupServices.GetRotatingImageGroups(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(RotatingImageGroupModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_rotatingImageGroupServices.ManageRotatingImageGroup(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        #region Edit Settings

        public ActionResult EditSettings(int id)
        {
            var model = _rotatingImageGroupServices.GetGroupManageSettingModel(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult EditSettings(GroupManageSettingModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _rotatingImageGroupServices.SaveGroupSettings(model);
                if (response.Success)
                {
                    SetSuccessMessage(response.Message);
                    switch (submit)
                    {
                        case SubmitTypeEnums.Save:
                            return RedirectToAction("Index");
                        default:
                            return RedirectToAction("EditSettings", new { id = model.Id });
                    }
                }
                SetErrorMessage(response.Message);
            }
            return View(model);
        }

        #endregion

        #region Rotating Image Gallery
        public ActionResult Gallery(int id)
        {
            var model = _rotatingImageGroupServices.GetGroupGallery(id);
            if(model == null)
            {
                SetErrorMessage(LocalizedResourceServices.T("AdminModule:::RotatingImageGroups:::Messages:::UpdateFailure:::Rotating Image Group not founded."));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult SortRotatingImages(GroupImageSortingModel model)
        {
            return Json(_rotatingImageGroupServices.SortImages(model));
        }
        #endregion
    }
}
