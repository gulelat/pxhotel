using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PX.Business.Models;
using PX.Business.Models.UserModels;
using PX.EntityModel;
using PX.EntityModel.Enums;
using PX.EntityModel.Framework.Repositories;
using PX.Library.Common;

namespace PX.Business.Services.Users
{
    public class UserServices : IUserServices
    {
        #region Base
        public IQueryable<User> GetAll()
        {
            return UserRepository.GetAll();
        }
        public User GetById(int id)
        {
            return UserRepository.GetById(id);
        }
        public ResponseModel Insert(User user)
        {
            var response = new ResponseModel();
            try
            {
                UserRepository.Insert(user);
                response.Success = true;
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }
        public ResponseModel Update(User user)
        {
            var response = new ResponseModel();
            try
            {
                UserRepository.Update(user);
                response.Success = true;
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }
        public ResponseModel Delete(User user)
        {
            var response = new ResponseModel();
            try
            {
                UserRepository.Delete(user);
                response.Success = true;
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }
        public ResponseModel Delete(int id)
        {
            var response = new ResponseModel();
            try
            {
                UserRepository.Delete(id);
                response.Success = true;
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }
        #endregion

        public User GetUser(string email)
        {
            return GetAll().FirstOrDefault(u => u.Email.Equals(email));
        }

        /// <summary>
        /// search the users.
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> SearchUsers(UserSearchModel searchModel)
        {
            var users = GetAll();
            //if (User.CurrentUser.RoleEnums != UserEnums.UserTypesEnums.Admin)
            //{
            //    users = users.Where(u => u.RoleId != (int)UserEnums.UserTypesEnums.Admin);
            //}
            users = users.Where(u => (string.IsNullOrEmpty(searchModel.Email) || u.Email.Contains(searchModel.Email))
                                     && (string.IsNullOrEmpty(searchModel.IdentityNumber) || u.IdentityNumber.Contains(searchModel.IdentityNumber))
                                     && (!searchModel.StatusId.HasValue || u.StatusId == searchModel.StatusId)
                                     && (!searchModel.RoleId.HasValue || u.RoleId == searchModel.RoleId));
            return users;
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
        public IQueryable<Role> GetRoles()
        {
            return RoleRepository.GetAll();
        }
    }
}
