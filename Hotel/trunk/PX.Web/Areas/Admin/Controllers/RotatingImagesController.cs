﻿using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.RotatingImages;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.RotatingImageGroups;
using PX.Business.Services.RotatingImages;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class RotatingImagesController : AdminController
    {
        private readonly IRotatingImageServices _rotatingImageServices;
        private readonly IRotatingImageGroupServices _rotatingImageGroupServices;
        public RotatingImagesController(IRotatingImageServices rotatingImageServices, IRotatingImageGroupServices rotatingImageGroupServices)
        {
            _rotatingImageServices = rotatingImageServices;
            _rotatingImageGroupServices = rotatingImageGroupServices;
        }

        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_rotatingImageServices.SearchRotatingImages(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(RotatingImageModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_rotatingImageServices.ManageRotatingImage(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        /// <summary>
        /// Delete the image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteImage(int id)
        {
            return Json(_rotatingImageServices.ManageRotatingImage( GridOperationEnums.Del, new RotatingImageModel
                {
                    Id = id
                }));
        }

        /// <summary>
        /// Update url of rotating image
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UpdateUrl(int id, string url)
        {
            return Json(_rotatingImageServices.UpdateRotatingImageUrl(id, url));
        }

        #region Create
        public ActionResult Create()
        {
            var model = _rotatingImageServices.GetRotatingImageManageModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(RotatingImageManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _rotatingImageServices.SaveRotatingImage(model);
                if (response.Success)
                {
                    var rotatingImageId = (int)response.Data;
                    SetSuccessMessage(response.Message);
                    switch (submit)
                    {
                        case SubmitTypeEnums.Save:
                            return RedirectToAction("Index");
                        default:
                            return RedirectToAction("Edit", new { id = rotatingImageId });
                    }
                }
                SetErrorMessage(response.Message);
            }
            model.Groups = _rotatingImageGroupServices.GetRotatingImageGroups();
            return View(model);
        }

        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            var model = _rotatingImageServices.GetRotatingImageManageModel(id);
            if (!model.Id.HasValue)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(RotatingImageManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _rotatingImageServices.SaveRotatingImage(model);
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
            model.Groups = _rotatingImageGroupServices.GetRotatingImageGroups();
            return View(model);
        }

        #endregion
    }
}
