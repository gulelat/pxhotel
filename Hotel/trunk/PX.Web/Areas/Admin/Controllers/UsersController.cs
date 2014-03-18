using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.DTO;
using PX.Business.Services.Users;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

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

        public ActionResult Login()
        {
            return View();
        }
    }
}
