using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.HotelRoomTypes;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.HotelRoomTypes;
using PX.Business.Services.HotelServices;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class HotelRoomTypesController : AdminController
    {
        private readonly IHotelRoomTypeServices _hotelRoomTypeServices;
        private readonly IHotelServiceServices _hotelServiceServices;
        public HotelRoomTypesController(IHotelRoomTypeServices hotelRoomTypeServices, IHotelServiceServices hotelServiceServices)
        {
            _hotelRoomTypeServices = hotelRoomTypeServices;
            _hotelServiceServices = hotelServiceServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_hotelRoomTypeServices.SearchHotelRoomTypes(si));
        }

        public JsonResult GetHotelRoomTypes(int? id)
        {
            return Json(_hotelRoomTypeServices.GetHotelRoomTypes(id), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(HotelRoomTypeModel model, GridManagingModel manageModel)
        {
            if (ModelState.IsValid || manageModel.Operation == GridOperationEnums.Del)
            {
                return Json(_hotelRoomTypeServices.ManageHotelRoomType(manageModel.Operation, model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        #region Create
        public ActionResult Create()
        {
            var model = _hotelRoomTypeServices.GetHotelRoomTypeManageModel();
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(HotelRoomTypeManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _hotelRoomTypeServices.SaveHotelRoomTypeManageModel(model);
                if (response.Success)
                {
                    var templateId = (int)response.Data;
                    SetSuccessMessage(response.Message);
                    switch (submit)
                    {
                        case SubmitTypeEnums.Save:
                            return RedirectToAction("Index");
                        default:
                            return RedirectToAction("Edit", new { id = templateId });
                    }
                }
                SetErrorMessage(response.Message);
            }
            model.HotelRoomTypeServices = _hotelServiceServices.GetHotelRoomServices();
            return View(model);
        }
        #endregion

        #region Edit

        public ActionResult Edit(int id)
        {
            var model = _hotelRoomTypeServices.GetHotelRoomTypeManageModel(id);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(HotelRoomTypeManageModel model, SubmitTypeEnums submit)
        {
            if (ModelState.IsValid)
            {
                var response = _hotelRoomTypeServices.SaveHotelRoomTypeManageModel(model);
                if (response.Success)
                {
                    SetSuccessMessage(response.Message);
                    switch (submit)
                    {
                        case SubmitTypeEnums.Save:
                            return RedirectToAction("Index");
                        default:
                            return RedirectToAction("Edit", new { id = model.Id });
                    }
                }
                SetErrorMessage(response.Message);
            }
            model.HotelRoomTypeServices = _hotelServiceServices.GetHotelRoomServices(model.Id);
            return View(model);
        }
        #endregion

        #region Rotating Image Gallery
        public ActionResult Gallery(int id)
        {
            var model = _hotelRoomTypeServices.GetRoomTypeGallery(id);
            if (model == null)
            {
                SetErrorMessage(LocalizedResourceServices.T("AdminModule:::RotatingImageRoomTypes:::Messages:::UpdateFailure:::Rotating Image RoomType not founded."));
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult SortImages(RoomTypeImageSortingModel model)
        {
            return Json(_hotelRoomTypeServices.SortImages(model));
        }
        #endregion
    }
}
