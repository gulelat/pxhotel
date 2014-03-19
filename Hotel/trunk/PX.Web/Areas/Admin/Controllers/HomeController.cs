using System.Linq;
using System.Web.Mvc;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.Localizes;
using PX.Business.Services.Menus;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize]
    public class HomeController : Controller
    {
        private readonly IMenuServices _menuServices;
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public HomeController(IMenuServices menuServices, ILocalizedResourceServices localizedResourceServices)
        {
            _menuServices = menuServices;
            _localizedResourceServices = localizedResourceServices;
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
