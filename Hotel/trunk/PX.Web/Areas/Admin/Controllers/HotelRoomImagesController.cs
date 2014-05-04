using System.Web.Mvc;
using PX.Business.Models.HotelRoomImages;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.HotelRoomImages;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class HotelRoomImagesController : AdminController
    {
        private readonly IHotelRoomImageServices _hotelRoomImageServices;
        public HotelRoomImagesController(IHotelRoomImageServices hotelRoomImageServices)
        {
            _hotelRoomImageServices = hotelRoomImageServices;
        }

        /// <summary>
        /// Delete the image
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult DeleteImage(int id)
        {
            return Json(_hotelRoomImageServices.Delete(id));
        }

        [HttpPost]
        public JsonResult MarkAsDefault(int id)
        {
            return Json(_hotelRoomImageServices.MarkAsDefault(id));
        }

        #region Create
        public ActionResult Create(int hotelRoomTypeId)
        {
            var model = _hotelRoomImageServices.GetHotelRoomImageManageModel();
            model.HotelRoomTypeId = hotelRoomTypeId;
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Create(HotelRoomImageManageModel model)
        {
            if (ModelState.IsValid)
            {
                return Json(_hotelRoomImageServices.SaveHotelRoomImage(model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        #endregion

        #region Edit
        public ActionResult Edit(int id)
        {
            var template = _hotelRoomImageServices.GetHotelRoomImageManageModel(id);
            if (!template.Id.HasValue)
            {
                return HttpNotFound();
            }
            return View(template);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Edit(HotelRoomImageManageModel model)
        {
            if (ModelState.IsValid)
            {
                return Json(_hotelRoomImageServices.SaveHotelRoomImage(model));
            }

            return Json(new ResponseModel
            {
                Success = false,
                Message = GetFirstValidationResults(ModelState).Message
            });
        }

        #endregion
    }
}
