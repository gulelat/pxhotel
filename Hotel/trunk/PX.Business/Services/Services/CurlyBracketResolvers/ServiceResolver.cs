using System.Collections.Generic;
using PX.Business.Models.Services.CurlyBrackets;
using PX.Business.Mvc.Attributes;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Templates;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Services.Services.CurlyBracketResolvers
{
    [CurlyBracket(Name = "Service", CurlyBracket = "Services", Descrition = "Service curly bracket", Type = typeof(List<ServiceCurlyBracket>))]
    public class ServiceResolver : ICurlyBracketResolver
    {
        #region Private Properties
        private readonly ITemplateServices _templateServices;
        private readonly IServiceServices _serviceServices;
        private const int DefaultRandomNumber = 5;
        #endregion

        #region Public Properties

        public string DefaultTemplate
        {
            get { return "Default.Services"; }
        }
        #endregion

        #region Constructors
        public ServiceResolver()
        {
            _serviceServices = HostContainer.GetInstance<IServiceServices>();
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
                        DataType = typeof(List<ServiceCurlyBracket>).AssemblyQualifiedName,
                        Content = string.Empty,
                        IsDefaultTemplate = true
                    };
                _templateServices.Insert(template);
            }
        }

        /// <summary>
        /// Render Service curlybracket
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string Render(string[] parameters)
        {
            ParseParams(parameters);
            var templateManageModel = _templateServices.GetTemplateByName(Template) ??
                                                      _templateServices.GetTemplateByName(DefaultTemplate);

            var model = _serviceServices.GetServices(Count);
            return _templateServices.Parse(templateManageModel.Content, model, null, templateManageModel.CacheName);
        }
    }
}
