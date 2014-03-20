using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PX.Business.Models.Users
{
    public class ChangePasswordModel
    {
        public int UserId { get; set; }

        [Required]
        public string OldPassword { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [DisplayName(@"Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
