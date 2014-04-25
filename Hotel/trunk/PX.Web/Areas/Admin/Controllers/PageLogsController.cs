using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.PageLogs;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageUser })]
    public class PageLogsController : AdminController
    {
        private readonly IPageLogServices _pageLogServices;
        public PageLogsController(IPageLogServices pageLogServices)
        {
            _pageLogServices = pageLogServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_pageLogServices.SearchPageLogs(si));
        }
    }
}
