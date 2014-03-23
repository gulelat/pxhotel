using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PX.Business.Mvc.Environments;
using PX.Business.Services.Localizes;

namespace PX.Business.Mvc.Attributes
{
    public class EmailValidationAttribute : RegularExpressionAttribute
    {
        public EmailValidationAttribute()
            : base(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")
        {
            var localizedResourceServies = HostContainer.GetInstance<ILocalizedResourceServices>();
            ErrorMessage = localizedResourceServies.T("AdminModule:::Users:::Validation:::Email is invalid");
        }

        static EmailValidationAttribute()
        {
            DataAnnotationsModelValidatorProvider.RegisterAdapter(typeof(EmailValidationAttribute), typeof(RegularExpressionAttributeAdapter));
        }
    }
}
