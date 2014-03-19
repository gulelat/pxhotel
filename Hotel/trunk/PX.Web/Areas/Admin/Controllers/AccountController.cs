using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PX.Business.Models.Users.Logins;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.Users;
using PX.Core.Framework.Mvc.Models;
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
            var path = Path.Combine(Server.MapPath("~/Images/uploads/"), avatar.FileName);
            var returnPath = string.Format("/Images/uploads/{0}", avatar.FileName);
            avatar.SaveAs(path);
            return Json(new ResponseModel
                {
                    Success = true,
                    Data = returnPath
                });
        }
        #endregion

        #region Settings
        [PxAuthorize]
        public ActionResult Settings()
        {
            return View();
        }
        #endregion
    }
}
