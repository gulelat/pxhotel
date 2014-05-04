using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.HotelRoomImages;
using PX.Core.Framework.Mvc.Models;
using PX.EntityModel;

namespace PX.Business.Services.HotelRoomImages
{
    public interface IHotelRoomImageServices
    {
        #region Base

        IQueryable<HotelRoomImage> GetAll();
        IQueryable<HotelRoomImage> Fetch(Expression<Func<HotelRoomImage, bool>> expression);
        HotelRoomImage FetchFirst(Expression<Func<HotelRoomImage, bool>> expression);
        HotelRoomImage GetById(object id);
        ResponseModel Insert(HotelRoomImage hotelRoomImage);
        ResponseModel Update(HotelRoomImage hotelRoomImage);
        ResponseModel Delete(HotelRoomImage hotelRoomImage);
        ResponseModel Delete(object id);

        #endregion

        HotelRoomImageManageModel GetHotelRoomImageManageModel(int? id = null);

        ResponseModel SaveHotelRoomImage(HotelRoomImageManageModel model);

        ResponseModel MarkAsDefault(int id);
    }
}
