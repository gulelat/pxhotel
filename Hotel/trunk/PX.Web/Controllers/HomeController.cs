using System.Web.Mvc;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Controllers;

namespace PX.Web.Controllers
{
    public class HomeController : ClientController
    {
        //
        // GET: /Home/
        [Template(Name = "Home Page Template")]
        public ActionResult Index(int? id)
        {
            return View("Index");
        }
    }
}
