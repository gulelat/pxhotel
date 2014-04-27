using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PX.Business.Services.Services;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;

namespace PX.Business.Models.Services
{
    public class ServiceManageModel : BaseModel, IValidatableObject
    {
        #region Public Properties
        public int? Id { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public int Status { get; set; }

        public IEnumerable<SelectListItem> StatusList { get; set; }

        /// <summary>
        /// Validate the model
        /// </summary>
        /// <param name="context">the validation context</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var serviceServices = HostContainer.GetInstance<IServiceServices>();
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (serviceServices.IsTitleExisted(Id, Title))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::Services:::ValidationMessages:::ExistingTitle:::Service title is existed."), new[]{ "Title"});
            }
        }
        #endregion
    }
}
