using System.Linq;
using System.Web.Mvc;
using PX.Business.Services.Menus;

namespace PX.Web.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IMenuServices MenuServices;
        public HomeController(IMenuServices menuServices)
        {
            MenuServices = menuServices;
        }

        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Menu()
        {
            var model = MenuServices.GetAll().ToList();
            return PartialView("_Menu", model);
        }
    }
}
