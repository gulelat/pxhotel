using System.Collections.Generic;
using System.Linq;
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
        UserGroup GetById(object id);
        ResponseModel Insert(UserGroup userGroup);
        ResponseModel Update(UserGroup userGroup);
        ResponseModel Delete(UserGroup userGroup);
        ResponseModel Delete(object id);

        #endregion

        ResponseModel ManageUserGroup(GridOperationEnums operation, UserGroupModel model);

        JqGridSearchOut SearchUserGroups(JqSearchIn si);
        
        IEnumerable<SelectListItem> GetRoles();

        GroupPermissionsModel GetPermissionSettings(int id);

        ResponseModel SavePermissions(List<int> permissionIds, int userGroupId);
    }
}
