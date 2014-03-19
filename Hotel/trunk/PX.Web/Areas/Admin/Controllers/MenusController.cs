using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Menus;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.Menus;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize]
    public class MenusController : Controller
    {
        private readonly IMenuServices _menuServices;
        public MenusController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_menuServices.SearchMenus(si));
        }

        #region Ajax Methods
        public JsonResult GetParents(int? id)
        {
            return Json(_menuServices.GetPossibleParents(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(MenuModel model, GridManagingModel manageModel)
        {
            return Json(_menuServices.ManageMenu(manageModel.Operation, model));
        }
    }
}
