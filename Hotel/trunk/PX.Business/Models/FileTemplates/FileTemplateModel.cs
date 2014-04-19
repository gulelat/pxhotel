using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PX.Business.Services.FileTemplates;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Mvc.Environments;

namespace PX.Business.Models.FileTemplates
{
    public class FileTemplateModel : BaseModel, IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Controller { get; set; }

        [Required]
        public string Action { get; set; }

        public string Parameters { get; set; }

        public int? PageTemplateId { get; set; }

        public string PageTemplateName { get; set; }
        
        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        /// <summary>
        /// Validate the model
        /// </summary>
        /// <param name="context">the validation context</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var fileTemplateServices = HostContainer.GetInstance<IFileTemplateServices>();
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (fileTemplateServices.GetAll().Any(u => u.Name.Equals(Name) && u.Id != Id))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::FileTemplates:::ValidationMessages:::ExistingName:::File template name is existed."), new[] { "Name" });
            }
        }
    }
}
