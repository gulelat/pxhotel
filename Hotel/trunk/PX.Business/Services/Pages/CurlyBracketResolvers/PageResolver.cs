using PX.Business.Models.Pages;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Localizes;
using PX.Business.Services.Templates;
using PX.Core.Framework.Mvc.Environments;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Services.Pages.CurlyBracketResolvers
{
    [CurlyBracket(Name = "PageContent", CurlyBracket = "PageContent", Descrition = "Load content of page", Type = typeof(PageRenderModel))]
    public class PageResolver : ICurlyBracketResolver
    {
        #region Private Properties
        private readonly ITemplateServices _templateServices;
        private readonly IPageServices _pageServices;
        private readonly ILocalizedResourceServices _localizedResourceServices;

        #endregion

        public string DefaultTemplate
        {
            get { return "Default.GetPageContentTemplate"; }
        }

        #region Constructor
        public PageResolver()
        {
            _pageServices = HostContainer.GetInstance<IPageServices>();
            _templateServices = HostContainer.GetInstance<ITemplateServices>();
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();

        }

        #endregion

        #region Parse Params

        private int PageId { get; set; }

        private string Template { get; set; }

        private void ParseParams(string[] parameters)
        {
            /*
             * Params:
             * * Page Id
             * * Template
             */

            //Count
            if (parameters.Length > 1)
            {
                PageId = parameters[1].ToInt(0);
            }

            //Template
            if (parameters.Length > 1)
            {
                Template = parameters[2];
            }
        }
        #endregion

        /// <summary>
        /// Initialize template and default data
        /// </summary>
        public void Initialize()
        {
            if (_templateServices.GetTemplateByName(DefaultTemplate) == null)
            {
                var template = new Template
                {
                    Name = DefaultTemplate,
                    DataType = typeof(PageRenderModel).FullName,
                    Content = string.Empty,
                    IsDefaultTemplate = true
                };
                _templateServices.Insert(template);
            }
        }

        /// <summary>
        /// Render curly bracket
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string Render(string[] parameters)
        {
            ParseParams(parameters);

            var page = _pageServices.GetById(PageId);

            if(page == null)
            {
                return _localizedResourceServices.T("CurlyBracketsRendering:::Messages:::GetPageContentIdNotFounded:::Page id is invalid. Please check the data again.");
            }

            var pageRenderModel = new PageRenderModel(page);

            var template = _templateServices.GetTemplateByName(Template) ??
                           _templateServices.GetTemplateByName(DefaultTemplate);
            return _templateServices.RenderTemplate(template.Content, pageRenderModel, template.CacheName);
        }
    }
}
