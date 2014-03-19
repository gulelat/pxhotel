using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.UserGroups;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.Users;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize]
    public class UserGroupsController : Controller
    {
        private readonly IUserServices _userServices;
        public UserGroupsController(IUserServices userServices)
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
            return JsonConvert.SerializeObject(_userServices.SearchUserGroups(si));
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
        public JsonResult Manage(UserGroupModel model, GridManagingModel manageModel)
        {
            return Json(_userServices.ManageUserGroup(manageModel.Operation, model));
        }
    }
}
