using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Banners;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.Banners;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class BannerController : AdminController
    {
        private readonly IBannerServices _bannerServices;
        public BannerController(IBannerServices bannerServices)
        {
            _bannerServices = bannerServices;
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
            return JsonConvert.SerializeObject(_bannerServices.SearchBanners(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(BannerModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_bannerServices.ManageBanner(manageModel.Operation, model));
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
            return Json(_bannerServices.ManageBanner( GridOperationEnums.Del, new BannerModel
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
            return Json(_bannerServices.UpdateBannerUrl(id, url));
        }

        #region Create
        public ActionResult Create()
        {
            var model = _bannerServices.GetBannerManageModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(BannerManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _bannerServices.SaveBanner(model);
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
            var template = _bannerServices.GetBannerManageModel(id);
            if (!template.Id.HasValue)
            {
                return RedirectToAction("Index");
            }
            return View(template);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(BannerManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _bannerServices.SaveBanner(model);
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
