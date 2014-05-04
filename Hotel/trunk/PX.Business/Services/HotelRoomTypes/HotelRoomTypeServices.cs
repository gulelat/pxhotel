using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.HotelRoomTypes;
using PX.Business.Models.HotelRoomTypes.ViewModels;
using PX.Business.Services.HotelServices;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using AutoMapper;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.HotelRoomTypes
{
    public class HotelRoomTypeServices : IHotelRoomTypeServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly IHotelServiceServices _hotelServiceServices;
        private readonly HotelRoomTypeRepository _hotelRoomTypeRepository;
        private readonly HotelRoomServiceRepository _hotelRoomServiceRepository;
        private readonly HotelRoomImageRepository _hotelRoomImageRepository;
        public HotelRoomTypeServices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _hotelServiceServices = HostContainer.GetInstance<IHotelServiceServices>();
            _hotelRoomTypeRepository = new HotelRoomTypeRepository(entities);
            _hotelRoomServiceRepository = new HotelRoomServiceRepository(entities);
            _hotelRoomImageRepository = new HotelRoomImageRepository(entities);
        }

        #region Base
        public IQueryable<HotelRoomType> GetAll()
        {
            return _hotelRoomTypeRepository.GetAll();
        }
        public IQueryable<HotelRoomType> Fetch(Expression<Func<HotelRoomType, bool>> expression)
        {
            return _hotelRoomTypeRepository.Fetch(expression);
        }
        public HotelRoomType FetchFirst(Expression<Func<HotelRoomType, bool>> expression)
        {
            return _hotelRoomTypeRepository.FetchFirst(expression);
        }
        public HotelRoomType GetById(object id)
        {
            return _hotelRoomTypeRepository.GetById(id);
        }
        public ResponseModel Insert(HotelRoomType hotelRoomType)
        {
            return _hotelRoomTypeRepository.Insert(hotelRoomType);
        }
        public ResponseModel Update(HotelRoomType hotelRoomType)
        {
            return _hotelRoomTypeRepository.Update(hotelRoomType);
        }
        public ResponseModel Delete(HotelRoomType hotelRoomType)
        {
            return _hotelRoomTypeRepository.Delete(hotelRoomType);
        }
        public ResponseModel Delete(object id)
        {
            return _hotelRoomTypeRepository.Delete(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the room types.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchHotelRoomTypes(JqSearchIn si)
        {
            var hotelRoomTypes = GetAll().Select(u => new HotelRoomTypeModel
            {
                Id = u.Id,
                Name = u.Name,
                Description = u.Description,
                Price = u.Price,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(hotelRoomTypes);
        }

        #endregion

        #region Grid Manage

        /// <summary>
        /// Manage user RoomType
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the user RoomType model</param>
        /// <returns></returns>
        public ResponseModel ManageHotelRoomType(GridOperationEnums operation, HotelRoomTypeModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<HotelRoomTypeModel, HotelRoomType>();
            HotelRoomType hotelRoomType;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    hotelRoomType = _hotelRoomTypeRepository.GetById(model.Id);
                    hotelRoomType.Name = model.Name;
                    hotelRoomType.RecordOrder = model.RecordOrder;
                    hotelRoomType.RecordActive = model.RecordActive;
                    response = Update(hotelRoomType);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelRoomTypes:::Messages:::UpdateSuccessfully:::Update HotelRoom type successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelRoomTypes:::Messages:::UpdateFailure:::Update HotelRoom type failed. Please try again later."));
                
                case GridOperationEnums.Add:
                    hotelRoomType = Mapper.Map<HotelRoomTypeModel, HotelRoomType>(model);
                    response = Insert(hotelRoomType);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelRoomTypes:::Messages:::CreateSuccessfully:::Create HotelRoom type successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelRoomTypes:::Messages:::CreateFailure:::Create HotelRoom type failed. Please try again later."));
                
                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::HotelRoomTypes:::Messages:::DeleteSuccessfully:::Delete HotelRoom type successfully.")
                        : _localizedResourceServices.T("AdminModule:::HotelRoomTypes:::Messages:::DeleteFailure:::Delete HotelRoom type failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::HotelRoomTypes:::Messages:::ObjectNotFounded:::HotelRoom type is not founded.")
            };
        }

        #endregion

        #region Manage

        /// <summary>
        /// Get HotelRoomType manage model by id
        /// </summary>
        /// <param name="id">the HotelRoomType id</param>
        /// <returns></returns>
        public HotelRoomTypeManageModel GetHotelRoomTypeManageModel(int? id = null)
        {
            var hotelRoomType = GetById(id);
            if (hotelRoomType != null)
            {
                return new HotelRoomTypeManageModel
                {
                    Id = hotelRoomType.Id,
                    Name = hotelRoomType.Name,
                    Description = hotelRoomType.Description,
                    Price = hotelRoomType.Price,
                    HotelRoomTypeServices = _hotelServiceServices.GetHotelRoomServices(hotelRoomType.Id),
                    RecordOrder = hotelRoomType.RecordOrder,
                    RecordActive = hotelRoomType.RecordActive
                };
            }
            return new HotelRoomTypeManageModel
            {
                HotelRoomTypeServices = _hotelServiceServices.GetHotelRoomServices(),
            };
        }

        /// <summary>
        /// Save HotelRoomType manage model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SaveHotelRoomTypeManageModel(HotelRoomTypeManageModel model)
        {
            ResponseModel response;
            var hotelRoomType = GetById(model.Id);

            #region Edit HotelRoomType
            if (hotelRoomType != null)
            {
                hotelRoomType.Name = model.Name;
                hotelRoomType.Description = model.Description;
                hotelRoomType.Price = model.Price;
                hotelRoomType.RecordOrder = model.RecordOrder;

                var currentServices = hotelRoomType.HotelRoomServices.Select(rs => rs.ServiceId).ToList();
                foreach (var id in currentServices)
                {
                    if (!model.HotelRoomTypeServiceIds.Contains(id))
                    {
                        _hotelRoomServiceRepository.Delete(id);
                    }
                }
                foreach (var serviceId in model.HotelRoomTypeServiceIds)
                {
                    if (currentServices.All(n => n != serviceId))
                    {
                        var hotelRoomTypeHotelRoomTypeCategory = new HotelRoomService
                        {
                            RoomTypeId = hotelRoomType.Id,
                            ServiceId = serviceId
                        };
                        _hotelRoomServiceRepository.Insert(hotelRoomTypeHotelRoomTypeCategory);
                    }
                }

                //Get page record order
                response = Update(hotelRoomType);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::HotelRoomTypes:::Messages:::UpdateSuccessfully:::Update HotelRoomType successfully.")
                    : _localizedResourceServices.T("AdminModule:::HotelRoomTypes:::Messages:::UpdateFailure:::Update HotelRoomType failed. Please try again later."));
            }
            #endregion

            hotelRoomType = new HotelRoomType
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price,
            };

            response = Insert(hotelRoomType);
            foreach (var serviceId in model.HotelRoomTypeServiceIds)
            {
                var hotelRoomTypeHotelRoomTypeCategory = new HotelRoomService
                {
                    RoomTypeId = hotelRoomType.Id,
                    ServiceId = serviceId
                };
                _hotelRoomServiceRepository.Insert(hotelRoomTypeHotelRoomTypeCategory);
            }
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::HotelRoomTypes:::Messages:::CreateSuccessfully:::Create HotelRoomType successfully.")
                : _localizedResourceServices.T("AdminModule:::HotelRoomTypes:::Messages:::CreateFailure:::Create HotelRoomType failed. Please try again later."));
        }
        #endregion

        /// <summary>
        /// Gets the HotelRoom types.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetHotelRoomTypes(int? typeId)
        {
            return GetAll().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = SqlFunctions.StringConvert((double)r.Id).Trim(),
                Selected = typeId.HasValue && r.Id == typeId
            });
        }

        /// <summary>
        /// Check if room type name is existed
        /// </summary>
        /// <param name="roomTypeId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsRoomTypeNameExisted(int? roomTypeId, string name)
        {
            return Fetch(u => u.Name.Equals(name) && u.Id != roomTypeId).Any();
        }

        /// <summary>
        /// Get all rotating image of gallery
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RoomTypeGalleryModel GetRoomTypeGallery(int id)
        {
            var roomType = GetById(id);
            if (roomType != null)
            {
                var images = GetById(id).HotelRoomImages.OrderBy(i => i.RecordOrder);
                return new RoomTypeGalleryModel
                {
                    Id = id,
                    RoomTypeName = roomType.Name,
                    GalleryItems = images.Select(i => new GalleryItemModel
                    {
                        Id = i.Id,
                        ImageUrl = i.ImageUrl,
                        IsDefaultImage = i.IsDefaultImage,
                        RecordOrder = i.RecordOrder
                    }).ToList()
                };
            }
            return null;
        }

        public ResponseModel SortImages(RoomTypeImageSortingModel model)
        {
            var roomType = GetById(model.RoomTypeId);
            if (roomType != null)
            {
                var images = roomType.HotelRoomImages.OrderBy(i => model.Ids.ToList().IndexOf(i.Id)).ToList();
                var dictionary = images.OrderBy(i => i.RecordOrder).Select(i => new { i.Id, i.RecordOrder }).ToList();
                var index = 0;
                foreach (var image in images)
                {
                    if (image.Id != dictionary[index].Id)
                    {
                        image.RecordOrder = dictionary[index].RecordOrder;
                        _hotelRoomImageRepository.Update(image);
                    }
                    index++;
                }
                return new ResponseModel
                {
                    Success = true,
                    Message = _localizedResourceServices.T("AdminModule:::HotelRoomTypes:::Messages:::SortSuccessfully:::Sort successfully.")
                };
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::HotelRoomTypes:::Messages:::ObjectNotFounded:::Room is not founded.")
            };
        }

        /// <summary>
        /// Get room events
        /// </summary>
        /// <returns></returns>
        public List<RoomEventViewModel> GetRoomEvents()
        {
            return GetAll().Select(r => new RoomEventViewModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    Price = r.Price,
                    RecordOrder = r.RecordOrder,
                    TotalRooms = r.TotalRooms,
                    RoomColor = r.RoomColor
                }).ToList();
        }
    }
}
