using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.HotelRoomTypes;
using PX.Business.Models.HotelRoomTypes.ViewModels;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.HotelRoomTypes
{
    public interface IHotelRoomTypeServices
    {
        #region Base

        IQueryable<HotelRoomType> GetAll();
        IQueryable<HotelRoomType> Fetch(Expression<Func<HotelRoomType, bool>> expression);
        HotelRoomType FetchFirst(Expression<Func<HotelRoomType, bool>> expression);
        HotelRoomType GetById(object id);
        ResponseModel Insert(HotelRoomType hotelRoomType);
        ResponseModel Update(HotelRoomType hotelRoomType);
        ResponseModel Delete(HotelRoomType hotelRoomType);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchHotelRoomTypes(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageHotelRoomType(GridOperationEnums operation, HotelRoomTypeModel model);

        #endregion

        #region Manage

        HotelRoomTypeManageModel GetHotelRoomTypeManageModel(int? id = null);

        ResponseModel SaveHotelRoomTypeManageModel(HotelRoomTypeManageModel model);

        #endregion

        IEnumerable<SelectListItem> GetHotelRoomTypes(int? typeId);

        bool IsRoomTypeNameExisted(int? roomTypeId, string name);

        #region Gallery

        RoomTypeGalleryModel GetRoomTypeGallery(int id);

        ResponseModel SortImages(RoomTypeImageSortingModel model);

        #endregion

        #region Events

        List<RoomEventViewModel> GetRoomEvents();

        #endregion
    }
}
