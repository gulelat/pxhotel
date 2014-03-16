using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Models.DTO;
using AutoMapper;
using PX.EntityModel.Repositories;

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
        /// search the users.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchUsers(JqSearchIn si)
        {
            var users = GetAll().Select(u => new UserModel
            {
                Id = u.Id,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName,
                ImageFileName = u.ImageFileName,
                LastLogin = u.LastLogin,
                Phone = u.Phone,
                Password = u.Password,
                StatusId = u.StatusId,
                RoleId = u.RoleId,
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
        public IQueryable<Role> GetAllRoles()
        {
            return RoleRepository.GetAll();
        }

        /// <summary>
        /// Gets the user roles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetRoles()
        {
            return RoleRepository.GetAll().ToList().Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString(CultureInfo.InvariantCulture)
                });
        }

        /// <summary>
        /// Manage user
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="model"></param>
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
                    user.RoleId = model.RoleId;
                    user.StatusId = model.StatusId;
                    user.IdentityNumber = model.IdentityNumber;
                    return Update(user);
                case GridOperationEnums.Add:
                    user = Mapper.Map<UserModel, User>(model);
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
    }
}
