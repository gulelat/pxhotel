using System.Web.Mvc;
using PX.Business.Mvc.Controllers;
using PX.Business.Mvc.WorkContext;

namespace PX.Web.Controllers
{
    public class HomeController : PxController
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            var user = WorkContext.CurrentUser;
            return View();
        }
    }
}
