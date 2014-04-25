using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PX.Business.Models.Users;
using PX.Business.Models.Users.Logins;
using PX.Core.Configurations;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Mvc.WorkContext;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.Editable;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using AutoMapper;
using PX.EntityModel.Repositories;
using PX.EntityModel.Repositories.RepositoryBase.Extensions;

namespace PX.Business.Services.Users
{
    public class UserServices : IUserServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;

        public UserServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base
        public IQueryable<User> GetAll()
        {
            return UserRepository.GetAll();
        }
        public IQueryable<User> Fetch(Expression<Func<User, bool>> expression)
        {
            return UserRepository.Fetch(expression);
        }
        public User GetById(object id)
        {
            return UserRepository.GetById(id);
        }
        public ResponseModel Insert(User user)
        {
            return UserRepository.Insert(user);
        }
        public ResponseModel Update(User user)
        {
            return UserRepository.Update(user);
        }
        public ResponseModel Delete(User user)
        {
            return UserRepository.Delete(user);
        }
        public ResponseModel Delete(object id)
        {
            return UserRepository.Delete(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the users.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchUsers(JqSearchIn si)
        {
            var users = GetAll().ToList().Select(u => new UserModel
            {
                Id = u.Id,
                Email = u.Email,
                Password = u.Password,
                FirstName = u.FirstName,
                LastName = u.LastName,
                AvatarFileName = u.AvatarFileName,
                LastLogin = u.LastLogin,
                Phone = u.Phone,
                IdentityNumber = u.IdentityNumber,
                Status = u.Status,
                UserGroups = u.UserInGroups.Any() ? string.Join(",", u.UserInGroups.Select(g => g.UserGroup.Name)) : string.Empty,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            }).AsQueryable();

            return si.Search(users);
        }

        #endregion

        #region Grid Manage

        /// <summary>
        /// Manage user
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the user model</param>
        /// <returns></returns>
        public ResponseModel ManageUser(GridOperationEnums operation, UserModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<UserModel, User>();
            User user;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    user = GetById(model.Id);
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.Phone = model.Phone;
                    user.Status = model.Status;

                    user.IdentityNumber = model.IdentityNumber;
                    user.RecordActive = model.RecordActive;
                    user.RecordOrder = 0;
                    response = Update(user);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Users:::Messages:::UpdateSuccessfully:::Update user successfully.")
                        : _localizedResourceServices.T("AdminModule:::Users:::Messages:::UpdateFailure:::Update user failed. Please try again later."));

                //case GridOperationEnums.Add:
                //    user = Mapper.Map<UserModel, User>(model);
                //    user.Status = model.Status;

                //    user.RecordOrder = 0;
                //    response = Insert(user);
                //    return response.SetMessage(response.Success ?
                //        _localizedResourceServices.T("AdminModule:::Users:::Messages:::CreateSuccessfully:::Create user successfully.")
                //        : _localizedResourceServices.T("AdminModule:::Users:::Messages:::CreateFailure:::Create user failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Users:::Messages:::DeleteSuccessfully:::Delete user successfully.")
                        : _localizedResourceServices.T("AdminModule:::Users:::Messages:::DeleteFailure:::Delete user failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Users:::Messages:::ObjectNotFounded:::User is not founded.")
            };
        }

        #endregion

        #region Login/ Register / Forgot Password

        /// <summary>
        /// Login method
        /// </summary>
        /// <param name="model">the login model</param>
        /// <returns></returns>
        public ResponseModel Login(LoginModel model)
        {
            var user = GetUser(model.Email);
            if (user != null)
            {
                if (user.StatusEnums == UserEnums.UserStatusEnums.Active && user.Password.Equals(model.Password))
                {
                    if (model.RememberMe)
                    {
                        var authenticationTicket = new FormsAuthenticationTicket(
                            1,
                            user.Email,
                            DateTime.Now,
                            DateTime.Now.AddYears(1),
                            model.RememberMe,
                            user.Email,
                            "/"
                            );

                        //encrypt the ticket and add it to a cookie
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authenticationTicket));
                        HttpContext.Current.Response.Cookies.Add(cookie);
                    }

                    FormsAuthentication.SetAuthCookie(Convert.ToString(user.Email), true);
                    WorkContext.CurrentUser = user;

                    user.LastLogin = DateTime.Now;
                    Update(user);
                    var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                    return new ResponseModel
                        {
                            Success = true,
                            Message = _localizedResourceServices.T("AdminModule:::Users:::Messages:::LoginSuccessfully:::Login succesfully"),
                            Data = string.IsNullOrEmpty(model.ReturnUrl) ? urlHelper.Action("LoginSuccess", "Account") : model.ReturnUrl
                        };
                }
            }
            return new ResponseModel
                {
                    Success = false,
                    Message = _localizedResourceServices.T("AdminModule:::Users:::ValidationMessages:::InvalidUserPassword:::Invalid email or password. Please try again.")
                };
        }

        #endregion

        #region User Profile
        /// <summary>
        /// Upload user avatar
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="avatar"></param>
        /// <returns></returns>
        public ResponseModel UploadAvatar(int userId, HttpPostedFileBase avatar)
        {
            var user = GetById(userId);
            if (user == null)
            {
                return new ResponseModel
                    {
                        Success = false,
                        Message = _localizedResourceServices.T("AdminModule:::Users:::Messsages:::ObjectNotFounded:::User is not founded.")
                    };
            }
            var extension = avatar.FileName.GetExtension();
            var fileName = user.Id.GenerateAvatarFileName(extension);
            var path = Path.Combine(HttpContext.Current.Server.MapPath(Configurations.AvatarFolder), fileName);
            var returnPath = string.Format("{0}{1}", Configurations.AvatarFolder, fileName);
            user.AvatarFileName = fileName;
            var response = Update(user);

            //Refresh current user data
            if (user.Id == WorkContext.CurrentUser.Id)
            {
                WorkContext.CurrentUser = user;
            }

            if (response.Success)
            {
                avatar.SaveAs(path);
                response.Data = returnPath;
            }
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::Users:::Messages:::UploadAvatarSuccessfully:::Upload avatar successfully.")
                : _localizedResourceServices.T("AdminModule:::Users:::Messages:::UploadAvatarFailure:::Upload avatar failed. Please try again later."));
        }
        #endregion

        public List<int> GetUserPermissions(int? userId = null)
        {
            var user = userId.HasValue ? GetById(userId) : WorkContext.CurrentUser;
            if (user == null)
                return new List<int>();

            var uPermissions = user.UserInGroups
                .Select(ug => ug.UserGroup.GroupPermissions.Where(gp => gp.HasPermission).Select(gp => gp.PermissionId)).Distinct().ToList();
            var userPermissions = new List<int>();
            foreach (var uPermission in uPermissions)
            {
                foreach (var i in uPermission)
                {
                    if (!userPermissions.Contains(i))
                    {
                        userPermissions.Add(i);
                    }
                }
            }
            return userPermissions;
        }

        /// <summary>
        /// Get user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public User GetUser(string email)
        {
            return GetAll().FirstOrDefault(u => u.Email.Equals(email));
        }

        /// <summary>
        /// Get active user by email
        /// </summary>
        /// <param name="email">user email</param>
        /// <returns></returns>
        public User GetActiveUser(string email)
        {
            return GetAll().FirstOrDefault(u => u.Email.Equals(email) && u.Status == (int)UserEnums.UserStatusEnums.Active);
        }

        /// <summary>
        /// Gets the user status.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetStatus()
        {
            return EnumUtilities.GetAllItemsFromEnum<UserEnums.UserStatusEnums>();
        }

        /// <summary>
        /// Update user data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel UpdateUserData(XEditableModel model)
        {
            var user = GetById(model.Pk);
            if (user != null)
            {
                object value = model.Value;
                if (model.Name.Equals("BirthDay"))
                {
                    value = model.Value.ToDate();
                }
                user.SetProperty(model.Name, value);
                var response = Update(user);
                if (user.Id == WorkContext.CurrentUser.Id && response.Success)
                    WorkContext.CurrentUser = user;
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::Users:::Messages:::UpdateUserInfoSuccessfully:::Update user info successfully.")
                    : _localizedResourceServices.T("AdminModule:::Users:::Messages:::UpdateUserInfoFailure:::Update user info failed. Please try again later."));
            }
            return new ResponseModel
                {
                    Success = false,
                    Message = _localizedResourceServices.T("AdminModule:::Users:::Messages:::ObjectNotFounded:::User is not founded.")
                };
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="model">the change password model</param>
        /// <returns></returns>
        public ResponseModel ChangePassword(ChangePasswordModel model)
        {
            var user = GetById(model.UserId);
            if (user != null)
            {
                user.Password = model.Password;
                var response = Update(user);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::Users:::Messages:::ChangePasswordSuccessfully:::Change password successfully")
                    : _localizedResourceServices.T("AdminModule:::Users:::Messages:::ChagePasswordFailure:::Change password failed. Please try again later."));
            }
            return new ResponseModel
                {
                    Success = false,
                    Message = _localizedResourceServices.T("AdminModule:::Users:::Messages:::ObjectNotFounded:::User is not founded.")
                };
        }

        /// <summary>
        /// Check if user is existed
        /// </summary>
        /// <param name="userId">the user id</param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsEmailExisted(int? userId, string email)
        {
            return Fetch(u => u.Email.Equals(email) && u.Id != userId).Any();
        }
    }
}
