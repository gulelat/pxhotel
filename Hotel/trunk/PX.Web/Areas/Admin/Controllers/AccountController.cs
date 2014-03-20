using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PX.Business.Models.Users;
using PX.Business.Models.Users.Logins;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.Users;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.Editable;
using PX.EntityModel;

namespace PX.Web.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserServices _userServices;
        public AccountController(IUserServices userServices)
        {
            _userServices = userServices;
        }
        
        #region Login

        public ActionResult Login()
        {
            return View();
        }

        [PxAuthorize]
        public ActionResult LoginSuccess()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult LoginForm()
        {
            var model = new LoginModel();
            return PartialView("Login/_Login", model);
        }

        [HttpPost]
        public JsonResult LoginForm(LoginModel model)
        {
            return Json(_userServices.Login(model));
        }
        #endregion

        #region Forgot Password

        [ChildActionOnly]
        public ActionResult ForgotPasswordForm()
        {
            return PartialView("Login/_ForgotPassword");
        }

        [HttpPost]
        public JsonResult ForgotPasswordForm(User model)
        {
            return Json("");
        }

        #endregion

        #region Register
        [ChildActionOnly]
        public ActionResult RegisterForm()
        {
            return PartialView("Login/_Register");
        }

        [HttpPost]
        public JsonResult RegisterForm(User model)
        {
            return Json("");
        }

        #endregion

        #region Sign Out
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }
        
        #endregion

        #region My Profile
        [PxAuthorize]
        public ActionResult MyProfile()
        {
            var model = _userServices.GetById(EntityModel.User.CurrentUser.Id);
            return View(model);
        }

        [HttpPost]
        public JsonResult UploadAvatar(HttpPostedFileBase avatar)
        {
            var response = _userServices.UploadAvatar(EntityModel.User.CurrentUser.Id, avatar);
            return Json(response);
        }
        #endregion

        #region Settings
        [PxAuthorize]
        public ActionResult Settings()
        {
            return View();
        }
        #endregion

        [HttpPost]
        public JsonResult UpdateUserData(XEditableModel model)
        {
            var response = _userServices.UpdateUserData(model);
            return Json(response);
        }

        [ChildActionOnly]
        public PartialViewResult ChangePassword()
        {
            var model = new ChangePasswordModel
                {
                    UserId = EntityModel.User.CurrentUser.Id
                };
            return PartialView("_ChangePassword", model);
        }

        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordModel model)
        {
            return Json(new ResponseModel
                {
                    Success = true
                });
        }
    }
}
