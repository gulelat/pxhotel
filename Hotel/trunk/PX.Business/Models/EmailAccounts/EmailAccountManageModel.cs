using System.ComponentModel.DataAnnotations;
using PX.Business.Mvc.Attributes.Validation;

namespace PX.Business.Models.EmailAccounts
{
    public class EmailAccountManageModel
    {
        public int? Id { get; set; }

        [Required]
        [EmailValidation]
        public string Email { get; set; }

        public string DisplayName { get; set; }

        [Required]
        public string Host { get; set; }

        [Required]
        public int Port { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public bool EnableSsl { get; set; }

        [Required]
        public bool UseDefaultCredentials { get; set; }

        public bool IsDefault { get; set; }
    }
}
