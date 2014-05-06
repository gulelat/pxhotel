using System.Web;
using PX.Business.Models.News.CurlyBrackets;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Templates;
using PX.Core.Framework.Mvc.Environments;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Services.News.CurlyBracketResolvers
{
    [CurlyBracket(Name = "News", CurlyBracket = "NewsListing", Descrition = "News curly bracket", Type = typeof(NewsListingModel))]
    public class NewsListingResolver : ICurlyBracketResolver
    {
        #region Private Properties
        private readonly ITemplateServices _templateServices;
        private readonly INewsServices _newsServices;
        private const int DefaultPageSize = 5;
        #endregion

        #region Public Properties

        public string DefaultTemplate
        {
            get { return "Default.NewsListing"; }
        }
        #endregion

        #region Constructors
        public NewsListingResolver()
        {
            _newsServices = HostContainer.GetInstance<INewsServices>();
            _templateServices = HostContainer.GetInstance<ITemplateServices>();
        }

        #endregion

        #region Parse Params

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        private string Template { get; set; }

        private void ParseParams(string[] parameters)
        {
            /*
             * Params:
             * * Template
             */
            Template = DefaultTemplate;
            PageSize = DefaultPageSize;

            //Template
            if (parameters.Length > 1)
            {
                PageSize = parameters[1].ToInt(DefaultPageSize);
            }

            //Template
            if (parameters.Length > 2)
            {
                Template = parameters[2];
            }

            var context = HttpContext.Current.Request;
            PageIndex = context.QueryString["page"].ToInt(0);
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
                        DataType = typeof(NewsListingModel).AssemblyQualifiedName,
                        CurlyBracket = "{NewsListing}",
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

            var model = _newsServices.GetNewsListing(PageIndex, PageSize);
            return _templateServices.Parse(templateManageModel.Content, model, null, templateManageModel.CacheName);
        }
    }
}
