using System.Collections.Generic;
using PX.Business.Models.Testimonials.CurlyBrackets;
using PX.Business.Mvc.Attributes;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Templates;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Services.Testimonials.CurlyBracketResolvers
{
    [CurlyBracket(Name = "Testimonial", CurlyBracket = "Testimonials", Descrition = "Testimonial curly bracket", Type = typeof(List<TestimonialCurlyBracket>))]
    public class TestimonialResolver : ICurlyBracketResolver
    {
        #region Private Properties
        private readonly ITemplateServices _templateServices;
        private readonly ITestimonialServices _testimonialServices;
        private const int DefaultRandomNumber = 5;
        #endregion

        #region Public Properties

        public string DefaultTemplate
        {
            get { return "Default.Testimonials"; }
        }
        #endregion

        #region Constructors
        public TestimonialResolver()
        {
            _testimonialServices = HostContainer.GetInstance<ITestimonialServices>();
            _templateServices = HostContainer.GetInstance<ITemplateServices>();
        }

        #endregion

        #region Parse Params

        private int Count { get; set; }

        private string Template { get; set; }

        private void ParseParams(string[] parameters)
        {
            /*
             * Params:
             * * Count
             * * Template
             */
            Count = DefaultRandomNumber;
            Template = DefaultTemplate;

            //Count
            if(parameters.Length > 1)
            {
                Count = parameters[1].ToInt(DefaultRandomNumber);
            }

            //Template
            if (parameters.Length > 2)
            {
                Template = parameters[2];
            }
        }
        #endregion

        /// <summary>
        /// Initialize template and required data
        /// </summary>
        public void Initialize()
        {
            if (_templateServices.GetTemplateByName(DefaultTemplate) == null)
            {
                var template = new Template
                    {
                        Name = DefaultTemplate,
                        DataType = typeof(List<TestimonialCurlyBracket>).AssemblyQualifiedName,
                        Content = string.Empty,
                        IsDefaultTemplate = true
                    };
                _templateServices.Insert(template);
            }
        }

        /// <summary>
        /// Render testimonial curlybracket
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string Render(string[] parameters)
        {
            ParseParams(parameters);
            var templateManageModel = _templateServices.GetTemplateByName(Template) ??
                                                      _templateServices.GetTemplateByName(DefaultTemplate);

            var model = _testimonialServices.GetRandom(Count);
            return _templateServices.Parse(templateManageModel.Content, model, null, templateManageModel.CacheName);
        }
    }
}
