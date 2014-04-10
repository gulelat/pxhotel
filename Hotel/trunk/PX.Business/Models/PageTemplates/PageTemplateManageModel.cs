using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.CurlyBrackets;
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
            var curlyBracketServices = HostContainer.GetInstance<ICurlyBracketServices>();
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (pageTemplateServices.IsPageTemplateNameExisted(Id, Name))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::PageTemplates:::ValidationMessage:::Name is existed."), new[]{ "Name"});
            }

            //Check if content is valid
            if (curlyBracketServices.IsPageTemplateValid(Content))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::PageTemplates:::ValidationMessage:::Template Content is not valid, please check {RenderBody} curly bracket."), new[] { "Content" });
            }
        }
        #endregion
    }
}
