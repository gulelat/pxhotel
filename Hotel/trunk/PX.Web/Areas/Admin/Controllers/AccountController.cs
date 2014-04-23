using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PX.Business.Models.Users;
using PX.Business.Models.Users.Logins;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Mvc.WorkContext;
using PX.Business.Services.Users;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.Editable;

namespace PX.Web.Areas.Admin.Controllers
{
    public class AccountController : AdminController
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
        public ActionResult LoginForm(string returnUrl)
        {
            var model = new LoginModel { ReturnUrl = returnUrl };
            return PartialView("Login/_Login", model);
        }

        [HttpPost]
        public JsonResult LoginForm(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var response = _userServices.Login(model);
                if (response.Success)
                {
                    SetSuccessMessage(LocalizedResourceServices.T("AdminModule:::Account:::Messages:::LoginSuccess:::Login successfully."));
                }
                return Json(response);
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }
        #endregion

        #region Forgot Password

        [ChildActionOnly]
        public ActionResult ForgotPasswordForm()
        {
            var model = new ForgotPasswordModel();
            return PartialView("Login/_ForgotPassword", model);
        }

        [HttpPost]
        public JsonResult ForgotPasswordForm(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                return Json("");
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        #endregion

        #region Register
        [ChildActionOnly]
        public ActionResult RegisterForm()
        {
            var model = new RegisterModel();
            return PartialView("Login/_Register", model);
        }

        [HttpPost]
        public JsonResult RegisterForm(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                return Json("");
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        #endregion

        #region Log Out
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();

            var returnUrl = System.Web.HttpContext.Current.Request.UrlReferrer != null ? System.Web.HttpContext.Current.Request.UrlReferrer.AbsolutePath : string.Empty;
            return RedirectToAction("Login", new { returnUrl });
        }

        #endregion

        #region My Profile
        [PxAuthorize]
        public ActionResult MyProfile()
        {
            var model = _userServices.GetActiveUser(HttpContext.User.Identity.Name);
            return View(model);
        }

        [HttpPost]
        public JsonResult UploadAvatar(HttpPostedFileBase avatar)
        {
            return Json(_userServices.UploadAvatar(WorkContext.CurrentUser.Id, avatar));
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
            if (ModelState.IsValid)
            {
                return Json(_userServices.UpdateUserData(model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        [ChildActionOnly]
        public PartialViewResult ChangePassword()
        {
            var model = new ChangePasswordModel
                {
                    UserId = WorkContext.CurrentUser.Id
                };
            return PartialView("_ChangePassword", model);
        }

        [HttpPost]
        public JsonResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                return Json(_userServices.ChangePassword(model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }
    }
}
