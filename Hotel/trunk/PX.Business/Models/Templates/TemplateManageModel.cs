﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Business.Services.Templates;
using RazorEngine.Templating;

namespace PX.Business.Models.Templates
{
    public class TemplateManageModel : BaseModel, IValidatableObject
    {
        #region Public Properties
        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DataType { get; set; }

        [Required]
        public string Content { get; set; }

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
            
            var razorEngineServices = new TemplateService();
            var type = Type.GetType(DataType);
            if (type != null)
            {
                var instance = Activator.CreateInstance(type);
                var razorValidMessage = string.Empty;
                try
                {
                    razorEngineServices.Parse(Content, instance, null, null);
                }
                catch (Exception exception)
                {
                    razorValidMessage = exception.Message;
                }
                if (!string.IsNullOrEmpty(razorValidMessage))
                    yield return new ValidationResult(string.Format(localizedResourceServices.T("AdminModule:::PageTemplates:::ValidationMessages:::TemplateCompileFailure:::Content is invalid. Error message: {0}."), razorValidMessage), new[] { "Content" });
            }
        }
        #endregion
    }
}
