using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Mvc.Environments;

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
