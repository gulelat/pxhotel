using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.UserGroups;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.UserGroups
{
    public interface IUserGroupServices
    {
        #region Base

        IQueryable<UserGroup> GetAll();
        IQueryable<UserGroup> Fetch(Expression<Func<UserGroup, bool>> expression);
        UserGroup FetchFirst(Expression<Func<UserGroup, bool>> expression);
        UserGroup GetById(object id);
        ResponseModel Insert(UserGroup userGroup);
        ResponseModel Update(UserGroup userGroup);
        ResponseModel Delete(UserGroup userGroup);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search

        JqGridSearchOut SearchUserGroups(JqSearchIn si);

        #endregion

        #region Manage Grid

        ResponseModel ManageUserGroup(GridOperationEnums operation, UserGroupModel model);

        #endregion
        
        IEnumerable<SelectListItem> GetUserGroups(int? userId = null);

        GroupPermissionsModel GetPermissionSettings(int id);

        ResponseModel SavePermissions(List<int> permissionIds, int userGroupId);
    }
}
