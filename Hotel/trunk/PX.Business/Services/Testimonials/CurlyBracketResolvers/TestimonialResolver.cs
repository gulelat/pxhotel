using System;
using PX.Business.Models.Templates;
using PX.Business.Models.Testimonials.CurlyBrackets;
using PX.Business.Mvc.Attributes;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Templates;
using PX.Core.Configurations.Constants;
using PX.EntityModel;
using RazorEngine.Templating;

namespace PX.Business.Services.Testimonials.CurlyBracketResolvers
{
    [CurlyBracket(Name = "Testimonial", CurlyBracket = "Testimonial", Descrition = "Testimonial curly bracket", Type = typeof(TestimonialCurlyBracket))]
    public class TestimonialResolver : ICurlyBracketResolver
    {
        #region Private Properties
        private readonly ITemplateServices _templateServices;
        private readonly ITestimonialServices _testimonialServices;
        public string DefaultTemplate()
        {
            return "Default.Testimonials";
        }
        #endregion

        #region Constructors
        public TestimonialResolver()
        {
            _testimonialServices = HostContainer.GetInstance<ITestimonialServices>();
            _templateServices = HostContainer.GetInstance<ITemplateServices>();
        }

        #endregion

        public void Initialize()
        {
            if (_templateServices.GetTemplateByName(DefaultTemplate()) == null)
            {
                var template = new Template
                    {
                        Name = DefaultTemplate(),
                        DataType = typeof(TestimonialCurlyBracket).FullName,
                        Content = "{Model.Author}",
                        RecordActive = true,
                        RecordOrder = 0,
                        Created = DateTime.Now,
                        CreatedBy = DefaultConstants.DefaultSystemAccount
                    };
                _templateServices.Insert(template);
            }
        }

        public string Render(string[] parameters)
        {
            var count = 5;
            TemplateManageModel templateManageModel = null;
            if (parameters.Length > 1)
            {
                if (int.TryParse(parameters[1], out count))
                {
                    if (parameters.Length > 2)
                    {
                        templateManageModel = _templateServices.GetTemplateByName(parameters[2]);
                    }
                }
            }
            if (templateManageModel == null)
            {
                templateManageModel = _templateServices.GetTemplateByName(DefaultTemplate());
            }
            var model = _testimonialServices.GetRandom(count);
            return _templateServices.RenderTemplate(templateManageModel.Content, model, templateManageModel.Name);
        }
    }
}
