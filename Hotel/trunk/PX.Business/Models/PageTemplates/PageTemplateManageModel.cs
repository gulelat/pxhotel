using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PX.Core.Configurations;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.CurlyBrackets;
using PX.Business.Services.Localizes;
using PX.Business.Services.PageTemplates;
using PX.EntityModel;

namespace PX.Business.Models.PageTemplates
{
    public class PageTemplateManageModel : BaseModel, IValidatableObject
    {
        private readonly IPageTemplateServices _pageTemplateServices;

        public PageTemplateManageModel()
        {
            _pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
            Content = Configurations.RenderBody;
            Parents = _pageTemplateServices.GetPossibleParents();
        }

        public PageTemplateManageModel(PageTemplate template)
            : this()
        {

            Id = template.Id;
            Name = template.Name;
            Content = template.Content;
            ParentId = template.ParentId;
            Parents = _pageTemplateServices.GetPossibleParents(template.Id);
        }

        public PageTemplateManageModel(PageTemplateLog log)
            : this()
        {
            Id = log.PageTemplateId;
            Name = log.Name;
            Content = log.Content;
            ParentId = log.ParentId;
            Parents = _pageTemplateServices.GetPossibleParents(log.PageTemplateId);
        }

        #region Public Properties
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }

        public int? ParentId { get; set; }

        public IEnumerable<SelectListItem> Parents { get; set; }
        #endregion

        /// <summary>
        /// Validate the model
        /// </summary>
        /// <param name="context">the validation context</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var curlyBracketServices = HostContainer.GetInstance<ICurlyBracketServices>();
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (_pageTemplateServices.IsPageTemplateNameExisted(Id, Name))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::PageTemplates:::ValidationMessages:::ExistingName:::Name is existed."), new[] { "Name" });
            }

            //Check if content is valid
            if (curlyBracketServices.IsPageTemplateValid(Content))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::PageTemplates:::ValidationMessages:::InvalidContentFormat:::Template Content is not valid, please check {RenderBody} curly bracket."), new[] { "Content" });
            }
        }
    }
}
