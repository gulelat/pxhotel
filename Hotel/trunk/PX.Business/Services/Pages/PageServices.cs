using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using PX.Business.Models.Pages;
using PX.Business.Mvc.Enums;
using PX.Business.Mvc.Environments;
using PX.Business.Services.CurlyBrackets;
using PX.Business.Services.Localizes;
using PX.Business.Services.PageTemplates;
using PX.Core.Configurations.Constants;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;
using PX.EntityModel.Repositories.RepositoryBase.Extensions;
using PX.EntityModel.Repositories.RepositoryBase.Models;
using RazorEngine.Templating;

namespace PX.Business.Services.Pages
{
    public class PageServices : IPageServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly ICurlyBracketServices _curlyBracketServices;
        private readonly IPageTemplateServices _pageTemplateServices;
        public PageServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _curlyBracketServices = HostContainer.GetInstance<ICurlyBracketServices>();
            _pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
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

        #region Manage Page

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
        /// Get page manage model by id
        /// </summary>
        /// <param name="id">the page id</param>
        /// <returns></returns>
        public PageManageModel GetPageManageModel(int? id = null)
        {
            var page = GetById(id);
            if (page != null)
            {
                return new PageManageModel
                    {
                        Id = page.Id,
                        Content = page.Content,
                        Title = page.Title,
                        FriendlyUrl = page.FriendlyUrl,
                        Caption = page.Caption,
                        Status = page.Status,
                        StatusList = GetStatus(),
                        ParentId = page.ParentId,
                        Parents = GetPossibleParents(page.Id),
                        PageTemplateId = page.PageTemplateId,
                        PageTemplates = _pageTemplateServices.GetPageTemplateSelectList(page.PageTemplateId),
                        Position = (int)PageEnums.PositionEnums.After,
                        Positions = EnumUtilities.GetAllItemsFromEnum<PageEnums.PositionEnums>(),
                        RelativePageOrder = page.RecordOrder,
                        RelativePages = GetRelativePages(page.Id, page.ParentId),
                        RecordOrder = page.RecordOrder,
                        RecordActive = page.RecordActive
                    };
            }
            return new PageManageModel
            {
                StatusList = GetStatus(),
                Parents = GetPossibleParents(),
                PageTemplates = _pageTemplateServices.GetPageTemplateSelectList(),
                Positions = EnumUtilities.GetAllItemsFromEnum<PageEnums.PositionEnums>(),
                RelativePages = GetRelativePages(),
            };
        }

        /// <summary>
        /// Save page manage model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SavePageManageModel(PageManageModel model)
        {
            Page relativePage;
            ResponseModel response;
            var page = GetById(model.Id);

            #region Edit Page
            if (page != null)
            {
                page.Title = model.Title;
                page.PageTemplateId = model.PageTemplateId;

                page.Status = model.Status;
                //Set content & caption base on status
                if (model.Status == (int)PageEnums.PageStatusEnums.Draft)
                {
                    page.ContentWorking = model.Content;
                    page.CaptionWorking = model.Caption;
                }
                else
                {
                    page.Content = model.Content;
                    page.Caption = model.Caption;
                }

                //Parse friendly url
                if (string.IsNullOrWhiteSpace(model.FriendlyUrl))
                {
                    page.FriendlyUrl = model.Title.ToUrlString();
                }
                else
                {
                    page.FriendlyUrl = model.FriendlyUrl.ToUrlString();
                }

                //Get page record order
                relativePage = GetById(model.RelativePageOrder);
                if (relativePage != null)
                {
                    if(model.Position == (int)PageEnums.PositionEnums.Before)
                    {
                        page.RecordOrder = relativePage.RecordOrder;
                        var query =
                            string.Format(
                                "Update Pages set RecordOrder = RecordOrder + 1 Where {0} And RecordOrder >= {1}",
                                relativePage.ParentId.HasValue ? string.Format(" ParentId = {0}", relativePage.ParentId) : "ParentId Is NULL", relativePage.RecordOrder);
                        PageRepository.ExcuteSql(query);
                    }
                    else
                    {
                        page.RecordOrder = relativePage.RecordOrder;
                        var query =
                            string.Format(
                                "Update Pages set RecordOrder = RecordOrder - 1 Where {0} And RecordOrder <= {1}",
                                relativePage.ParentId.HasValue ? string.Format(" ParentId = {0}", relativePage.ParentId) : "ParentId Is NULL", relativePage.RecordOrder);
                        PageRepository.ExcuteSql(query);
                    }
                }

                if (page.ParentId != model.ParentId)
                {
                    page.ParentId = model.ParentId;
                    response = HierarchyUpdate(page);
                }
                else
                {
                    response = Update(page);
                }
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::Pages:::Update page successfully")
                    : _localizedResourceServices.T("AdminModule:::Pages:::Update page failure"));
            }
            #endregion

            page = new Page
                {
                    Title = model.Title,
                    Status = model.Status,
                    Content = model.Content,
                    Caption = model.Caption,
                    ParentId = model.ParentId,
                    RecordOrder = 0,
                    PageTemplateId = model.PageTemplateId
                };

            //Parse friendly url
            if (string.IsNullOrWhiteSpace(model.FriendlyUrl))
            {
                page.FriendlyUrl = model.Title.ToUrlString();
            }
            else
            {
                page.FriendlyUrl = model.FriendlyUrl.ToUrlString();
            }

            //Set content & caption base on status
            if (model.Status == (int)PageEnums.PageStatusEnums.Draft)
            {
                page.ContentWorking = model.Content;
                page.CaptionWorking = model.Caption;
            }

            //Get page record order
            relativePage = GetById(model.RelativePageOrder);
            if (relativePage != null)
            {
                page.RecordOrder = relativePage.RecordOrder;
                var query =
                    string.Format(
                        "Update Pages set RecordOrder = RecordOrder + 1 Where ParentId = {0} And RecordOrder >= {1}'",
                        relativePage.ParentId, relativePage.RecordOrder);
                PageRepository.ExcuteSql(query);
            }

            response = HierarchyInsert(page);
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::Pages:::Create page successfully")
                : _localizedResourceServices.T("AdminModule:::Pages:::Create page failure"));
        }

        #endregion

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
        /// Get possible parent menu
        /// </summary>
        /// <param name="id">the current menu id</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPossibleParents(int? id = null)
        {
            var pages = GetAll();
            if (id.HasValue)
            {
                var key = id.Value.GetHierarchyValueForRoot();
                pages = pages.Where(m => !m.Hierarchy.Contains(key));
            }
            var data = pages.Select(p => new HierarchyModel
            {
                Id = p.Id,
                Name = p.Title,
                Hierarchy = p.Hierarchy,
                RecordOrder = p.RecordOrder
            }).ToList();
            return PageRepository.BuildSelectList(data, DefaultConstants.HierarchyLevelPrefix);
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
        /// <param name="pageId"> </param>
        /// <param name="parentId">the parent id</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetRelativePages(int? pageId = null, int? parentId = null)
        {
            var x = GetAll().ToList();
            return GetAll().Where(p => (!pageId.HasValue || p.Id != pageId) && (!parentId.HasValue || p.ParentId == parentId))
                .OrderBy(p => p.RecordOrder)
                .Select(p => new SelectListItem
                    {
                        Text = p.Title,
                        Value = SqlFunctions.StringConvert((double)p.Id).Trim()
                    });
        }

        /// <summary>
        /// Get page by parent id
        /// </summary>
        /// <param name="parentId">the parent id</param>
        /// <returns></returns>
        public List<Page> GetPages(int? parentId = null)
        {
            return GetAll().Where(p => !parentId.HasValue || p.ParentId == parentId).OrderBy(p => p.RecordOrder).ToList();
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
