using PX.Business.Models.Pages;
using PX.Business.Models.RotatingImageGroups;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Localizes;
using PX.Business.Services.Templates;
using PX.Core.Framework.Mvc.Environments;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Services.RotatingImageGroups.CurlyBracketResolvers
{
    [CurlyBracket(Name = "Rotating Images", CurlyBracket = "RotatingImages", Descrition = "Load rotating images", Type = typeof(GroupGalleryModel))]
    public class RotatingImageResolver : ICurlyBracketResolver
    {
        #region Private Properties
        private readonly ITemplateServices _templateServices;
        private readonly IRotatingImageGroupServices _rotatingImageGroupServices;
        private readonly ILocalizedResourceServices _localizedResourceServices;

        #endregion

        public string DefaultTemplate
        {
            get { return "Default.RotatingImagesTemplate"; }
        }

        #region Constructor
        public RotatingImageResolver()
        {
            _rotatingImageGroupServices = HostContainer.GetInstance<IRotatingImageGroupServices>();
            _templateServices = HostContainer.GetInstance<ITemplateServices>();
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();

        }

        #endregion

        #region Parse Params

        private int GroupId { get; set; }

        private string Template { get; set; }

        private void ParseParams(string[] parameters)
        {
            /*
             * Params:
             * * Group Id
             * * Template
             */

            //Count
            if (parameters.Length > 1)
            {
                GroupId = parameters[1].ToInt(0);
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
                    DataType = typeof(GroupGalleryModel).AssemblyQualifiedName,
                    CurlyBracket = "{RotatingImages}",
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

            var model = _rotatingImageGroupServices.GetGroupGallery(GroupId);

            if (model == null)
            {
                return _localizedResourceServices.T("CurlyBracketsRendering:::RotatingImages:::Messages:::GroupNotFounded:::Group id is invalid. Please check the data again.");
            }

            var template = _templateServices.GetTemplateByName(Template) ??
                           _templateServices.GetTemplateByName(DefaultTemplate);
            return _templateServices.Parse(template.Content, model, null, template.CacheName);
        }
    }
}
