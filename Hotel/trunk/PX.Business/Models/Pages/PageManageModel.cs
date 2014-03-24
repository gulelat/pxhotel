using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using PX.Business.Mvc.Enums;
using PX.Business.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.Pages;

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

        public PageEnums.PositionEnums Position { get; set; }

        public IEnumerable<SelectListItem> Positions { get; set; }

        public int RelativeOrder { get; set; }

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
            if (pageServices.GetAll().Any(u => u.Title.Equals(Title) && u.Id != Id))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::Pages:::ValidationMessage:::Title is existed."), new[]{ "Title"});
            }

            if (pageServices.GetAll().Any(u => u.FriendlyUrl.Equals(FriendlyUrl) && u.Id != Id))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::Pages:::ValidationMessage:::Friendly Url is existed."), new[] { "FriendlyUrl" });
            }
        }
        #endregion
    }
}
