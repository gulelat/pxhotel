using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Models.DTO;

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

        ResponseModel ManageUser(GridOperationEnums operation, UserModel model);

        User GetUser(string email);

        JqGridSearchOut SearchUsers(JqSearchIn si);

        IEnumerable<SelectListItem> GetStatus();

        IQueryable<Role> GetAllRoles();

        IEnumerable<SelectListItem> GetRoles();
    }
}
