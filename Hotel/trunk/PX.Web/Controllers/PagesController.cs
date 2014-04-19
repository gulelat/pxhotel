using System.Web.Mvc;
using PX.Business.Mvc.Controllers;
using PX.Business.Mvc.ViewEngines.ViewResult;
using PX.Business.Services.Pages;

namespace PX.Web.Controllers
{
    public class PagesController : ClientController
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
            if(model.IsFileTemplate)
            {
                var routeValues = new
                {
                    controller = model.FileTemplateModel.Controller,
                    action = model.FileTemplateModel.Action
                };
                return new MVCTransferResult(routeValues, model.FileTemplateModel.Parameters);
            }
            return View(model);
        }
    }
}
