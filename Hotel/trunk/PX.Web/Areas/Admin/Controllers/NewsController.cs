using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.News;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.News;
using PX.Business.Services.NewsCategories;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class NewsController : PxController
    {
        private readonly INewsServices _newsServices;
        private readonly INewsCategoryServices _newsCategoryServices;
        public NewsController(INewsServices newsServices, INewsCategoryServices newsCategoryServices)
        {
            _newsServices = newsServices;
            _newsCategoryServices = newsCategoryServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Ajax Methods

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_newsServices.SearchNews(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(NewsModel model, GridManagingModel manageModel)
        {
            return Json(_newsServices.ManageNews(manageModel.Operation, model));
        }

        public JsonResult GetStatus()
        {
            return Json(_newsServices.GetStatus(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create
        public ActionResult Create()
        {
            var model = _newsServices.GetNewsManageModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NewsManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _newsServices.SaveNewsManageModel(model);
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
            model.StatusList = _newsServices.GetStatus();
            model.NewsCategories = _newsCategoryServices.GetNewsCategories(model.Id);
            return View(model);
        }
        #endregion

        #region Edit

        public ActionResult Edit(int id)
        {
            var model = _newsServices.GetNewsManageModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(NewsManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _newsServices.SaveNewsManageModel(model);
                if (response.Success)
                {
                    var newsId = (int)response.Data;
                    SetSuccessMessage(response.Message);
                    switch (submit)
                    {
                        case SubmitTypeEnums.Save:
                            return RedirectToAction("Index");
                        default:
                            return RedirectToAction("Edit", new { id = newsId });
                    }
                }
                SetErrorMessage(response.Message);
            }
            model.StatusList = _newsServices.GetStatus();
            model.NewsCategories = _newsCategoryServices.GetNewsCategories(model.Id);
            return View(model);
        }
        #endregion
    }
}
