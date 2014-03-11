using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PX.Business.Models;
using PX.Business.Models.UserModels;
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

        User GetUser(string email);

        IQueryable<User> SearchUsers(UserSearchModel searchModel);

        IEnumerable<SelectListItem> GetStatus();

        IQueryable<Role> GetRoles();
    }
}
