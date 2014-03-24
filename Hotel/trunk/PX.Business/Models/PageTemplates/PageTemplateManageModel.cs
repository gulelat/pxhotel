using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using PX.Business.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.PageTemplates;
using PX.Core.Configurations.Constants;

namespace PX.Business.Models.PageTemplates
{
    public class PageTemplateManageModel : BaseModel, IValidatableObject
    {
        public PageTemplateManageModel()
        {
            Content = DefaultConstants.CurlyBracketRenderBody;
        }

        #region Public Properties
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        public int? ParentId { get; set; }

        public IEnumerable<SelectListItem> Parents { get; set; }

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
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::PageTemplates:::ValidationMessage:::Name is existed."), new[]{ "Name"});
            }

            if (!Content.Contains(DefaultConstants.CurlyBracketRenderBody))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::PageTemplates:::ValidationMessage:::Missing {RenderBody} Curly Bracket."), new[] { "Content" });
            }
        }
        #endregion
    }
}
