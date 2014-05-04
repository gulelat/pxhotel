using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Services;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.Services;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class ServicesController : AdminController
    {
        private readonly IServiceServices _serviceServices;
        public ServicesController(IServiceServices serviceServices)
        {
            _serviceServices = serviceServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        #region Ajax Methods

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_serviceServices.SearchService(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(ServiceModel model, GridManagingModel manageModel)
        {
            return Json(_serviceServices.ManageService(manageModel.Operation, model));
        }

        public JsonResult GetStatus()
        {
            return Json(_serviceServices.GetStatus(), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Create
        public ActionResult Create()
        {
            var model = _serviceServices.GetServiceManageModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(ServiceManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _serviceServices.SaveServiceManageModel(model);
                if (response.Success)
                {
                    var serviceId = (int)response.Data;
                    SetSuccessMessage(response.Message);
                    switch (submit)
                    {
                        case SubmitTypeEnums.Save:
                            return RedirectToAction("Index");
                        default:
                            return RedirectToAction("Edit", new { id = serviceId });
                    }
                }
                SetErrorMessage(response.Message);
            }
            model.StatusList = _serviceServices.GetStatus();
            return View(model);
        }
        #endregion

        #region Edit

        public ActionResult Edit(int id)
        {
            var model = _serviceServices.GetServiceManageModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(ServiceManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _serviceServices.SaveServiceManageModel(model);
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
            model.StatusList = _serviceServices.GetStatus();
            return View(model);
        }
        #endregion
    }
}
