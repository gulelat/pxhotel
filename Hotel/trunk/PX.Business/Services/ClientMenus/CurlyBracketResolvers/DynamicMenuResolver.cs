using System;
using System.Collections.Generic;
using System.Linq;
using PX.Business.Models.ClientMenus.CurlyBrackets;
using PX.Business.Models.Templates;
using PX.Business.Models.Testimonials.CurlyBrackets;
using PX.Business.Mvc.Attributes;
using PX.Core.Configurations;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Templates;
using PX.Core.Ultilities;
using PX.EntityModel;

namespace PX.Business.Services.ClientMenus.CurlyBracketResolvers
{
    [CurlyBracket(Name = "Dynamic Menu", CurlyBracket = "DynamicMenu", Descrition = "Dynamic menu curly bracket", Type = typeof(List<DynamicMenuCurlyBracket>))]
    public class DynamicMenuResolver : ICurlyBracketResolver
    {
        #region Private Properties
        private readonly ITemplateServices _templateServices;
        private readonly IClientMenuServices _clientMenuServices;
        private TemplateRenderModel _childTemplate;
        private TemplateRenderModel _parentTemplate;
        private const string DefaultChildTemplate = "Default.DynamicMenus.Children";
        #endregion

        #region Public Properties


        public string DefaultTemplate
        {
            get
            {
                return "Default.DynamicMenus";
            }
        }
        #endregion

        #region Constructors
        public DynamicMenuResolver()
        {
            _clientMenuServices = HostContainer.GetInstance<IClientMenuServices>();
            _templateServices = HostContainer.GetInstance<ITemplateServices>();
            ShowParentMenu = false;
        }

        #endregion

        #region Parse Params

        private int? ParentId { get; set; }

        private bool ShowParentMenu { get; set; }

        private string ParentTemplateName { get; set; }

        private string ChildTemplateName { get; set; }

        /// <summary>
        /// Parse params from parameters
        /// </summary>
        /// <param name="parameters"></param>
        private void ParseParams(string[] parameters)
        {
            /*
             * Params:
             * * Parent Id
             * * Show Parent
             * * Parent Template
             * * Children Template
             */

            //ParentId
            if (parameters.Length > 1 && !string.IsNullOrEmpty(parameters[1]))
            {
                ParentId = parameters[1].ToNullableInt();
            }

            //ShowParentMenu
            if (parameters.Length > 2 && !string.IsNullOrEmpty(parameters[2]) && ParentId.HasValue)
            {
                ShowParentMenu = parameters[1].ToBool(false);
            }

            //ParentTemplateName
            if (parameters.Length > 3 && !string.IsNullOrEmpty(parameters[3]))
            {
                ParentTemplateName = parameters[3];
            }

            //ChildTemplateName
            if (parameters.Length > 4 && !string.IsNullOrEmpty(parameters[4]))
            {
                ChildTemplateName = parameters[4];
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
                    DataType = typeof(TestimonialCurlyBracket).FullName,
                    Content = string.Empty,
                    IsDefaultTemplate = true
                };
                _templateServices.Insert(template);
            }

            if (_templateServices.GetTemplateByName(DefaultChildTemplate) == null)
            {
                var template = new Template
                {
                    Name = DefaultTemplate,
                    DataType = typeof(TestimonialCurlyBracket).FullName,
                    Content = string.Empty,
                    RecordActive = true,
                    RecordOrder = 0
                };
                _templateServices.Insert(template);
            }
        }

        /// <summary>
        /// Render dynamic menu
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string Render(string[] parameters)
        {
            //Initialize parameter
            ParseParams(parameters);

            _parentTemplate = _templateServices.GetTemplateByName(ParentTemplateName) ?? _templateServices.GetTemplateByName(DefaultTemplate);
            _childTemplate = _templateServices.GetTemplateByName(ChildTemplateName) ?? _templateServices.GetTemplateByName(DefaultChildTemplate);

            var items = _clientMenuServices.GetAll().ToList();
            var data = GetTree(items, ParentId);

            foreach (var dynamicMenu in data)
            {
                dynamicMenu.ChildMenusString = RenderMenus(dynamicMenu.ChildMenus, _childTemplate);
            }

            return _templateServices.RenderTemplate(_parentTemplate.Content, data, _parentTemplate.CacheName);
        }

        #region Private Methods
        /// <summary>
        /// Render child menu html
        /// </summary>
        /// <param name="menus"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        private string RenderMenus(List<DynamicMenuCurlyBracket> menus, TemplateRenderModel template)
        {
            if (menus.Any())
            {
                foreach (var dynamicMenuCurlyBracket in menus)
                {
                    //Recursive render child html
                    dynamicMenuCurlyBracket.ChildMenusString = RenderMenus(dynamicMenuCurlyBracket.ChildMenus, _childTemplate);
                }
                return _templateServices.RenderTemplate(template.Content, menus, template.CacheName);
            }
            return string.Empty;
        }

        /// <summary>
        /// Build menu tree
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        private List<DynamicMenuCurlyBracket> GetTree(List<ClientMenu> list, int? parent)
        {
            return
                list.Where(
                    p =>
                    parent.HasValue
                        ? (p.ParentId == parent.Value || (ShowParentMenu && p.Id == parent))
                        : !p.ParentId.HasValue)
                    .OrderBy(p => p.RecordOrder)
                    .Select(p => new DynamicMenuCurlyBracket
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Order = p.RecordOrder,
                            Url = p.Url,
                            ChildMenus = p.Id == parent
                                             ? new List<DynamicMenuCurlyBracket>()
                                             : GetTree(list, p.Id)
                        }).ToList();
        }

        #endregion
    }
}
