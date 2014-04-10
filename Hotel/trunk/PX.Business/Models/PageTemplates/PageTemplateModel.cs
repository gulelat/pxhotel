using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PX.Business.Services.Localizes;
using PX.Business.Services.PageTemplates;
using PX.Core.Framework.Mvc.Environments;

namespace PX.Business.Models.PageTemplates
{
    public class PageTemplateModel : BaseModel, IValidatableObject
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public int? ParentId { get; set; }

        public string ParentName { get; set; }

        /// <summary>
        /// Validate the model
        /// </summary>
        /// <param name="context">the validation context</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (pageTemplateServices.GetAll().Any(u => u.Name.Equals(Name) && u.Id != Id))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::PageTemplates:::ValidationMessage:::Name is existed."), new[] { "Name" });
            }
        }
    }
}
