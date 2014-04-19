using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using PX.Business.Models.Users;
using PX.Business.Models.Users.Logins;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.Editable;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.Users
{
    public interface IUserServices
    {
        #region Base

        IQueryable<User> GetAll();
        IQueryable<User> Fetch(Expression<Func<User, bool>> expression);
        User GetById(object id);
        ResponseModel Insert(User user);
        ResponseModel Update(User user);
        ResponseModel Delete(User user);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search

        JqGridSearchOut SearchUsers(JqSearchIn si);

        #endregion

        #region Manage Grid

        ResponseModel ManageUser(GridOperationEnums operation, UserModel model);

        #endregion

        #region Manage

        ResponseModel ChangePassword(ChangePasswordModel model);

        ResponseModel UploadAvatar(int userId, HttpPostedFileBase avatar);

        ResponseModel UpdateUserData(XEditableModel model);

        #endregion

        User GetUser(string email);

        User GetActiveUser(string email);

        IEnumerable<SelectListItem> GetStatus();

        ResponseModel Login(LoginModel model);

        bool IsEmailExisted(int? userId, string email);
    }
}
