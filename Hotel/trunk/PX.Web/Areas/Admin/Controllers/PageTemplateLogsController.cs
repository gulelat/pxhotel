using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.PageTemplateLogs;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class PageTemplateLogsController : AdminController
    {
        private readonly IPageTemplateLogServices _pageTemplateLogServices;
        public PageTemplateLogsController(IPageTemplateLogServices pageTemplateLogServices)
        {
            _pageTemplateLogServices = pageTemplateLogServices;
        }

        #region Listing Page
        public ActionResult Index()
        {
            return View();
        }

        #region Ajax Methods

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_pageTemplateLogServices.SearchPageTemplateLogs(si));
        }
        #endregion

        #endregion
    }
}
