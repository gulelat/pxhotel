using System.Web.Mvc;
using PX.Business.Mvc.Attributes;

namespace PX.Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }
    }
}
