using System.Collections.Generic;
using System.Linq;
using PX.Business.Models;
using PX.Business.Models.UserModels;
using PX.Business.Services.Users;
using PX.EntityModel;
using PX.Library.Configuration;

namespace PX.Web.ViewModels.BackEnd.UserModels
{

    public class UserListingViewModel : ViewModelBase
    {
        private readonly IUserServices _userServices;

        public UserListingViewModel(IUserServices userServices)
        {
            _userServices = userServices;
            Pagination = new PaginationModel { Order = "asc", OrderName = "Email", PageSize = Configurations.AdminPageSizes };
            UserSearchViewModel = new UserSearchViewModel(userServices);
        }
        
        public UserListingViewModel(IUserServices userServices, int? page, string search)
            : this(userServices)
        {
            Pagination.PageIndex = (page.HasValue && page > 0) ? page.Value : 1;
            UserSearchViewModel.Name = search;
        }

        public void Search()
        {
            var userSearchModel = new UserSearchModel
                {
                    RoleId = UserSearchViewModel.RoleId,
                    StatusId = UserSearchViewModel.Status,
                    Email = UserSearchViewModel.Email,
                    DateOfBirth = UserSearchViewModel.DateOfBirth,
                    IdentityNumber = UserSearchViewModel.IdentityNumber
                };
            var users = _userServices.SearchUsers(userSearchModel);
            Users = Pagination.Paging(users).ToList();
        }

        public void DeleteUser(int id)
        {
            var response = _userServices.Delete(id);
            Search();
            Message = response.Message;
        }

        public void ChangeStatus(int userId, int status)
        {
            var user = _userServices.GetById(userId);
            user.StatusId = status;
            var response = _userServices.Update(user);
            Message = response.Message;
            ResponseStatusEnums = response.ResponseStatus;
        }

        #region Public Properties

        public PaginationModel Pagination { get; set; }

        public List<User> Users { get; set; }

        public UserSearchViewModel UserSearchViewModel { get; set; }

        #endregion
    }
}