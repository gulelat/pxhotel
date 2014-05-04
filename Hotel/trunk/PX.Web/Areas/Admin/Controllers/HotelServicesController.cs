using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.HotelServices;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.HotelServices;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class HotelServicesController : AdminController
    {
        private readonly IHotelServiceServices _hotelServiceServices;
        public HotelServicesController(IHotelServiceServices hotelServiceServices)
        {
            _hotelServiceServices = hotelServiceServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_hotelServiceServices.SearchHotelServices(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(HotelServiceModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_hotelServiceServices.ManageHotelService(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }
    }
}
