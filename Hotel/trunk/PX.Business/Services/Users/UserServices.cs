using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using PX.Business.Models.UserGroups;
using PX.Business.Models.UserModels;
using PX.Business.Models.Users;
using PX.Business.Models.Users.Logins;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using AutoMapper;
using PX.EntityModel.Repositories;
using UserGroup = PX.EntityModel.UserGroup;

namespace PX.Business.Services.Users
{
    public class UserServices : IUserServices
    {
        #region Base
        public IQueryable<User> GetAll()
        {
            return UserRepository.GetAll();
        }
        public User GetById(int? id)
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
        public ResponseModel Delete(int id)
        {
            return UserRepository.Delete(id);
        }
        #endregion

        public User GetUser(string email)
        {
            return GetAll().FirstOrDefault(u => u.Email.Equals(email));
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
        /// Gets the user roles.
        /// </summary>
        /// <returns></returns>
        public IQueryable<UserGroup> GetAllUserGroups()
        {
            return UserGroupRepository.GetAll();
        }

        /// <summary>
        /// Gets the user roles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetRoles()
        {
            return UserGroupRepository.GetAll().ToList().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString(CultureInfo.InvariantCulture)
            });
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
                ImageFileName = u.ImageFileName,
                LastLogin = u.LastLogin,
                Phone = u.Phone,
                IdentityNumber = u.IdentityNumber,
                StatusId = u.StatusId,
                UserGroupId = u.UserGroupId,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(users);
        }

        /// <summary>
        /// search the users.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchUserGroups(JqSearchIn si)
        {
            var userGroups = GetAllUserGroups().Select(u => new UserGroupModel
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
        /// Manage user
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the user model</param>
        /// <returns></returns>
        public ResponseModel ManageUser(GridOperationEnums operation, UserModel model)
        {
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
                    user.UserGroupId = model.UserGroupId;
                    user.StatusId = model.StatusId;
                    user.IdentityNumber = model.IdentityNumber;
                    user.RecordActive = model.RecordActive;
                    user.RecordOrder = 0;
                    return Update(user);
                case GridOperationEnums.Add:
                    user = Mapper.Map<UserModel, User>(model);
                    user.RecordOrder = 0;
                    return Insert(user);
                case GridOperationEnums.Del:
                    return Delete(model.Id);
            }
            return new ResponseModel
            {
                Success = false,
                Message = "Object not founded"
            };
        }

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

        #region Login/ Register / Forgot Password
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
                    User.CurrentUser = user;

                    user.LastLogin = DateTime.Now;
                    Update(user);
                    var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
                    return new ResponseModel
                        {
                            Success = true,
                            Message = "Login Success.",
                            Data = urlHelper.Action("LoginSuccess", "Account")
                        };
                }
            }
            return new ResponseModel
                {
                    Success = false,
                    Message = "Invalid email or password. Please try again."
                };
        }
        #endregion
    }
}
