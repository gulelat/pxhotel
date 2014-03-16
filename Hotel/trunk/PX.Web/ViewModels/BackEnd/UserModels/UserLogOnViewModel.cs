using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Security;
using PX.Business.Services.Users;
using PX.Core.Framework.Enums;
using PX.EntityModel;
using PX.EntityModel.Resources;

namespace PX.Web.ViewModels.BackEnd.UserModels
{
    public class UserLogOnViewModel : ViewModelBase
    {
        private readonly IUserServices _userServices;
        public UserLogOnViewModel(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public UserLogOnViewModel(string returnUrl)
        {
            RememberMe = false;
            ReturnUrl = returnUrl;
        }

        public void Login()
        {
            User = _userServices.GetUser(UserName);
            if (User != null)
            {
                if (User.Password == Password && User.StatusEnums == UserEnums.UserStatusEnums.Active)
                {
                    FormsAuthentication.SetAuthCookie(Convert.ToString(User.Id), true);
                    User.CurrentUser = User;
                    LoginStatus = true;
                }
            }
            Message = SystemResources.InvalidUserNameOrPassword;
            ResponseStatusEnums = ResponseStatusEnums.Warning;
            LoginStatus = false;
        }

        #region Public Properties

        [Required(ErrorMessageResourceType = typeof(SystemResources), ErrorMessageResourceName = "RequiredUserName")]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(SystemResources), ErrorMessageResourceName = "RequiredPassword")]
        public string Password { get; set; }

        public bool LoginStatus { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }

        public User User { get; set; }

        #endregion
    }
}