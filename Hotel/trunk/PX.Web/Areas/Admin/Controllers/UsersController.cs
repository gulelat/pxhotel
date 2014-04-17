using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Users;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.UserGroups;
using PX.Business.Services.Users;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new [] { PermissionEnums.ManageUser })]
    public class UsersController : PxController
    {
        private readonly IUserServices _userServices;
        private readonly IUserGroupServices _userGroupServices;
        public UsersController(IUserServices userServices, IUserGroupServices userGroupServices)
        {
            _userServices = userServices;
            _userGroupServices = userGroupServices;
        }

        #region Listing & Manage User

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
        public JsonResult GetUserGroups(int? id)
        {
            return Json(_userGroupServices.GetUserGroups(id), JsonRequestBehavior.AllowGet);
        }

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

        public ActionResult Edit()
        {
            return View();
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
