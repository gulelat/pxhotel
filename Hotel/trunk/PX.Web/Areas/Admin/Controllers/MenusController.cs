using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.DTO;
using PX.Business.Services.Menus;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    public class MenusController : Controller
    {
        private readonly IMenuServices _menuServices;
        public MenusController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_menuServices.SearchMenus(si));
        }

        #region Ajax Methods
        public JsonResult GetParents(int? id)
        {
            return Json(_menuServices.GetPossibleParents(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(MenuDTO dto, GridManagingModel manageModel)
        {
            return Json(_menuServices.ManageMenu(manageModel.Operation, dto));
        }
    }
}
