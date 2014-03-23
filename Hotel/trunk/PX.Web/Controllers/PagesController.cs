using System.Web.Mvc;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.Pages;

namespace PX.Web.Controllers
{
    public class PagesController : PxController
    {
        private readonly IPageServices _pageServices;
        public PagesController(IPageServices pageServices)
        {
            _pageServices = pageServices;
        }
        //
        // GET: /Page/

        public ActionResult Index(string url)
        {
            var model = _pageServices.RenderContent(url);
            if (model == null) return new HttpNotFoundResult();
            return View(model);
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}
