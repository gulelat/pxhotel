using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.HotelBookings;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.HotelBookings;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class HotelBookingsController : AdminController
    {
        private readonly IHotelBookingServices _hotelBookingServices;
        public HotelBookingsController(IHotelBookingServices hotelBookingServices)
        {
            _hotelBookingServices = hotelBookingServices;
        }

        #region Listing

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_hotelBookingServices.SearchHotelBookings(si));
        }
        public JsonResult GetStatus()
        {
            return Json(_hotelBookingServices.GetStatus(), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(HotelBookingModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_hotelBookingServices.ManageHotelBooking(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        #endregion

        public ActionResult Calendar()
        {
            var model = _hotelBookingServices.GetBookingCalendar();
            return View(model);
        }

        #region Create/Edit Booking
        public ActionResult Create()
        {
            return View();
        }
        #endregion
    }
}