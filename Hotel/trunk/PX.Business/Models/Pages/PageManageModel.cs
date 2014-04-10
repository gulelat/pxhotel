using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.Pages;
using PX.Core.Ultilities;

namespace PX.Business.Models.Pages
{
    public class PageManageModel : BaseModel, IValidatableObject
    {
        #region Public Properties
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string FriendlyUrl { get; set; }

        public string Caption { get; set; }

        [Required]
        public string Content { get; set; }

        public int? ParentId { get; set; }

        public IEnumerable<SelectListItem> Parents { get; set; }

        public int? PageTemplateId { get; set; }

        public IEnumerable<SelectListItem> PageTemplates { get; set; }

        public int Status { get; set; }

        public IEnumerable<SelectListItem> StatusList { get; set; }

        public int Position { get; set; }

        public IEnumerable<SelectListItem> Positions { get; set; }

        public int? RelativePageId { get; set; }

        public IEnumerable<SelectListItem> RelativePages { get; set; } 

        /// <summary>
        /// Validate the model
        /// </summary>
        /// <param name="context">the validation context</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var pageServices = HostContainer.GetInstance<IPageServices>();
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (pageServices.IsTitleExisted(Id, Title))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::Pages:::ValidationMessage:::Title is existed."), new[]{ "Title"});
            }

            FriendlyUrl = string.IsNullOrWhiteSpace(FriendlyUrl) ? Title.ToUrlString() : FriendlyUrl.ToUrlString();
            if (pageServices.IsFriendlyUrlExisted(Id, FriendlyUrl))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::Pages:::ValidationMessage:::Friendly Url is existed."), new[] { "FriendlyUrl" });
            }
        }
        #endregion
    }
}
