using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.Users;
using PX.Core.Framework.Enums;
using PX.Core.Ultilities;

namespace PX.Business.Models.Users
{
    public class UserModel : BaseModel, IValidatableObject
    {
        #region Public Properties

        [Key]
        public int Id { get; set; }

        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [DisplayName(@"Fist Name")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName(@"Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string IdentityNumber { get; set; }

        public string AvatarFileName { get; set; }

        public List<int> UserGroupIds { get; set; }

        public DateTime? LastLogin { get; set; }

        public int Status { get; set; }

        public string StatusName
        {
            get
            {
                return EnumUtilities.GetName((UserEnums.UserStatusEnums)Status);
            }
            set
            {
                Status = value.ToInt();
            }
        }
        #endregion

        public string UserGroups { get; set; }

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
