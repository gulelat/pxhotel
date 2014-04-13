using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.RotatingImageGroups;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using AutoMapper;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.RotatingImageGroups
{
    public class RotatingImageGroupServices : IRotatingImageGroupServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;

        public RotatingImageGroupServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base

        public IQueryable<RotatingImageGroup> GetAll()
        {
            return RotatingImageGroupRepository.GetAll();
        }

        public IQueryable<RotatingImageGroup> Fetch(Expression<Func<RotatingImageGroup, bool>> expression)
        {
            return RotatingImageGroupRepository.Fetch(expression);
        }

        public RotatingImageGroup GetById(object id)
        {
            return RotatingImageGroupRepository.GetById(id);
        }

        public ResponseModel Insert(RotatingImageGroup rotatingImageGroup)
        {
            return RotatingImageGroupRepository.Insert(rotatingImageGroup);
        }

        public ResponseModel Update(RotatingImageGroup rotatingImageGroup)
        {
            return RotatingImageGroupRepository.Update(rotatingImageGroup);
        }

        public ResponseModel Delete(RotatingImageGroup rotatingImageGroup)
        {
            return RotatingImageGroupRepository.Delete(rotatingImageGroup);
        }

        public ResponseModel Delete(object id)
        {
            return RotatingImageGroupRepository.Delete(id);
        }

        #endregion

        #region Search Methods

        /// <summary>
        /// search the rotating image groups.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchRotatingImageGroups(JqSearchIn si)
        {
            var rotatingImageGroups = GetAll().Select(u => new RotatingImageGroupModel
                {
                    Id = u.Id,
                    Name = u.Name,
                    Settings = u.Settings,
                    RecordActive = u.RecordActive,
                    RecordOrder = u.RecordOrder,
                    Created = u.Created,
                    CreatedBy = u.CreatedBy,
                    Updated = u.Updated,
                    UpdatedBy = u.UpdatedBy
                });

            return si.Search(rotatingImageGroups);
        }

        #endregion

        #region Manage Methods

        /// <summary>
        /// Manage user group
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the user group model</param>
        /// <returns></returns>
        public ResponseModel ManageRotatingImageGroup(GridOperationEnums operation, RotatingImageGroupModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<RotatingImageGroupModel, RotatingImageGroup>();
            RotatingImageGroup rotatingImageGroup;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    rotatingImageGroup = RotatingImageGroupRepository.GetById(model.Id);
                    rotatingImageGroup.Name = model.Name;
                    rotatingImageGroup.RecordOrder = model.RecordOrder;
                    rotatingImageGroup.RecordActive = model.RecordActive;
                    response = Update(rotatingImageGroup);
                    return response.SetMessage(response.Success
                                                   ? _localizedResourceServices.T(
                                                       "AdminModule:::RotatingImageGroups:::Update group successfully")
                                                   : _localizedResourceServices.T(
                                                       "AdminModule:::RotatingImageGroups:::Update group failure. Please try again later."));

                case GridOperationEnums.Add:
                    rotatingImageGroup = Mapper.Map<RotatingImageGroupModel, RotatingImageGroup>(model);
                    rotatingImageGroup.Settings = string.Empty;
                    var groupSettingModel = new GroupSettingModel();
                    rotatingImageGroup.Settings = SerializeUtilities.SerializeObject(groupSettingModel);
                    response = Insert(rotatingImageGroup);
                    return response.SetMessage(response.Success
                                                   ? _localizedResourceServices.T(
                                                       "AdminModule:::RotatingImageGroups:::Insert group successfully")
                                                   : _localizedResourceServices.T(
                                                       "AdminModule:::RotatingImageGroups:::Insert group failure. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success
                                                   ? _localizedResourceServices.T(
                                                       "AdminModule:::RotatingImageGroups:::Delete group successfully")
                                                   : _localizedResourceServices.T(
                                                       "AdminModule:::RotatingImageGroups:::Delete group failure. Please try again later."));
            }
            return new ResponseModel
                {
                    Success = false,
                    Message = _localizedResourceServices.T("AdminModule:::RotatingImageGroups:::Group not founded")
                };
        }

        #endregion

        /// <summary>
        /// Gets the rotating image groups.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetRotatingImageGroups()
        {
            return GetAll().ToList().Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString(CultureInfo.InvariantCulture)
                });
        }

        /// <summary>
        /// Get group rotating image manage model
        /// </summary>
        /// <param name="id">the group id</param>
        /// <returns></returns>
        public GroupManageSettingModel GetGroupManageSettingModel(int id)
        {
            var group = GetById(id);
            if (group != null)
            {
                var model = new GroupManageSettingModel
                    {
                        Id = id,
                        GroupSettingModel = SerializeUtilities.DeserializeString<GroupSettingModel>(@group.Settings)
                    };
                return model;
            }
            return null;
        }

        /// <summary>
        /// Save group settings
        /// </summary>
        /// <param name="model">the group setting model</param>
        /// <returns></returns>
        public ResponseModel SaveGroupSettings(GroupManageSettingModel model)
        {
            var group = GetById(model.Id);
            if (group != null)
            {
                group.Settings = SerializeUtilities.SerializeObject(model.GroupSettingModel);
                var response = Update(group);

                return response.SetMessage(response.Success
                                               ? _localizedResourceServices.T(
                                                   "AdminModule:::RotatingImageGroups:::Update group settings successfully")
                                               : _localizedResourceServices.T(
                                                   "AdminModule:::RotatingImageGroups:::Update group settings failure. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::RotatingImageGroups:::Group not found")
            };
        }
    }
}
