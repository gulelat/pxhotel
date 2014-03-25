using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.Menus;
using PX.Core.Configurations;
using PX.Core.Configurations.Constants;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize]
    public class HomeController : PxController
    {
        private readonly IMenuServices _menuServices;
        public HomeController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }

        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public ActionResult Menu()
        {
            var controller = ControllerContext.ParentActionViewContext.RouteData.Values["controller"].ToString();
            var action = ControllerContext.ParentActionViewContext.RouteData.Values["action"].ToString();
            var menu = _menuServices.GetAll().FirstOrDefault(m => m.Controller.Equals(controller) && m.Action.Equals(action));
            ViewBag.Hierarchy = menu != null ? menu.Hierarchy : string.Empty;
            var model = _menuServices.GetMenus();
            return PartialView("_Menu", model);
        }

        [ChildActionOnly]
        public PartialViewResult GetBreadCrumb()
        {
            var controller = ControllerContext.ParentActionViewContext.RouteData.Values["controller"].ToString();
            var action = ControllerContext.ParentActionViewContext.RouteData.Values["action"].ToString();
            var model = _menuServices.GetBreadCrumbs(controller, action);
            return PartialView("_Breadcrumb", model);
        }
    }
}
