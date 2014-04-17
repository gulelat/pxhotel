using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PX.Business.Services.FileTemplates;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Configurations.Constants;

namespace PX.Business.Models.FileTemplates
{
    public class FileTemplateManageModel : BaseModel, IValidatableObject
    {
        public FileTemplateManageModel()
        {
            Content = DefaultConstants.RenderBody;
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
            var fileTemplateServices = HostContainer.GetInstance<IFileTemplateServices>();
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (fileTemplateServices.IsFileTemplateNameExisted(Id, Name))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::FileTemplates:::ValidationMessage:::Name is existed."), new[] { "Name" });
            }
        }
    }
}
