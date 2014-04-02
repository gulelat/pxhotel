using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.RotatingImages;
using PX.Business.Models.Settings;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Controllers;
using PX.Business.Mvc.Enums;
using PX.Business.Services.RotatingImages;
using PX.Business.Services.Settings;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class RotatingImagesController : PxController
    {
        private readonly IRotatingImageServices _rotatingImageServices;
        public RotatingImagesController(IRotatingImageServices rotatingImageServices)
        {
            _rotatingImageServices = rotatingImageServices;
        }

        //
        // GET: /Admin/Home/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_rotatingImageServices.SearchRotatingImages(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(RotatingImageModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_rotatingImageServices.ManageRotatingImage(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }
    }
}
