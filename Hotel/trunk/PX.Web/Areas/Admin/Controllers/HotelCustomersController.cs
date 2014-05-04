using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.HotelCustomers;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.HotelCustomers;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class HotelCustomersController : AdminController
    {
        private readonly IHotelCustomerServices _hotelCustomerServices;
        public HotelCustomersController(IHotelCustomerServices hotelCustomerServices)
        {
            _hotelCustomerServices = hotelCustomerServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_hotelCustomerServices.SearchHotelCustomers(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(HotelCustomerModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_hotelCustomerServices.ManageHotelCustomer(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }
    }
}
