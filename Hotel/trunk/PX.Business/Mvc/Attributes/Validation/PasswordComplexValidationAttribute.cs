using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using PX.Business.Models.Settings.SettingTypes;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.Settings;

namespace PX.Business.Mvc.Attributes.Validation
{
    /// <summary>
    /// Defines the <see cref="PasswordComplexValidationAttribute" /> object
    /// </summary>
    public class PasswordComplexValidationAttribute : ValidationAttribute
    {
        public const string StartRegexPattern = "^.*";
        public const string EndRegexPattern = ".*$";
        public const string DigitPattern = "(?=.*\\d)";
        public const string UpperLowerCaseMixedPattern = "(?=.*[a-z])(?=.*[A-Z])";
        public const string SymbolPattern = "(?=.*[!@#$%^\\*])";

        public override bool IsValid(object value)
        {
            var siteSettingManager = HostContainer.GetInstance<ISettingServices>();
            var localizedResourceServies = HostContainer.GetInstance<ILocalizedResourceServices>();

            if (string.IsNullOrEmpty((string)value))
            {
                ErrorMessage =  localizedResourceServies.T("AdminModule:::Users:::ValidationMessages:::RequiredPassword:::Password field is required");
                return false;
            }
            
            var passwordSetting = (PasswordSetting)siteSettingManager.LoadSetting<PasswordSettingResolver>();

            if (passwordSetting == null)
                return true;

            var errorMessage = new List<string>();

            var regexPatterns = Enumerable.Empty<string>();
            regexPatterns = regexPatterns.Concat(new[] { StartRegexPattern });

            if (passwordSetting.PasswordMustHaveDigit)
            {
                errorMessage.Add("1 digit");
                regexPatterns = regexPatterns.Concat(new[] { DigitPattern });
            }

            if (passwordSetting.PasswordMustHaveSymbol)
            {
                errorMessage.Add("1 symbol");
                regexPatterns = regexPatterns.Concat(new[] { SymbolPattern });
            }

            if (passwordSetting.PasswordMustHaveUpperAndLowerCaseLetters)
            {
                errorMessage.Add("both upper and lower case characters");
                regexPatterns = regexPatterns.Concat(new[] { UpperLowerCaseMixedPattern });
            }
            regexPatterns = regexPatterns.Concat(new[] { EndRegexPattern });

            var regexPatternString = string.Join("", regexPatterns);
            var valid = new Regex(regexPatternString).IsMatch(((string)value));
            if (!valid)
            {
                ErrorMessage = "The password must have " + string.Join(", ", errorMessage);
                return false;
            }

            var pwdLength = passwordSetting.PasswordMinLengthRequired > 0
                                                            ? passwordSetting.PasswordMinLengthRequired
                                                            : 8;
            if (((string)value).Length < pwdLength)
            {
                ErrorMessage = string.Format("Password must have at least {0} characters in length", pwdLength);
                return false;
            }

            return true;
        }
    }
}
