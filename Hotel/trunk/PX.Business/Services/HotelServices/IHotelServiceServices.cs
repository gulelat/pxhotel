using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.HotelServices;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.HotelServices
{
    public interface IHotelServiceServices
    {
        #region Base

        IQueryable<HotelService> GetAll();
        IQueryable<HotelService> Fetch(Expression<Func<HotelService, bool>> expression);
        HotelService FetchFirst(Expression<Func<HotelService, bool>> expression);
        HotelService GetById(object id);
        ResponseModel Insert(HotelService hotelService);
        ResponseModel Update(HotelService hotelService);
        ResponseModel Delete(HotelService hotelService);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchHotelServices(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageHotelService(GridOperationEnums operation, HotelServiceModel model);

        #endregion

        IEnumerable<SelectListItem> GetHotelRoomServices(int? roomTypeId = null);
    }
}
