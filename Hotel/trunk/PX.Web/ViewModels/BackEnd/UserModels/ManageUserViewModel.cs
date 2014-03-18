using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PX.EntityModel.Enums;
using PX.Library.Configuration;

namespace PX.Web.ViewModels.BackEnd.UserModels
{
    public class ManageUserViewModel : ViewModelBase, IValidatableObject
    {
        public ManageUserViewModel()
        {
            Status = UserUtilities.GetStatus();
            Roles = UserUtilities.GetRoles().ToList();
            UserInfo = new UserInfo { ImageFileName = Configurations.DefaultUserImage };

            ImageCropViewModel = new ImageCropViewModel()
            {
                ToThumbnail = true,
                Folder = Configurations.DefaultUserFolder,
                FileName = Configurations.DefaultUserImage,
                Ratio = Configurations.DefaultUserRatio
            };
        }

        public ManageUserViewModel(int? id)
            : this()
        {
            if (!id.HasValue)
                return;
            User = UserUtilities.GetUser(id.Value);
            if (User == null)
                return;

            if(User.CurrentUser.RoleEnums != UserEnums.UserTypesEnums.Admin)
            {
                if(User.RoleEnums == UserEnums.UserTypesEnums.Admin)
                {
                    return;
                }
            }

            User.ConfirmPassword = User.Password;
            if (User.RoleEnums == UserEnums.UserTypesEnums.Admin || User.RoleEnums == UserEnums.UserTypesEnums.Moderator || User.RoleEnums == UserEnums.UserTypesEnums.Parent)
            {
                Schools = new List<School>();
            }
            UserInfo = User.UserInfo;

            ImageCropViewModel.FileName = User.CurrentImageFileName;
        }

        public bool SaveChanges()
        {
            try
            {
                if (ImageCropViewModel.SaveLastFile(User.UserName))
                {
                    if (!string.IsNullOrEmpty(UserInfo.ImageFileName) && !UserInfo.ImageFileName.Equals(ImageCropViewModel.FileName))
                    {
                        ImageCropViewModel.DeleteCurrentFile(Configurations.DefaultUserFolder, UserInfo.ImageFileName);
                    }
                    if (!ImageCropViewModel.FileName.Equals(Configurations.DefaultUserImage))
                        UserInfo.ImageFileName = ImageCropViewModel.FileName;
                }
                ImageCropViewModel.RemoveTempFiles();
                if (!UserWorkUnits.SaveChanges(UserInfo))
                {
                    ErrorMessage = SystemMessages.SaveError;
                    return false;
                }
                User.InfoId = UserInfo.Id;

                if (!UserWorkUnits.SaveChanges(User))
                {
                    ErrorMessage = SystemMessages.SaveError;
                    return false;
                }

                StatusMessage = SystemMessages.SaveSuccess;
            }
            catch
            {
                StatusMessage = SystemMessages.SaveError;
                return false;
            }
            return true;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var checkUserNameUser = UserUtilities.GetUser(User.UserName);
            if (checkUserNameUser != null && User.Id != checkUserNameUser.Id)
            {
                yield return new ValidationResult(SystemMessages.ExistingUsername, new string[] { "User.UserName" });
            }

            var checkEmailUser = UserUtilities.GetUserByEmail(UserInfo.Email);
            if (checkEmailUser != null && User.Id != checkEmailUser.Id)
            {
                yield return new ValidationResult(SystemMessages.ExistingEmail, new string[] { "UserInfo.Email" });
            }

            if (User.RoleId == (int) UserEnums.UserTypesEnums.SchoolManager ||
                User.RoleId == (int) UserEnums.UserTypesEnums.Teacher)
            {
                if (!User.SchoolId.HasValue)
                {
                    yield return new ValidationResult(SystemMessages.RequiredSchool, new string[] {"User.SchoolId"});
                }
            }
        }

        #region Public Properties

        public User User { get; set; }

        public UserInfo UserInfo { get; set; }

        public Kid Kid { get; set; }

        public List<Role> Roles { get; set; }

        public List<School> Schools { get; set; }

        public IEnumerable<SelectListItem> Status { get; set; }

        public ImageCropViewModel ImageCropViewModel { get; set; }

        #endregion
    }
}