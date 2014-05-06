using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.Templates;
using PX.EntityModel;
using RazorEngine.Templating;

namespace PX.Business.Models.Templates
{
    public class TemplateManageModel : BaseModel, IValidatableObject
    {
        public TemplateManageModel()
        {
            
        }

        public TemplateManageModel(Template template)
        {
            Id = template.Id;
            Name = template.Name;
            Content = template.Content;
            DataType = template.DataType;
        }

        public TemplateManageModel(TemplateLog log)
            : this()
        {
            Id = log.TemplateId;
            Name = log.Name;
            Content = log.Content;
            DataType = log.DataType;
        }

        #region Public Properties
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DataType { get; set; }

        [Required]
        public string Content { get; set; }
        #endregion

        /// <summary>
        /// Validate the model
        /// </summary>
        /// <param name="context">the validation context</param>
        /// <returns></returns>
        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            var templateServices = HostContainer.GetInstance<ITemplateServices>();
            var localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            if (templateServices.IsTemplateNameExisted(Id, Name))
            {
                yield return new ValidationResult(localizedResourceServices.T("AdminModule:::PageTemplates:::ValidationMessages:::ExistingName:::Name is existed."), new[]{ "Name"});
            }
            
            var type = Type.GetType(DataType);
            if (type != null)
            {
                var instance = Activator.CreateInstance(type);
                var razorValidMessage = string.Empty;
                try
                {
                    templateServices.Parse(Content, instance, null, null);
                }
                catch (TemplateCompilationException exception)
                {
                    razorValidMessage = string.Join("\n", exception.Errors);
                }
                if (!string.IsNullOrEmpty(razorValidMessage))
                    yield return new ValidationResult(string.Format(localizedResourceServices.T("AdminModule:::PageTemplates:::ValidationMessages:::TemplateCompileFailure:::Content is invalid. Error message: {0}."), razorValidMessage), new[] { "Content" });
            }
        }
    }
}
