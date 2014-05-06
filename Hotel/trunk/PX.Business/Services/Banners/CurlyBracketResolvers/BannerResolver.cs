using PX.Business.Models.Banners.CurlyBrackets;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Localizes;
using PX.Business.Services.Templates;
using PX.Core.Framework.Mvc.Environments;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Services.Banners.CurlyBracketResolvers
{
    [CurlyBracket(Name = "Single Banner", CurlyBracket = "Banner", Descrition = "Load single banner", Type = typeof(BannerCurlyBracket))]
    public class BannerResolver : ICurlyBracketResolver
    {
        #region Private Properties
        private readonly ITemplateServices _templateServices;
        private readonly IBannerServices _bannerServices;
        private readonly ILocalizedResourceServices _localizedResourceServices;

        #endregion

        public string DefaultTemplate
        {
            get { return "Default.SingleBanner"; }
        }

        #region Constructor
        public BannerResolver()
        {
            _bannerServices = HostContainer.GetInstance<IBannerServices>();
            _templateServices = HostContainer.GetInstance<ITemplateServices>();
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #endregion

        #region Parse Params

        private int BannerId { get; set; }

        private string Template { get; set; }

        private void ParseParams(string[] parameters)
        {
            /*
             * Params:
             * * Banner Id
             * * Template
             */

            //Count
            if (parameters.Length > 1)
            {
                BannerId = parameters[1].ToInt();
            }

            //Template
            if (parameters.Length > 2)
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
                    DataType = typeof(BannerCurlyBracket).AssemblyQualifiedName,
                    CurlyBracket = "{Banner}",
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

            var banner = _bannerServices.GetById(BannerId);

            if(banner == null)
            {
                return _localizedResourceServices.T("CurlyBracketsRendering:::SingleBanner:::Messages:::BannerNotFounded:::Banner id is invalid. Please check the data again.");
            }

            var bannerRenderModel = new BannerCurlyBracket(banner);

            var template = _templateServices.GetTemplateByName(Template) ??
                           _templateServices.GetTemplateByName(DefaultTemplate);
            return _templateServices.Parse(template.Content, bannerRenderModel, null, template.CacheName);
        }
    }
}
