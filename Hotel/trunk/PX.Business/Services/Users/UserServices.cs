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
using PX.Business.Mvc.Enums;
using PX.Business.Mvc.Environments;
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
using Configurations = PX.Core.Configurations.Configurations;

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

        #region Search Methods

        /// <summary>
        /// search the users.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchUsers(JqSearchIn si)
        {
            var users = GetAll().Select(u => new UserModel
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
                UserGroupId = u.UserGroupId,
                UserGroupName = u.UserGroup.Name,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(users);
        }

        #endregion

        #region Manage Methods

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
                    user.Email = model.Email;
                    user.Password = model.Password;
                    user.Phone = model.Phone;
                    user.Status = model.Status;

                    // Convert post data from jqGrid post
                    user.UserGroupId = int.Parse(model.UserGroupName);
                    user.IdentityNumber = model.IdentityNumber;
                    user.RecordActive = model.RecordActive;
                    user.RecordOrder = 0;
                    response = Update(user);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Users:::Messages:::Update user successfully")
                        : _localizedResourceServices.T("AdminModule:::Users:::Messages:::Update user failure. Please try again later."));

                case GridOperationEnums.Add:
                    user = Mapper.Map<UserModel, User>(model);
                    user.Status = model.Status;
                    // Convert post data from jqGrid post
                    user.UserGroupId = int.Parse(model.UserGroupName);
                    user.RecordOrder = 0;
                    response = Insert(user);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Users:::Messages:::Create user successfully")
                        : _localizedResourceServices.T("AdminModule:::Users:::Messages:::Create user failure. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Users:::Messages:::Delete user successfully")
                        : _localizedResourceServices.T("AdminModule:::Users:::Messages:::Delete user failure. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Users:::Messages:::User not founded")
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
            if(user != null)
            {
                if(user.StatusEnums == UserEnums.UserStatusEnums.Active && user.Password.Equals(model.Password))
                {
                    if(model.RememberMe)
                    {
                        var authenticationTicket = new FormsAuthenticationTicket(
                            1,
                            user.Email,
                            DateTime.Now,
                            DateTime.Now.AddYears(1),
                            model.RememberMe,
                            user.UserGroup.Name,
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
                            Message = _localizedResourceServices.T("AdminModule:::Users:::Login succesfully"),
                            Data = string.IsNullOrEmpty(model.ReturnUrl) ? urlHelper.Action("LoginSuccess", "Account") : model.ReturnUrl
                        };
                }
            }
            return new ResponseModel
                {
                    Success = false,
                    Message = _localizedResourceServices.T("AdminModule:::Users:::Invalid email or password. Please try again.")
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
            if(user == null)
            {
                return new ResponseModel
                    {
                        Success = false,
                        Message = _localizedResourceServices.T("AdminModule:::Users:::User not founded")
                    };
            }
            var avatarUploadFolder = Configurations.AvatarFolder;
            var extension = avatar.FileName.GetExtension();
            var fileName = user.Id.GenerateAvatarFileName(extension);
            var path = Path.Combine(HttpContext.Current.Server.MapPath(avatarUploadFolder), fileName);
            var returnPath = string.Format("{0}{1}", avatarUploadFolder, fileName);
            user.AvatarFileName = fileName;
            var response = Update(user);

            //Refresh current user data
            if (user.Id == WorkContext.CurrentUser.Id)
            {
                WorkContext.CurrentUser = user;
            }

            if(response.Success)
            {
                avatar.SaveAs(path);
                response.Data = returnPath;
            }
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::Users:::Upload avatar successfully")
                : _localizedResourceServices.T("AdminModule:::Users:::Upload avatar failure. Please try again later."));
        }
        #endregion

        /// <summary>
        /// Update user data
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel UpdateUserData(XEditableModel model)
        {
            var user = GetById(model.Pk);
            if(user != null)
            {
                object value = model.Value;
                if(model.Name.Equals("BirthDay"))
                {
                    value = model.Value.ToDate();
                }
                user.SetProperty(model.Name, value);
                var response = Update(user);
                if (user.Id == WorkContext.CurrentUser.Id && response.Success)
                    WorkContext.CurrentUser = user;
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::Users:::Update user data successfully")
                    : _localizedResourceServices.T("AdminModule:::Users:::Update user data failure. Please try again later."));
            }
            return new ResponseModel
                {
                    Success = false,
                    Message = _localizedResourceServices.T("AdminModule:::Users:::User not founded")
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
            if(user != null)
            {
                user.Password = model.Password;
                var response = Update(user);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::Users:::Change password successfully")
                    : _localizedResourceServices.T("AdminModule:::Users:::Change password failure. Please try again later."));
            }
            return new ResponseModel
                {
                    Success = false,
                    Message = _localizedResourceServices.T("AdminModule:::Users:::User not founded")
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
