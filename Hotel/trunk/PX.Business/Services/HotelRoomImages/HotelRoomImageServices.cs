using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using PX.Business.Models.HotelRoomImages;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Mvc.Models;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.HotelRoomImages
{
    public class HotelRoomImageServices : IHotelRoomImageServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly HotelRoomImageRepository _hotelRoomImageRepository;
        public HotelRoomImageServices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _hotelRoomImageRepository = new HotelRoomImageRepository(entities);
        }

        #region Base
        public IQueryable<HotelRoomImage> GetAll()
        {
            return _hotelRoomImageRepository.GetAll();
        }
        public IQueryable<HotelRoomImage> Fetch(Expression<Func<HotelRoomImage, bool>> expression)
        {
            return _hotelRoomImageRepository.Fetch(expression);
        }
        public HotelRoomImage FetchFirst(Expression<Func<HotelRoomImage, bool>> expression)
        {
            return _hotelRoomImageRepository.FetchFirst(expression);
        }
        public HotelRoomImage GetById(object id)
        {
            return _hotelRoomImageRepository.GetById(id);
        }
        public ResponseModel Insert(HotelRoomImage hotelRoomImage)
        {
            return _hotelRoomImageRepository.Insert(hotelRoomImage);
        }
        public ResponseModel Update(HotelRoomImage hotelRoomImage)
        {
            return _hotelRoomImageRepository.Update(hotelRoomImage);
        }
        public ResponseModel Delete(HotelRoomImage hotelRoomImage)
        {
            return _hotelRoomImageRepository.Delete(hotelRoomImage);
        }
        public ResponseModel Delete(object id)
        {
            return _hotelRoomImageRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _hotelRoomImageRepository.InactiveRecord(id);
        }
        #endregion

        #region Manage

        /// <summary>
        /// Get HotelRoom image manage model for edit/create
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HotelRoomImageManageModel GetHotelRoomImageManageModel(int? id = null)
        {
            var hotelRoomImage = GetById(id);
            if (hotelRoomImage != null)
            {
                return new HotelRoomImageManageModel
                {
                    Id = hotelRoomImage.Id,
                    ImageUrl = hotelRoomImage.ImageUrl,
                    Description = hotelRoomImage.Description,
                    HotelRoomTypeId = hotelRoomImage.HotelRoomTypeId,
                };
            }
            return new HotelRoomImageManageModel();
        }

        /// <summary>
        /// Save HotelRoom image
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SaveHotelRoomImage(HotelRoomImageManageModel model)
        {
            ResponseModel response;
            var hotelRoomImage = GetById(model.Id);
            if (hotelRoomImage != null)
            {
                hotelRoomImage.ImageUrl = model.ImageUrl;
                hotelRoomImage.Description = model.Description;
                hotelRoomImage.HotelRoomTypeId = model.HotelRoomTypeId;

                response = Update(hotelRoomImage);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::HotelRoomImages:::Messages:::UpdateSuccessfully:::Update room image successfully.")
                    : _localizedResourceServices.T("AdminModule:::HotelRoomImages:::Messages:::UpdateFailure:::Update room image failed. Please try again later."));
            }
            Mapper.CreateMap<HotelRoomImageManageModel, HotelRoomImage>();
            hotelRoomImage = Mapper.Map<HotelRoomImageManageModel, HotelRoomImage>(model);
            hotelRoomImage.RecordOrder = Fetch(i => i.HotelRoomTypeId == model.HotelRoomTypeId).Any() ? Fetch(i => i.HotelRoomTypeId == model.HotelRoomTypeId).Max(i => i.RecordOrder) + 1 : 0;
            response = Insert(hotelRoomImage);
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::HotelRoomImages:::Messages:::CreateSuccessfully:::Create room image successfully.")
                : _localizedResourceServices.T("AdminModule:::HotelRoomImages:::Messages:::CreateFailure:::Create room image failed. Please try again later."));
        }

        /// <summary>
        /// Mark image as default
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel MarkAsDefault(int id)
        {
            var image = GetById(id);
            if(image != null)
            {
                var query = string.Format("Update HotelRoomImages Set IsDefaultImage = 0 Where HotelRoomTypeId = {0}", image.HotelRoomTypeId);
                var response = _hotelRoomImageRepository.ExcuteSql(query);
                if (response.Success)
                {
                    image.IsDefaultImage = true;
                    response = Update(image);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelRoomImages:::Messages:::MarkDefaultSuccessfully:::Mark room image as default successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelRoomImages:::Messages:::MarkDefaultFailure:::Mark room image as default failed. Please try again later."));
                }
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::HotelRoomImages:::Messages:::ObjectNotFounded:::Image is not founded.")
            };
        }

        #endregion
    }
}
