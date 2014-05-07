using PX.Business.Mvc.Attributes.Validation;

namespace PX.Business.Models.EmailAccounts
{
    public class TestEmailModel
    {
        public int Id { get; set; }

        [EmailValidation]
        public string Email { get; set; }
    }
}
