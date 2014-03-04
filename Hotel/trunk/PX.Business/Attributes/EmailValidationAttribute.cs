using System.ComponentModel.DataAnnotations;
using System.Web.ModelBinding;

namespace PX.Framework.Attributes
{
    public class EmailValidationAttribute : RegularExpressionAttribute
    {
        public EmailValidationAttribute()
            : base(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
        {
            ErrorMessage = "Invalid email";
        }

        static EmailValidationAttribute()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EmailValidationAttribute), typeof(RegularExpressionAttributeAdapter));
        }
    }
}
