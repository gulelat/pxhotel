using System.Collections.Generic;
using System.Linq;
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
        User GetById(object id);
        ResponseModel Insert(User user);
        ResponseModel Update(User user);
        ResponseModel Delete(User user);
        ResponseModel Delete(object id);

        #endregion

        ResponseModel ManageUser(GridOperationEnums operation, UserModel model);

        User GetUser(string email);

        JqGridSearchOut SearchUsers(JqSearchIn si);

        IEnumerable<SelectListItem> GetStatus();

        ResponseModel Login(LoginModel model);

        ResponseModel ChangePassword(ChangePasswordModel model);

        ResponseModel UploadAvatar(int userId, HttpPostedFileBase avatar);

        ResponseModel UpdateUserData(XEditableModel model);
    }
}
