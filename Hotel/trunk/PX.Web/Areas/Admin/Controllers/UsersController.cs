using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Users;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.Users;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize]
    public class UsersController : Controller
    {
        private readonly IUserServices _userServices;
        public UsersController(IUserServices userServices)
        {
            _userServices = userServices;
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
        public JsonResult Manage(UserModel model, GridManagingModel manageModel)
        {
            return Json(_userServices.ManageUser(manageModel.Operation, model));
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
