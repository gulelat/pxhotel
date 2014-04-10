using System;
using System.Collections.Generic;
using System.Linq;
using PX.Business.Models.Pages.CurlyBrackets;
using PX.Business.Models.Templates;
using PX.Business.Models.Testimonials.CurlyBrackets;
using PX.Business.Mvc.Attributes;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Templates;
using PX.Business.Services.Testimonials;
using PX.Core.Configurations.Constants;
using PX.EntityModel;
using RazorEngine.Templating;

namespace PX.Business.Services.Pages.CurlyBracketResolvers
{

    [CurlyBracket(Name = "Dynamic Menu", CurlyBracket = "DynamicMenu", Descrition = "Dynamic menu curly bracket", Type = typeof(List<DynamicMenuCurlyBracket>))]
    public class DynamicMenuResolver : ICurlyBracketResolver
    {
        #region Private Properties
        private readonly ITemplateServices _templateServices;
        private readonly IPageServices _pageServices;
        public const string ChildTemplate = "Default.DynamicMenus.Children";
        public string DefaultTemplate()
        {
            return "Default.DynamicMenus";
        }
        #endregion

        #region Constructors
        public DynamicMenuResolver()
        {
            _pageServices = HostContainer.GetInstance<IPageServices>();
            _templateServices = HostContainer.GetInstance<ITemplateServices>();
        }

        #endregion

        public void Initialize()
        {
            if (_templateServices.GetTemplateByName(DefaultTemplate()) == null)
            {
                var template = new Template
                {
                    Name = DefaultTemplate(),
                    DataType = typeof(TestimonialCurlyBracket).FullName,
                    Content = "{Model.Author}",
                    RecordActive = true,
                    RecordOrder = 0,
                    Created = DateTime.Now,
                    CreatedBy = DefaultConstants.DefaultSystemAccount
                };
                _templateServices.Insert(template);
            }
        }

        public string Render(string[] parameters)
        {
            var pages = _pageServices.GetAll().ToList();
            var data = GetTree(pages, null);
            var template = _templateServices.GetTemplateByName(DefaultTemplate());
            return RenderMenus(data, template);
        }

        public string RenderMenus(List<DynamicMenuCurlyBracket> menus,TemplateManageModel template)
        {
            var childTemplate = _templateServices.GetTemplateByName(ChildTemplate);
            foreach (var menu in menus)
            {
                if(menu.ChildMenus.Any())
                    menu.ChildMenusString = RenderMenus(menu.ChildMenus, childTemplate);
            }
            return _templateServices.RenderTemplate(template.Content, menus, template.Name);
        }

        public List<DynamicMenuCurlyBracket> GetTree(List<Page> list, int? parent)
        {
            return list.Where(p => parent.HasValue ? p.ParentId == parent.Value : !p.ParentId.HasValue).Select(p => new DynamicMenuCurlyBracket
            {
                Id = p.Id,
                Title = p.Title,
                Order = p.RecordOrder,
                Url = p.FriendlyUrl,
                ChildMenus = GetTree(list, p.Id)
            }).ToList();
        }
    }
}
