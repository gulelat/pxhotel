using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Attributes.Validation;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.Users;

namespace PX.Business.Models.Users
{
    public class ChangePasswordModel : IValidatableObject
    {
        #region Public Properties
        
        public int UserId { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        [PasswordComplexValidation]
        public string Password { get; set; }

        [Required]
        [DisplayName(@"Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
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
            if (!userServices.GetById(UserId).Password.Equals(OldPassword))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::Users:::ValidationMessages:::InvalidOldPassword:::Wrong old password."));
            }
        }
    }
}
