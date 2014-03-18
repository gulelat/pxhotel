using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.DTO;
using PX.Business.Services.Users;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Web.Areas.Admin.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
        }

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
        public JsonResult GetRoles()
        {
            return Json(_userServices.GetRoles(), JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetStatus()
        {
            return Json(_userServices.GetStatus(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(UserDTO model, GridManagingModel manageModel)
        {
            return Json(_userServices.ManageUser(manageModel.Operation, model));
        }

        public ActionResult Edit()
        {
            return View();
        }

        #region My Profile
        public ActionResult Profile(int? id)
        {
            if (!id.HasValue)
            {
                id = 4;
                //id = EntityModel.User.CurrentUser.Id;
            }
            var model = _userServices.GetById(id);
            return View(model);
        }
        #endregion

        #region Login

        public ActionResult Login()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult LoginForm()
        {
            return PartialView("_Login");
        }

        [HttpPost]
        public JsonResult LoginForm(User model)
        {
            return Json("");
        }

        [ChildActionOnly]
        public ActionResult ForgotPasswordForm()
        {
            return PartialView("_ForgotPassword");
        }

        [HttpPost]
        public JsonResult ForgotPasswordForm(User model)
        {
            return Json("");
        }

        [ChildActionOnly]
        public ActionResult RegisterForm()
        {
            return PartialView("_Register");
        }

        [HttpPost]
        public JsonResult RegisterForm(User model)
        {
            return Json("");
        }

        #endregion
    }
}
