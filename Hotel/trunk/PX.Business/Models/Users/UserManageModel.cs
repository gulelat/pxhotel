using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;
using System.Web.Mvc;
using PX.Core.Configurations;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.Users;
using PX.Core.Framework.Enums;
using PX.Core.Ultilities;

namespace PX.Business.Models.Users
{
    public class UserManageModel : BaseModel, IValidatableObject
    {
        #region Public Properties

        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [DisplayName(@"Confirm Password")]
        [System.ComponentModel.DataAnnotations.Compare("Password")]
        public string ConfirmPassword { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        public int? Gender { get; set; }

        public IEnumerable<SelectListItem> Genders { get; set; }

        public string About { get; set; }

        public string Address { get; set; }

        [Required]
        public string IdentityNumber { get; set; }

        public string AvatarFileName { get; set; }

        public IEnumerable<int> UserGroupIds { get; set; }

        public IEnumerable<SelectListItem> UserGroups { get; set; }

        public string Facebook { get; set; }

        public string Twitter { get; set; }

        public string Google { get; set; }

        public DateTime? BirthDay { get; set; }

        public DateTime? LastLogin { get; set; }

        public int Status { get; set; }

        public IEnumerable<SelectListItem> StatusList { get; set; }

        public string AvatarPath
        {
            get
            {
                if (string.IsNullOrEmpty(AvatarFileName) || !File.Exists(HttpContext.Current.Server.MapPath(Configurations.AvatarFolder + AvatarFileName)))
                {
                    return Configurations.AvatarFolder + Configurations.NoAvatar;
                }
                return Configurations.AvatarFolder + AvatarFileName;
            }
        }

        public double LastLoginHours
        {
            get
            {
                if (LastLogin.HasValue)
                    return (DateTime.Now - LastLogin.Value).TotalHours;
                return 0;
            }
        }

        public int? Age
        {
            get
            {
                if (BirthDay.HasValue)
                {
                    return (int)(DateTime.Now - BirthDay.Value).TotalDays / 365;
                }
                return null;
            }
        }
        #endregion
        
        /// <summary>
        /// Validate the model
        /// </summary>
        /// <param name="context">the validation context</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var userServices = HostContainer.GetInstance<IUserServices>();
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (userServices.IsEmailExisted(Id, Email))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::Users:::ValidationMessages:::ExistingEmail:::Email is existed."));
            }
        }
    }
}
