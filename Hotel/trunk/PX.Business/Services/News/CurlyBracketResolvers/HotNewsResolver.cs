using System.Collections.Generic;
using PX.Business.Models.News.CurlyBrackets;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Templates;
using PX.Core.Framework.Mvc.Environments;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Services.News.CurlyBracketResolvers
{
    [CurlyBracket(Name = "Hot News", CurlyBracket = "HotNews", Descrition = "Hot news curly bracket", Type = typeof(List<NewsCurlyBracket>))]
    public class HotNewsResolver : ICurlyBracketResolver
    {
        #region Private Properties
        private readonly ITemplateServices _templateServices;
        private readonly INewsServices _newsServices;
        private const int DefaultRandomNumber = 5;
        #endregion

        #region Public Properties

        public string DefaultTemplate
        {
            get { return "Default.HotNews"; }
        }
        #endregion

        #region Constructors
        public HotNewsResolver()
        {
            _newsServices = HostContainer.GetInstance<INewsServices>();
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
                        DataType = typeof(List<NewsCurlyBracket>).AssemblyQualifiedName,
                        CurlyBracket = "{HotNews}",
                        Content = string.Empty,
                        IsDefaultTemplate = true
                    };
                _templateServices.Insert(template);
            }
        }

        /// <summary>
        /// Render News curlybracket
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string Render(string[] parameters)
        {
            ParseParams(parameters);
            var templateManageModel = _templateServices.GetTemplateByName(Template) ??
                                                      _templateServices.GetTemplateByName(DefaultTemplate);

            var model = _newsServices.GetHotNews(Count);
            return _templateServices.Parse(templateManageModel.Content, model, null, templateManageModel.CacheName);
        }
    }
}
