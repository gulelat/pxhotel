using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.EmailAccounts;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.EmailAccounts;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class EmailAccountsController : AdminController
    {
        private readonly IEmailAccountServices _emailAccountServices;
        public EmailAccountsController(IEmailAccountServices emailAccountServices)
        {
            _emailAccountServices = emailAccountServices;
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
            return JsonConvert.SerializeObject(_emailAccountServices.SearchEmailAccounts(si));
        }

        [HttpPost]
        public JsonResult MarkAsDefault(int id)
        {
            return Json(_emailAccountServices.MarkAsDefault(id));
        }

        [HttpPost]
        public JsonResult SendTestEmail(TestEmailModel model)
        {
            if (ModelState.IsValid)
            {
                return Json(_emailAccountServices.SendTestEmail(model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        #region Create
        public ActionResult Create()
        {
            var model = _emailAccountServices.GetEmailAccountManageModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(EmailAccountManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _emailAccountServices.SaveEmailAccount(model);
                if (response.Success)
                {
                    var emailAccountId = (int)response.Data;
                    SetSuccessMessage(response.Message);
                    switch (submit)
                    {
                        case SubmitTypeEnums.Save:
                            return RedirectToAction("Index");
                        default:
                            return RedirectToAction("Edit", new { id = emailAccountId });
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
            var model = _emailAccountServices.GetEmailAccountManageModel(id);
            if (!model.Id.HasValue)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(EmailAccountManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _emailAccountServices.SaveEmailAccount(model);
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
