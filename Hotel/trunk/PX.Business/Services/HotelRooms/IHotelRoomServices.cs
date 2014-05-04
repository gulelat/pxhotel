using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.HotelRooms;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.HotelRooms
{
    public interface IHotelRoomServices
    {
        #region Base

        IQueryable<HotelRoom> GetAll();
        IQueryable<HotelRoom> Fetch(Expression<Func<HotelRoom, bool>> expression);
        HotelRoom FetchFirst(Expression<Func<HotelRoom, bool>> expression);
        HotelRoom GetById(object id);
        ResponseModel Insert(HotelRoom hotelRoom);
        ResponseModel Update(HotelRoom hotelRoom);
        ResponseModel Delete(HotelRoom hotelRoom);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchHotelRooms(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageHotelRoom(GridOperationEnums operation, HotelRoomModel model);

        #endregion

        IEnumerable<SelectListItem> GetStatus();
    }
}
