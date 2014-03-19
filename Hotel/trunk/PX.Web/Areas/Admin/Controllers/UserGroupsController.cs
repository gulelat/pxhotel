using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.UserGroups;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.UserGroups;
using PX.Business.Services.Users;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageUser })]
    public class UserGroupsController : Controller
    {
        private readonly IUserServices _userServices;
        private readonly IUserGroupServices _userGroupServices;
        public UserGroupsController(IUserServices userServices, IUserGroupServices userGroupServices)
        {
            _userServices = userServices;
            _userGroupServices = userGroupServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_userGroupServices.SearchUserGroups(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(UserGroupModel model, GridManagingModel manageModel)
        {
            return Json(_userGroupServices.ManageUserGroup(manageModel.Operation, model));
        }
        
        public ActionResult Permissions(int id)
        {
            var model = _userGroupServices.GetPermissionSettings(id);
            return View(model);
        }

        [HttpPost]
        [ActionName("Permissions")]
        public ActionResult PermissionsPost(int id)
        {
            var ids = new List<int>();
            foreach (string key in Request.Form)
            {
                if (Request.Form[key].Equals("on"))
                {
                    ids.Add(int.Parse(key));
                }
            }

            return Json(_userGroupServices.SavePermissions(ids, id));
        }
    }
}
