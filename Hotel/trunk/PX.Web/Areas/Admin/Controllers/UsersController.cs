using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Users;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.Users;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new [] { PermissionEnums.ManageUser })]
    public class UsersController : AdminController
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        #region Listing

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_userServices.SearchUsers(si));
        }

        #region Ajax Methods

        public JsonResult GetStatus()
        {
            return Json(_userServices.GetStatus(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(UserModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_userServices.ManageUser(manageModel.Operation, model));                
            }

            return Json(new ResponseModel
                {
                    Success = false,
                    Message = GetFirstValidationResults(ModelState).Message
                });
        }
        #endregion

        public ActionResult Edit(int id)
        {
            var model = _userServices.GetUserManageModel(id);
            return View(model);
        }

        [HttpPost]
        public JsonResult Edit(UserManageModel user, HttpPostedFileBase avatar)
        {
            if(ModelState.IsValid)
                return Json(_userServices.SaveUserManageModel(user, avatar));
            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        #region Profiles
        public ActionResult Profiles(int id)
        {
            var model = _userServices.GetById(id);
            return View(model);
        }
        #endregion
    }
}
