using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PX.Business.Models.Pages;
using PX.Business.Mvc.Environments;
using PX.Business.Services.CurlyBrackets;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Localizes;
using PX.Core.Configurations.Constants;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;
using RazorEngine.Templating;

namespace PX.Business.Services.Pages
{
    public class PageServices : IPageServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly ICurlyBracketServices _curlyBracketServices;
        public PageServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _curlyBracketServices = HostContainer.GetInstance<ICurlyBracketServices>();
        }

        #region Base
        public IQueryable<Page> GetAll()
        {
            return PageRepository.GetAll();
        }
        public Page GetById(object id)
        {
            return PageRepository.GetById(id);
        }
        public ResponseModel Insert(Page page)
        {
            return PageRepository.Insert(page);
        }
        public ResponseModel Update(Page page)
        {
            return PageRepository.Update(page);
        }
        public ResponseModel HierarchyUpdate(Page page)
        {
            return PageRepository.HierarchyUpdate(page);
        }
        public ResponseModel HierarchyInsert(Page page)
        {
            return PageRepository.HierarchyInsert(page);
        }
        public ResponseModel Delete(Page page)
        {
            return PageRepository.Delete(page);
        }
        public ResponseModel Delete(object id)
        {
            return PageRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return PageRepository.InactiveRecord(id);
        }
        #endregion

        /// <summary>
        /// Get page by friendly url
        /// </summary>
        /// <param name="friendlyUrl"></param>
        /// <returns></returns>
        public Page GetPage(string friendlyUrl)
        {
            return GetAll().FirstOrDefault(p => p.FriendlyUrl.Equals(friendlyUrl, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// search the Pages.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchPages(JqSearchIn si)
        {
            var pages = GetAll().Select(u => new PageModel
            {
                Id = u.Id,
                PageTemplateId = u.PageTemplateId,
                Title = u.Title,
                PageTemplateName = u.PageTemplate.Name,
                FriendlyUrl = u.FriendlyUrl,
                Status = u.Status,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(pages);
        }

        /// <summary>
        /// Manage Site Setting
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the setting model</param>
        /// <returns></returns>
        public ResponseModel ManagePage(GridOperationEnums operation, PageModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<PageModel, Page>();
            Page page;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    page = GetById(model.Id);
                    page.Title = model.Title;
                    page.Status = model.Status;
                    page.FriendlyUrl = model.FriendlyUrl;
                    page.RecordOrder = model.RecordOrder;

                    // Convert post data from jqGrid post
                    page.PageTemplateId = model.PageTemplateName.ToNullableInt();
                    page.ParentId = model.ParentName.ToNullableInt();

                    response = HierarchyUpdate(page);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Pages:::Update page successfully")
                        : _localizedResourceServices.T("AdminModule:::Pages:::Update page failure"));

                case GridOperationEnums.Add:
                    page = Mapper.Map<PageModel, Page>(model);
                    page.Content = string.Empty;
                    page.Caption = string.Empty;
                    page.Hierarchy = string.Empty;

                    // Convert post data from jqGrid post
                    page.PageTemplateId = model.PageTemplateName.ToNullableInt();
                    page.ParentId = model.ParentName.ToNullableInt();

                    response = HierarchyInsert(page);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Pages:::Insert page successfully")
                        : _localizedResourceServices.T("AdminModule:::Pages:::Insert page failure"));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Pages:::Delete page successfully")
                        : _localizedResourceServices.T("AdminModule:::Pages:::Delete page failure"));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Pages:::Page not founded")
            };
        }

        /// <summary>
        /// Get possible parent menu
        /// </summary>
        /// <param name="id">the current menu id</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPossibleParents(int? id)
        {
            var pages = GetAll();
            if (id.HasValue)
            {
                var key = string.Format(".{0}.", id.Value.ToString("D5"));
                pages = pages.Where(m => !m.Hierarchy.Contains(key));
            }
            return PageRepository.BuildSelectList(pages.ToList(), "--", "Title");
        }

        /// <summary>
        /// Get status of page
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetStatus()
        {
            return EnumUtilities.GetAllItemsFromEnum<PageEnums.PageStatusEnums>();
        }

        /// <summary>
        /// Get page by parent id
        /// </summary>
        /// <param name="parentId">the parent id</param>
        /// <returns></returns>
        public List<Page> GetPages(int? parentId = null)
        {
            return GetAll().Where(m => parentId.HasValue ? m.ParentId == parentId : !m.ParentId.HasValue).OrderBy(m => m.RecordOrder).ToList();
        }

        public PageRenderModel RenderContent(string url)
        {
            var page = GetPage(url);
            if (page != null)
            {
                var template = DefaultConstants.CurlyBracketRenderBody;
                if (page.PageTemplateId.HasValue)
                {
                    var pageTemplates =
                        PageTemplateRepository.GetAll().Where(t => page.PageTemplate.Hierarchy.Contains(t.Hierarchy))
                        .OrderBy(t => t.Hierarchy)
                        .Select(t => new
                            {
                                t.Content,
                                t.Name
                            });
                    if (pageTemplates.Any())
                    {
                        var cacheTemplateName = string.Empty;
                        foreach (var pageTemplate in pageTemplates)
                        {
                            template = template.Replace(DefaultConstants.CurlyBracketRenderBody, pageTemplate.Content);
                            cacheTemplateName = pageTemplate.Name;
                        }

                        var templateService = new TemplateService();
                        var model = new PageRenderModel
                            {
                                Title = page.Title,
                                Content = page.Content
                            };
                        template = templateService.Parse(template, model, null, cacheTemplateName);
                    }
                }

                template = template.Replace(DefaultConstants.CurlyBracketRenderBody, page.Content);

                return new PageRenderModel
                    {
                        Content = _curlyBracketServices.Render(template)
                    };
            }
            return null;
        }
    }
}
