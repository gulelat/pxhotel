using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using PX.Business.Models.RotatingImages;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.RotatingImageGroups;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.RotatingImages
{
    public class RotatingImageServices : IRotatingImageServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly IRotatingImageGroupServices _rotatingImageGroupServices;
        private readonly RotatingImageRepository _rotatingImageRepository;
        public RotatingImageServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _rotatingImageGroupServices = HostContainer.GetInstance<IRotatingImageGroupServices>();
            _rotatingImageRepository = new RotatingImageRepository();
        }

        #region Base
        public IQueryable<RotatingImage> GetAll()
        {
            return _rotatingImageRepository.GetAll();
        }
        public IQueryable<RotatingImage> Fetch(Expression<Func<RotatingImage, bool>> expression)
        {
            return _rotatingImageRepository.Fetch(expression);
        }
        public RotatingImage GetById(object id)
        {
            return _rotatingImageRepository.GetById(id);
        }
        public ResponseModel Insert(RotatingImage rotatingImage)
        {
            return _rotatingImageRepository.Insert(rotatingImage);
        }
        public ResponseModel Update(RotatingImage rotatingImage)
        {
            return _rotatingImageRepository.Update(rotatingImage);
        }
        public ResponseModel Delete(RotatingImage rotatingImage)
        {
            return _rotatingImageRepository.Delete(rotatingImage);
        }
        public ResponseModel Delete(object id)
        {
            return _rotatingImageRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _rotatingImageRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the rotating images.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchRotatingImages(JqSearchIn si)
        {
            var rotatingImages = GetAll().Select(u => new RotatingImageModel
            {
                Id = u.Id,
                ImageUrl = u.ImageUrl,
                Url = u.Url,
                GroupId = u.GroupId,
                GroupName = u.RotatingImageGroup.Name,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(rotatingImages);
        }

        #endregion

        #region Manage Grid

        /// <summary>
        /// Manage Rotating Images
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the setting model</param>
        /// <returns></returns>
        public ResponseModel ManageRotatingImage(GridOperationEnums operation, RotatingImageModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<RotatingImageModel, RotatingImage>();
            RotatingImage rotatingImage;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    rotatingImage = GetById(model.Id);
                    rotatingImage.ImageUrl = model.ImageUrl;
                    rotatingImage.Url = model.Url;
                    rotatingImage.GroupId = model.GroupName.ToInt();
                    rotatingImage.RecordOrder = model.RecordOrder;
                    response = Update(rotatingImage);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::RotatingImages:::Messages:::UpdateSuccessfully:::Update rotating image successfully.")
                        : _localizedResourceServices.T("AdminModule:::RotatingImages:::Messages:::UpdateFailure:::Update rotating image failed. Please try again later."));

                case GridOperationEnums.Add:
                    rotatingImage = Mapper.Map<RotatingImageModel, RotatingImage>(model);
                    rotatingImage.ImageUrl = string.Empty;
                    rotatingImage.GroupId = model.GroupName.ToInt();

                    response = Insert(rotatingImage);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::RotatingImages:::Messages:::CreateSuccessfully:::Create rotating image successfully.")
                        : _localizedResourceServices.T("AdminModule:::RotatingImages:::Messages:::CreateFailure:::Insert rotating image failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::RotatingImages:::Messages:::DeleteSuccessfully:::Delete rotating image successfully.")
                        : _localizedResourceServices.T("AdminModule:::RotatingImages:::Messages:::DeleteFailure:::Delete rotating image failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::RotatingImages:::Messages:::ObjectNotFounded:::Rotating image is not founded.")
            };
        }

        #endregion

        #region Manage

        /// <summary>
        /// Get rotating image manage model for edit/create
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RotatingImageManageModel GetRotatingImageManageModel(int? id = null)
        {
            var rotatingImage = GetById(id);
            if (rotatingImage != null)
            {
                return new RotatingImageManageModel
                {
                    Id = rotatingImage.Id,
                    ImageUrl = rotatingImage.ImageUrl,
                    Text = rotatingImage.Text,
                    Url = rotatingImage.Url,
                    GroupId = rotatingImage.GroupId,
                    Groups = _rotatingImageGroupServices.GetRotatingImageGroups()
                };
            }
            return new RotatingImageManageModel
            {
                Groups = _rotatingImageGroupServices.GetRotatingImageGroups()
            };
        }

        /// <summary>
        /// Save rotating image
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SaveRotatingImage(RotatingImageManageModel model)
        {
            ResponseModel response;
            var rotatingImage = GetById(model.Id);
            if (rotatingImage != null)
            {
                rotatingImage.ImageUrl = model.ImageUrl;
                rotatingImage.Text = model.Text;
                rotatingImage.Url = model.Url;
                rotatingImage.GroupId = model.GroupId;

                response = Update(rotatingImage);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::RotatingImages:::Messages:::UpdateSuccessfully:::Update rotating image successfully.")
                    : _localizedResourceServices.T("AdminModule:::RotatingImages:::Messages:::UpdateFailure:::Update rotating image failed. Please try again later."));
            }
            Mapper.CreateMap<RotatingImageManageModel, RotatingImage>();
            rotatingImage = Mapper.Map<RotatingImageManageModel, RotatingImage>(model);
            rotatingImage.RecordOrder = Fetch(i => i.GroupId == model.GroupId).Any() ? Fetch(i => i.GroupId == model.GroupId).Max(i => i.RecordOrder) + 1 : 0;
            response = Insert(rotatingImage);
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::RotatingImages:::Messages:::CreateSuccessfully:::Create rotating image successfully.")
                : _localizedResourceServices.T("AdminModule:::RotatingImages:::Messages:::CreateFailure:::Create rotating image failed. Please try again later."));
        }

        /// <summary>
        /// Update url of rotating image
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public ResponseModel UpdateRotatingImageUrl(int id, string url)
        {
            var image = GetById(id);
            if (image != null)
            {
                image.Url = url;
                var response = Update(image);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::RotatingImages:::Messages:::UpdateSuccessfully:::Update rotating image successfully.")
                    : _localizedResourceServices.T("AdminModule:::RotatingImages:::UpdateFailure:::Update rotating image failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::RotatingImages:::Messages:::ObjectNotFounded:::Rotating image is not founded.")
            };
        }
        #endregion
        
    }
}
