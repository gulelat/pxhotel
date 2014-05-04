using System.Web.Mvc;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.News;
using PX.Business.Services.NewsCategories;

namespace PX.Web.Controllers
{
    public class NewsCategoryController : ClientController
    {
        private readonly INewsServices _newsServices;
        private readonly INewsCategoryServices _newsCategoryServices;
        public NewsCategoryController(INewsServices newsServices, INewsCategoryServices newsCategoryServices)
        {
            _newsServices = newsServices;
            _newsCategoryServices = newsCategoryServices;
        }

        public ActionResult Details(int id)
        {
            var model = _newsCategoryServices.GetCategoryModel(id);
            if(model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
    }
}
