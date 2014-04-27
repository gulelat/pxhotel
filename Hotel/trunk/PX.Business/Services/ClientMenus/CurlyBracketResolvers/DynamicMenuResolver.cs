using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PX.Business.Models.ClientMenus;
using PX.Business.Models.ClientMenus.CurlyBrackets;
using PX.Business.Models.Templates;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.WorkContext;
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
        private TemplateRenderModel _childMobileTemplate;
        private TemplateRenderModel _childTemplate;
        private TemplateRenderModel _parentTemplate;
        private const string DefaultChildTemplate = "Default.DynamicMenus.Children";
        private const string DefaultChildMobileTemplate = "Default.DynamicMenus.Mobile";
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

        private string ChildMobileTemplateName { get; set; }

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

            ParentTemplateName = DefaultTemplate;
            ChildTemplateName = DefaultChildTemplate;
            ChildMobileTemplateName = DefaultChildMobileTemplate;

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

            //ChildTemplateName
            if (parameters.Length > 5 && !string.IsNullOrEmpty(parameters[5]))
            {
                ChildMobileTemplateName = parameters[5];
            }

            _parentTemplate = _templateServices.GetTemplateByName(ParentTemplateName);
            if (_parentTemplate == null)
            {
                throw new Exception(string.Format("Template {0} is not founded.", ParentTemplateName));
            }
            _childTemplate = _templateServices.GetTemplateByName(ChildTemplateName);
            if (_childTemplate == null)
            {
                throw new Exception(string.Format("Template {0} is not founded.", ChildTemplateName));
            }
            _childMobileTemplate = _templateServices.GetTemplateByName(ChildMobileTemplateName);
            if (_childMobileTemplate == null)
            {
                throw new Exception(string.Format("Template {0} is not founded.", ChildMobileTemplateName));
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
                    DataType = typeof(List<DynamicMenuCurlyBracket>).AssemblyQualifiedName,
                    Content = string.Empty,
                    IsDefaultTemplate = true
                };
                _templateServices.Insert(template);
            }

            if (_templateServices.GetTemplateByName(DefaultChildTemplate) == null)
            {
                var template = new Template
                {
                    Name = DefaultChildTemplate,
                    DataType = typeof(List<DynamicMenuCurlyBracket>).AssemblyQualifiedName,
                    Content = string.Empty,
                    IsDefaultTemplate = true
                };
                _templateServices.Insert(template);
            }

            if (_templateServices.GetTemplateByName(DefaultChildMobileTemplate) == null)
            {
                var template = new Template
                {
                    Name = DefaultChildMobileTemplate,
                    DataType = typeof(List<DynamicMenuCurlyBracket>).AssemblyQualifiedName,
                    Content = string.Empty,
                    IsDefaultTemplate = true
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

            var templateList = new List<string>
            {
                ParentTemplateName,
                ChildMobileTemplateName,
                ChildTemplateName
            };

            /*
             * Check storing menu result in cache
             * If all the menus are not updated then get the cache result
             * If not rebuild the data and recache
             */
            CacheMenu cacheMenu;
            var cacheName = string.Join("_", parameters);

            var lastCreated = _clientMenuServices.GetAll().Max(t => t.Created);
            var lastUpdated = _clientMenuServices.GetAll().Max(t => t.Updated) ?? DateTime.MinValue;
            var dataCacheTime = lastCreated > lastUpdated ? lastCreated : lastUpdated;

            lastCreated = _templateServices.Fetch(t => templateList.Contains(t.Name)).Max(t => t.Created);
            lastUpdated = _templateServices.Fetch(t => templateList.Contains(t.Name)).Max(t => t.Updated) ?? DateTime.MinValue;
            var templateCacheTime = lastCreated > lastUpdated ? lastCreated : lastUpdated;
            if (HttpContext.Current.Application[cacheName] != null)
            {
                cacheMenu = (CacheMenu)HttpContext.Current.Application[cacheName];
                if (cacheMenu.DataCacheTime == dataCacheTime && cacheMenu.TemplateCacheTime == templateCacheTime)
                {
                    return cacheMenu.Content;
                }
            }

            /*
             * Get tree data
             * Recursive to get all the html of dynamic menu
             */
            var items = _clientMenuServices.GetAll()
                .Where(m => m.IncludeInSiteNavigation 
                    && (!m.StartPublishingDate.HasValue || DateTime.Now > m.StartPublishingDate)
                    && (!m.EndPublishingDate.HasValue || DateTime.Now < m.StartPublishingDate))
                .Select(m => new ClientMenuModel
                {
                    Id = m.Id,
                    PageId = m.PageId,
                    Name = m.Name,
                    RecordOrder = m.RecordOrder,
                    Url = m.Url,
                    ParentId = m.ParentId,
                }).ToList();
            var data = GetTree(items, ParentId, 0);
            foreach (var dynamicMenu in data)
            {
                dynamicMenu.ChildMenusString = RenderMenus(dynamicMenu.ChildMenus, _childTemplate);
                dynamicMenu.ChildMenusMobileString = RenderMobileMenus(dynamicMenu.ChildMenus, _childMobileTemplate);
            }

            /*
             * Create cache version of this curly bracket and store in Application
             */
            cacheMenu = new CacheMenu
            {
                Content = _templateServices.Parse(_parentTemplate.Content, data, null, _parentTemplate.CacheName),
                DataCacheTime = dataCacheTime,
                TemplateCacheTime = templateCacheTime
            };
            HttpContext.Current.Application[cacheName] = cacheMenu;

            return cacheMenu.Content;
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
                    dynamicMenuCurlyBracket.ChildMenusMobileString = RenderMobileMenus(dynamicMenuCurlyBracket.ChildMenus, _childTemplate);
                }
                return _templateServices.Parse(template.Content, menus, null, template.CacheName);
            }
            return string.Empty;
        }

        private string RenderMobileMenus(List<DynamicMenuCurlyBracket> menus, TemplateRenderModel template)
        {
            if (menus.Any())
            {
                foreach (var dynamicMenuCurlyBracket in menus)
                {
                    //Recursive render child html
                    dynamicMenuCurlyBracket.ChildMenusString = RenderMobileMenus(dynamicMenuCurlyBracket.ChildMenus, _childMobileTemplate);
                }
                return _templateServices.Parse(template.Content, menus, null, template.CacheName);
            }
            return string.Empty;
        }

        /// <summary>
        /// Build menu tree
        /// </summary>
        /// <param name="list"></param>
        /// <param name="parent"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        private List<DynamicMenuCurlyBracket> GetTree(List<ClientMenuModel> list, int? parent, int level)
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
                            PageId = p.PageId,
                            Name = p.Name,
                            Order = p.RecordOrder,
                            Level = level + 1,
                            Url = p.Url,
                            ChildMenus = p.Id == parent
                                             ? new List<DynamicMenuCurlyBracket>()
                                             : GetTree(list, p.Id, level + 1)
                        }).ToList();
        }

        #endregion
    }

    public class CacheMenu
    {
        public DateTime TemplateCacheTime { get; set; }

        public DateTime DataCacheTime { get; set; }

        public string Content { get; set; }
    }
}
