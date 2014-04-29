using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.UserGroups;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using AutoMapper;
using PX.EntityModel.Repositories;
using PX.Core.Ultilities;

namespace PX.Business.Services.UserGroups
{
    public class UserGroupServices : IUserGroupServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly UserGroupRepository _userGroupRepository;
        private readonly GroupPermissionRepository _groupPermissionRepository;
        public UserGroupServices(PXHotelEntities entities)
        {
            _userGroupRepository = new UserGroupRepository(entities);
            _groupPermissionRepository = new GroupPermissionRepository(entities);
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base
        public IQueryable<UserGroup> GetAll()
        {
            return _userGroupRepository.GetAll();
        }
        public IQueryable<UserGroup> Fetch(Expression<Func<UserGroup, bool>> expression)
        {
            return _userGroupRepository.Fetch(expression);
        }
        public UserGroup GetById(object id)
        {
            return _userGroupRepository.GetById(id);
        }
        public ResponseModel Insert(UserGroup userGroup)
        {
            return _userGroupRepository.Insert(userGroup);
        }
        public ResponseModel Update(UserGroup userGroup)
        {
            return _userGroupRepository.Update(userGroup);
        }
        public ResponseModel Delete(UserGroup userGroup)
        {
            return _userGroupRepository.Delete(userGroup);
        }
        public ResponseModel Delete(object id)
        {
            return _userGroupRepository.Delete(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the user groups.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchUserGroups(JqSearchIn si)
        {
            var userGroups = GetAll().Select(u => new UserGroupModel
            {
                Id = u.Id,
                Name = u.Name,
                Description = u.Description,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(userGroups);
        }

        #endregion

        #region Grid Manage

        /// <summary>
        /// Manage user group
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the user group model</param>
        /// <returns></returns>
        public ResponseModel ManageUserGroup(GridOperationEnums operation, UserGroupModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<UserGroupModel, UserGroup>();
            UserGroup userGroup;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    userGroup = _userGroupRepository.GetById(model.Id);
                    userGroup.Name = model.Name;
                    userGroup.Description = model.Description;
                    userGroup.RecordOrder = model.RecordOrder;
                    userGroup.RecordActive = model.RecordActive;
                    response = Update(userGroup);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::UserGroups:::Messages:::UpdateSuccessfully:::Update user group successfully.")
                        : _localizedResourceServices.T("AdminModule:::UserGroups:::Messages:::UpdateFailure:::Update user group failed. Please try again later."));
                
                case GridOperationEnums.Add:
                    userGroup = Mapper.Map<UserGroupModel, UserGroup>(model);
                    response = Insert(userGroup);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::UserGroups:::Messages:::CreateSuccessfully:::Craete user group successfully.")
                        : _localizedResourceServices.T("AdminModule:::UserGroups:::Messages:::CreateFailure:::Create user group failed. Please try again later."));
                
                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::UserGroups:::Messages:::DeleteSuccessfully:::Delete user group successfully.")
                        : _localizedResourceServices.T("AdminModule:::UserGroups:::Messages:::DeleteFailure:::Delete user group failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::UserGroups:::Messages:::ObjectNotFounded:::User group is not founded.")
            };
        }

        #endregion

        /// <summary>
        /// Gets the user groups.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetUserGroups(int? userId = null)
        {
            return GetAll().Select(g => new SelectListItem
            {
                Text = g.Name,
                Value = SqlFunctions.StringConvert((double)g.Id).Trim(),
                Selected = g.UserInGroups.Any(ug => ug.UserId == userId)
            });
        }

        /// <summary>
        /// Gets permission setting of user group
        /// </summary>
        /// <param name="id">the user group id</param>
        /// <returns></returns>
        public GroupPermissionsModel GetPermissionSettings(int id)
        {
            var userGroup = GetById(id);
            if (userGroup == null)
                return null;

            var permissionIds = Enum.GetValues(typeof(PermissionEnums)).Cast<int>();

            var currentUserPermission = _groupPermissionRepository.GetByGroupId(id).ToList();

            foreach (var permissionId in permissionIds)
            {
                if (currentUserPermission.All(p => p.PermissionId != permissionId))
                {
                    var groupPermission = new GroupPermission
                    {
                        PermissionId = permissionId,
                        UserGroupId = id,
                        HasPermission = false
                    };
                    _groupPermissionRepository.Insert(groupPermission);
                }
            }

            var userPermissions =
                _groupPermissionRepository.GetByGroupId(id).ToList().Select(p => new GroupPermissionItem
                    {
                        GroupPermissionId = p.Id,
                        PermissionName = ((PermissionEnums)p.PermissionId).GetEnumDescription(),
                        HasPermission = p.HasPermission
                    }).ToList();
            return new GroupPermissionsModel
                {
                    Name = userGroup.Name,
                    Permissions = userPermissions
                };
        }

        /// <summary>
        /// Save permissions
        /// </summary>
        /// <param name="permissionIds">the right permission of user group</param>
        /// <param name="userGroupId">user group id</param>
        /// <returns></returns>
        public ResponseModel SavePermissions(List<int> permissionIds, int userGroupId)
        {
            var currentUserPermission = _groupPermissionRepository.GetByGroupId(userGroupId).ToList();
            foreach (var groupPermission in currentUserPermission)
            {
                if (permissionIds.Contains(groupPermission.Id))
                {
                    if (!groupPermission.HasPermission)
                    {
                        groupPermission.HasPermission = true;
                        _groupPermissionRepository.Update(groupPermission);
                    }
                }
                else if (groupPermission.HasPermission)
                {
                    groupPermission.HasPermission = false;
                    _groupPermissionRepository.Update(groupPermission);
                }
            }
            return new ResponseModel
                {
                    Success = true,
                    Message = _localizedResourceServices.T("AdminModule:::UserGroupPermissions:::Messages:::UpdatePermissionSuccessfully:::Save permission successfully.")
                }
            ;
        }
    }
}
