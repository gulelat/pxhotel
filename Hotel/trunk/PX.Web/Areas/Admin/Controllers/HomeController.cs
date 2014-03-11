using System.Linq;
using System.Web.Mvc;
using PX.Business.Services.Menus;

namespace PX.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
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

        public ActionResult Menu()
        {
            var model = _menuServices.GetAll().ToList();
            return PartialView("_Menu", model);
        }
    }
}
