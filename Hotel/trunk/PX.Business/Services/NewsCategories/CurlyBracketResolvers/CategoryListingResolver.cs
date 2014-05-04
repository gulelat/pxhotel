using PX.Business.Models.NewsCategories.CurlyBrackets;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Templates;
using PX.Core.Framework.Mvc.Environments;
using PX.EntityModel;

namespace PX.Business.Services.NewsCategories.CurlyBracketResolvers
{
    [CurlyBracket(Name = "Category Listing", CurlyBracket = "CategoryListing", Descrition = "Category listing curly bracket", Type = typeof(CategoriesModel))]
    public class CategoryListingResolver : ICurlyBracketResolver
    {
        #region Private Properties
        private readonly ITemplateServices _templateServices;
        private readonly INewsCategoryServices _newsCategoryServices;
        #endregion

        #region Public Properties

        public string DefaultTemplate
        {
            get { return "Default.CategoryListing"; }
        }
        #endregion

        #region Constructors
        public CategoryListingResolver()
        {
            _newsCategoryServices = HostContainer.GetInstance<INewsCategoryServices>();
            _templateServices = HostContainer.GetInstance<ITemplateServices>();
        }

        #endregion

        #region Parse Params

        private string Template { get; set; }

        private void ParseParams(string[] parameters)
        {
            /*
             * Params:
             * * Template
             */
            Template = DefaultTemplate;

            //Template
            if (parameters.Length > 1)
            {
                Template = parameters[1];
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
                        DataType = typeof(CategoriesModel).AssemblyQualifiedName,
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

            var model = _newsCategoryServices.GetCategoryListing();
            return _templateServices.Parse(templateManageModel.Content, model, null, templateManageModel.CacheName);
        }
    }
}
