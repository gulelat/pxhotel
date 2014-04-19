using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.ClientMenus;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.ClientMenus;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class ClientMenusController : AdminController
    {
        private readonly IClientMenuServices _clientMenuServices;
        public ClientMenusController(IClientMenuServices clientMenuServices)
        {
            _clientMenuServices = clientMenuServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_clientMenuServices.SearchClientMenus(si));
        }

        #region Ajax Methods
        public JsonResult GetParents(int? id)
        {
            return Json(_clientMenuServices.GetPossibleParents(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(ClientMenuModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_clientMenuServices.ManageClientMenu(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }
    }
}
