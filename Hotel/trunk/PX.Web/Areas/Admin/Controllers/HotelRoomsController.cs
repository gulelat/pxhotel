using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.HotelRooms;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.HotelRooms;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class HotelRoomsController : AdminController
    {
        private readonly IHotelRoomServices _hotelRoomServices;
        public HotelRoomsController(IHotelRoomServices hotelRoomServices)
        {
            _hotelRoomServices = hotelRoomServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_hotelRoomServices.SearchHotelRooms(si));
        }

        public JsonResult GetStatus()
        {
            return Json(_hotelRoomServices.GetStatus(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(HotelRoomModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_hotelRoomServices.ManageHotelRoom(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }
    }
}
