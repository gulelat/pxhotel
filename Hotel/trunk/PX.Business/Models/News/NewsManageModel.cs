using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using PX.Business.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.News;

namespace PX.Business.Models.News
{
    public class NewsManageModel : BaseModel, IValidatableObject
    {
        #region Public Properties
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public string Content { get; set; }

        public List<int> NewsCategoryIds { get; set; }

        public IEnumerable<SelectListItem> NewsCategories { get; set; }

        public int Status { get; set; }

        public IEnumerable<SelectListItem> StatusList { get; set; }

        /// <summary>
        /// Validate the model
        /// </summary>
        /// <param name="context">the validation context</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var newsServices = HostContainer.GetInstance<INewsServices>();
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (newsServices.IsTitleExisted(Id, Title))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::Newss:::ValidationMessage:::News title is existed."), new[]{ "Title"});
            }
        }
        #endregion
    }
}
