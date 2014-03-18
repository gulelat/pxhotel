using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PX.Business.Models.DTO;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.Users
{
    public interface IUserServices
    {
        #region Base

        IQueryable<User> GetAll();
        User GetById(int id);
        ResponseModel Insert(User user);
        ResponseModel Update(User user);
        ResponseModel Delete(User user);
        ResponseModel Delete(int id);

        #endregion

        ResponseModel ManageUser(GridOperationEnums operation, UserDTO model);

        ResponseModel ManageUserGroup(GridOperationEnums operation, UserGroupDTO model);

        User GetUser(string email);

        JqGridSearchOut SearchUsers(JqSearchIn si);

        JqGridSearchOut SearchUserGroups(JqSearchIn si);

        IEnumerable<SelectListItem> GetStatus();

        IQueryable<UserGroup> GetAllUserGroups();

        IEnumerable<SelectListItem> GetRoles();
    }
}
