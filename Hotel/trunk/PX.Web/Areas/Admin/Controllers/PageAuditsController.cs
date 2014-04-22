using System.Collections.Generic;
using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.PageAudits;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.PageAudits;
using PX.Business.Services.Users;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageUser })]
    public class PageAuditsController : AdminController
    {
        private readonly IUserServices _userServices;
        private readonly IPageAuditServices _pageAuditServices;
        public PageAuditsController(IUserServices userServices, IPageAuditServices pageAuditServices)
        {
            _userServices = userServices;
            _pageAuditServices = pageAuditServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_pageAuditServices.SearchPageAudits(si));
        }
    }
}
