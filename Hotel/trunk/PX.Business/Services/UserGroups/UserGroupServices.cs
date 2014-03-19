using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using PX.Business.Models.UserGroups;
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
        #region Base
        public IQueryable<UserGroup> GetAll()
        {
            return UserGroupRepository.GetAll();
        }
        public UserGroup GetById(object id)
        {
            return UserGroupRepository.GetById(id);
        }
        public ResponseModel Insert(UserGroup userGroup)
        {
            return UserGroupRepository.Insert(userGroup);
        }
        public ResponseModel Update(UserGroup userGroup)
        {
            return UserGroupRepository.Update(userGroup);
        }
        public ResponseModel Delete(UserGroup userGroup)
        {
            return UserGroupRepository.Delete(userGroup);
        }
        public ResponseModel Delete(object id)
        {
            return UserGroupRepository.Delete(id);
        }
        #endregion

        #region Search Methods

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

        #region Manage Methods

        /// <summary>
        /// Manage user group
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the user group model</param>
        /// <returns></returns>
        public ResponseModel ManageUserGroup(GridOperationEnums operation, UserGroupModel model)
        {
            Mapper.CreateMap<UserGroupModel, UserGroup>();
            UserGroup userGroup;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    userGroup = UserGroupRepository.GetById(model.Id);
                    userGroup.Name = model.Name;
                    userGroup.Description = model.Description;
                    userGroup.RecordOrder = model.RecordOrder;
                    userGroup.RecordActive = model.RecordActive;
                    return UserGroupRepository.Update(userGroup);
                case GridOperationEnums.Add:
                    userGroup = Mapper.Map<UserGroupModel, UserGroup>(model);
                    return UserGroupRepository.Insert(userGroup);
                case GridOperationEnums.Del:
                    return UserGroupRepository.Delete(model.Id);
            }
            return new ResponseModel
            {
                Success = false,
                Message = "Object not founded"
            };
        }

        #endregion

        /// <summary>
        /// Gets the user roles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetRoles()
        {
            return GetAll().ToList().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString(CultureInfo.InvariantCulture)
            });
        }

        public GroupPermissionsModel GetPermissionSettings(int id)
        {
            var userGroup = GetById(id);
            if (userGroup == null)
                return null;

            var permissionIds = Enum.GetValues(typeof(PermissionEnums)).Cast<int>();

            var currentUserPermission = GroupPermissionRepository.GetByGroupId(id).ToList();

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
                    GroupPermissionRepository.Insert(groupPermission);
                }
            }

            var userPermissions =
                GroupPermissionRepository.GetByGroupId(id).ToList().Select(p => new GroupPermissionItem
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

        public ResponseModel SavePermissions(List<int> permissionIds, int userGroupId)
        {
            var currentUserPermission = GroupPermissionRepository.GetByGroupId(userGroupId).ToList();
            foreach (var groupPermission in currentUserPermission)
            {
                if (permissionIds.Contains(groupPermission.Id))
                {
                    if (!groupPermission.HasPermission)
                    {
                        groupPermission.HasPermission = true;
                        GroupPermissionRepository.Update(groupPermission);
                    }
                }
                else if (groupPermission.HasPermission)
                {
                    groupPermission.HasPermission = false;
                    GroupPermissionRepository.Update(groupPermission);
                }
            }
            return new ResponseModel
                {
                    Success = true
                }
            ;
        }
    }
}
