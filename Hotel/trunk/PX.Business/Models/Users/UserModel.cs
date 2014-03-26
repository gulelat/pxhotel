using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PX.Business.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.Users;

namespace PX.Business.Models.Users
{
    public class UserModel : BaseModel, IValidatableObject
    {
        #region Public Properties

        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-|\.|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.?$")]
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

        public int UserGroupId { get; set; }

        public DateTime? LastLogin { get; set; }

        [Required]
        public int Status { get; set; }
        #endregion

        public string UserGroupName { get; set; }

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
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::Users:::ValidationMessage:::Email is existed."));
            }
        }
    }
}
