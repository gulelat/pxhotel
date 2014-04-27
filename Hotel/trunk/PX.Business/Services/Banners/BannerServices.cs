using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using PX.Business.Models.Banners;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.Banners
{
    public class BannerServices : IBannerServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly BannerRepository _bannerRepository;
        public BannerServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _bannerRepository = new BannerRepository();
        }

        #region Base
        public IQueryable<Banner> GetAll()
        {
            return _bannerRepository.GetAll();
        }
        public IQueryable<Banner> Fetch(Expression<Func<Banner, bool>> expression)
        {
            return _bannerRepository.Fetch(expression);
        }

        public Banner FetchFirst(Expression<Func<Banner, bool>> expression)
        {
            return _bannerRepository.FetchFirst(expression);
        }

        public Banner GetById(object id)
        {
            return _bannerRepository.GetById(id);
        }
        public ResponseModel Insert(Banner banner)
        {
            return _bannerRepository.Insert(banner);
        }
        public ResponseModel Update(Banner banner)
        {
            return _bannerRepository.Update(banner);
        }
        public ResponseModel Delete(Banner banner)
        {
            return _bannerRepository.Delete(banner);
        }
        public ResponseModel Delete(object id)
        {
            return _bannerRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _bannerRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the banners.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchBanners(JqSearchIn si)
        {
            var banners = GetAll().Select(b => new BannerModel
            {
                Id = b.Id,
                Text = b.Text,
                Url = b.Url,
                ImageUrl = b.ImageUrl,
                GroupName = b.GroupName,
                RecordActive = b.RecordActive,
                RecordOrder = b.RecordOrder,
                Created = b.Created,
                CreatedBy = b.CreatedBy,
                Updated = b.Updated,
                UpdatedBy = b.UpdatedBy
            });

            return si.Search(banners);
        }

        #endregion

        #region Manage Grid

        /// <summary>
        /// Manage banners
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the setting model</param>
        /// <returns></returns>
        public ResponseModel ManageBanner(GridOperationEnums operation, BannerModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<BannerModel, Banner>();
            Banner banner;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    banner = GetById(model.Id);
                    banner.Text = model.Text;
                    banner.Url = model.Url;
                    banner.GroupName = model.GroupName;
                    banner.RecordOrder = model.RecordOrder;
                    response = Update(banner);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Banners:::Messages:::UpdateSuccessfully:::Update banner successfully.")
                        : _localizedResourceServices.T("AdminModule:::Banners:::Messages:::UpdateFailure:::Update banner failed. Please try again later."));

                case GridOperationEnums.Add:
                    banner = Mapper.Map<BannerModel, Banner>(model);
                    banner.ImageUrl = string.Empty;
                    banner.GroupName = model.GroupName;

                    response = Insert(banner);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Banners:::Messages:::CreateSuccessfully:::Create banner successfully.")
                        : _localizedResourceServices.T("AdminModule:::Banners:::Messages:::CreateFailure:::Insert banner failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Banners:::Messages:::DeleteSuccessfully:::Delete banner successfully.")
                        : _localizedResourceServices.T("AdminModule:::Banners:::Messages:::DeleteFailure:::Delete banner failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Banners:::Messages:::ObjectNotFounded:::banner is not founded.")
            };
        }

        #endregion

        #region Manage

        /// <summary>
        /// Get banner manage model for edit/create
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public BannerManageModel GetBannerManageModel(int? id = null)
        {
            var banner = GetById(id);
            if (banner != null)
            {
                return new BannerManageModel
                {
                    Id = banner.Id,
                    ImageUrl = banner.ImageUrl,
                    Text = banner.Text,
                    Url = banner.Url,
                    GroupName = banner.GroupName
                };
            }
            return new BannerManageModel();
        }

        /// <summary>
        /// Save banner
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SaveBanner(BannerManageModel model)
        {
            ResponseModel response;
            var banner = GetById(model.Id);
            if (banner != null)
            {
                banner.ImageUrl = model.ImageUrl;
                banner.Text = model.Text;
                banner.Url = model.Url;
                banner.GroupName = model.GroupName;
                banner.RecordOrder = model.RecordOrder;

                response = Update(banner);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::Banners:::Messages:::UpdateSuccessfully:::Update banner successfully.")
                    : _localizedResourceServices.T("AdminModule:::Banners:::Messages:::UpdateFailure:::Update banner failed. Please try again later."));
            }
            Mapper.CreateMap<BannerManageModel, Banner>();
            banner = Mapper.Map<BannerManageModel, Banner>(model);
            response = Insert(banner);
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::Banners:::Messages:::CreateSuccessfully:::Create banner successfully.")
                : _localizedResourceServices.T("AdminModule:::Banners:::Messages:::CreateFailure:::Create banner failed. Please try again later."));
        }

        /// <summary>
        /// Update url of banner
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public ResponseModel UpdateBannerUrl(int id, string url)
        {
            var banner = GetById(id);
            if (banner != null)
            {
                banner.Url = url;
                var response = Update(banner);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::Banners:::Messages:::UpdateSuccessfully:::Update banner successfully.")
                    : _localizedResourceServices.T("AdminModule:::Banners:::UpdateFailure:::Update banner failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Banners:::Messages:::ObjectNotFounded:::banner is not founded.")
            };
        }
        #endregion
        
    }
}
