using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
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
        public IQueryable<Page> Fetch(Expression<Func<Page, bool>> expression)
        {
            return PageRepository.Fetch(expression);
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
            //Get Home Page
            if(string.IsNullOrEmpty(friendlyUrl))
            {
                return GetHomePage();
            }
            return GetAll().FirstOrDefault(p => p.FriendlyUrl.Equals(friendlyUrl, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Get home page
        /// </summary>
        /// <returns></returns>
        public Page GetHomePage()
        {
            return PageRepository.FetchFirst(p => p.IsHomePage);
        }

        #region Search Pages

        /// <summary>
        /// search the Pages.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchPages(JqSearchIn si)
        {
            var pages = GetAll().Select(p => new PageModel
            {
                Id = p.Id,
                PageTemplateId = p.PageTemplateId,
                Title = p.Title,
                PageTemplateName = p.PageTemplate.Name,
                ParentName = p.ParentId.HasValue ? p.Page1.Title : string.Empty,
                FriendlyUrl = p.FriendlyUrl,
                Status = p.Status,
                RecordActive = p.RecordActive,
                RecordOrder = p.RecordOrder,
                Created = p.Created,
                CreatedBy = p.CreatedBy,
                Updated = p.Updated,
                UpdatedBy = p.UpdatedBy,
                IsHomePage = p.IsHomePage
            });

            return si.Search(pages);
        }

        #endregion

        #region Manage Page

        /// <summary>
        /// Change home page
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel ChangeHomePage(int id)
        {
            var response = new ResponseModel();
            var page = GetById(id);
            var homePage = GetHomePage();
            if(page != null)
            {
                if(page.Id != homePage.Id)
                {
                    homePage.IsHomePage = false;
                    page.IsHomePage = true;
                    if(Update(homePage).Success)
                    {
                        response = Update(page);
                        if (response.Success)
                        {
                            response.Message =
                                string.Format(
                                    _localizedResourceServices.T(
                                        "AdminModule:::Pages:::Message:::Change page {0} to home page successfully."),
                                    page.Title);
                        }
                        else
                        {
                            response.Message =
                                string.Format(
                                    _localizedResourceServices.T(
                                        "AdminModule:::Pages:::Message:::Change page {0} to home page failure. Please try again later."),
                                    page.Title);
                        }
                    }
                }
                else
                {
                    response.Success = true;
                    response.Message =
                        string.Format(
                            _localizedResourceServices.T(
                                "AdminModule:::Pages:::Message:::Change page {0} to home page successfully."),
                            page.Title);
                }
            }
            else
            {
                response.Success = false;
                response.Message = _localizedResourceServices.T("AdminModule:::Pages:::Messages:::Page not founded");
            }
            return response;
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
                    page.FriendlyUrl = string.IsNullOrWhiteSpace(model.FriendlyUrl)
                                           ? model.Title.ToUrlString()
                                           : model.FriendlyUrl.ToUrlString();

                    // Convert post data from jqGrid post
                    page.PageTemplateId = model.PageTemplateName.ToNullableInt();
                    page.ParentId = model.ParentName.ToNullableInt();

                    response = HierarchyUpdate(page);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Pages:::Messages:::Update page successfully")
                        : _localizedResourceServices.T("AdminModule:::Pages:::Messages:::Update page failure. Please try again later.. Please try again later."));

                //Redundant Create function
                case GridOperationEnums.Add:
                    page = Mapper.Map<PageModel, Page>(model);
                    page.Status = model.Status;
                    page.Content = string.Empty;
                    page.Caption = string.Empty;

                    // Convert post data from jqGrid post
                    page.PageTemplateId = model.PageTemplateName.ToNullableInt();
                    page.ParentId = model.ParentName.ToNullableInt();

                    response = HierarchyInsert(page);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Pages:::Messages:::Create page successfully")
                        : _localizedResourceServices.T("AdminModule:::Pages:::Messages:::Create page failure. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Pages:::Messages:::Delete page successfully")
                        : _localizedResourceServices.T("AdminModule:::Pages:::Messages:::Delete page failure. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Pages:::Messages:::Page not founded")
            };
        }

        /// <summary>
        /// Get page manage model by id
        /// </summary>
        /// <param name="id">the page id</param>
        /// <returns></returns>
        public PageManageModel GetPageManageModel(int? id = null)
        {
            int position;
            int relativePageId;
            IEnumerable<SelectListItem> relativePages;
            var page = GetById(id);
            if (page != null)
            {
                relativePages = GetRelativePages(out position, out relativePageId, page.Id, page.ParentId);
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
                        Position = position,
                        Positions = EnumUtilities.GetAllItemsFromEnum<PageEnums.PositionEnums>(),
                        RelativePageId = relativePageId,
                        RelativePages = relativePages,
                        RecordOrder = page.RecordOrder,
                        RecordActive = page.RecordActive
                    };
            }
            relativePages = GetRelativePages(out position, out relativePageId);
            return new PageManageModel
            {
                StatusList = GetStatus(),
                Parents = GetPossibleParents(),
                PageTemplates = _pageTemplateServices.GetPageTemplateSelectList(),
                Positions = EnumUtilities.GetAllItemsFromEnum<PageEnums.PositionEnums>(),
                Position = position,
                RelativePageId = relativePageId,
                RelativePages = relativePages,
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
                page.FriendlyUrl = string.IsNullOrWhiteSpace(model.FriendlyUrl)
                                       ? model.Title.ToUrlString()
                                       : model.FriendlyUrl.ToUrlString();

                //Get page record order
                relativePage = GetById(model.RelativePageId);
                if (relativePage != null)
                {
                    if (model.Position == (int)PageEnums.PositionEnums.Before)
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
                        page.RecordOrder = relativePage.RecordOrder + 1;
                        var query =
                            string.Format(
                                "Update Pages set RecordOrder = RecordOrder + 1 Where {0} And RecordOrder > {1}",
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
                    : _localizedResourceServices.T("AdminModule:::Pages:::Update page failure. Please try again later."));
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
                    PageTemplateId = model.PageTemplateId,
                    FriendlyUrl = string.IsNullOrWhiteSpace(model.FriendlyUrl)
                                      ? model.Title.ToUrlString()
                                      : model.FriendlyUrl.ToUrlString()
                };

            //Set content & caption base on status
            if (model.Status == (int)PageEnums.PageStatusEnums.Draft)
            {
                page.ContentWorking = model.Content;
                page.CaptionWorking = model.Caption;
            }

            //Get page record order
            relativePage = GetById(model.RelativePageId);
            if (relativePage != null)
            {
                if (model.Position == (int)PageEnums.PositionEnums.Before)
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
                    page.RecordOrder = relativePage.RecordOrder + 1;
                    var query =
                        string.Format(
                            "Update Pages set RecordOrder = RecordOrder + 1 Where {0} And RecordOrder > {1}",
                            relativePage.ParentId.HasValue ? string.Format(" ParentId = {0}", relativePage.ParentId) : "ParentId Is NULL", relativePage.RecordOrder);
                    PageRepository.ExcuteSql(query);
                }
            }

            response = HierarchyInsert(page);
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::Pages:::Messages:::Create page successfully")
                : _localizedResourceServices.T("AdminModule:::Pages:::Messages:::Create page failure. Please try again later."));
        }

        #endregion

        /// <summary>
        /// Get possible parent menu
        /// </summary>
        /// <param name="id">the current menu id</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPossibleParents(int? id = null)
        {
            var pages = GetAll();
            int? parentId = null;
            var page = GetById(id);
            if (page != null)
            {
                parentId = page.ParentId;
                pages = PageRepository.GetPossibleParents(page);
            }
            var data = pages.Select(m => new HierarchyModel
            {
                Id = m.Id,
                Name = m.Title,
                Hierarchy = m.Hierarchy,
                RecordOrder = m.RecordOrder,
                Selected = parentId.HasValue && parentId.Value == m.Id
            }).ToList();
            return PageRepository.BuildSelectList(data);
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
        /// <param name="position"> </param>
        /// <param name="relativePageId"> </param>
        /// <param name="pageId"> </param>
        /// <param name="parentId">the parent id</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetRelativePages(out int position, out int relativePageId, int? pageId = null, int? parentId = null)
        {
            position = (int)PageEnums.PositionEnums.Before;
            relativePageId = 0;
            var order = 0;
            var relativePages = Fetch(p => (!pageId.HasValue || p.Id != pageId) && (parentId.HasValue ? p.ParentId == parentId : p.ParentId == null))
                .OrderBy(p => p.RecordOrder).Select(p => new
                    {
                        p.Title,
                        p.Id,
                        p.RecordOrder
                    }).ToList();
            var page = GetById(pageId);
            if (page != null)
            {
                order = page.RecordOrder;
            }

            //Get position for relative page list

            //Flag to check if current page is the bigest order in relative page list
            var flag = false;
            for (var i = 0; i < relativePages.Count(); i++)
            {
                if (relativePages[i].RecordOrder > order)
                {
                    relativePageId = relativePages[i].Id;
                    flag = true;
                    break;
                }
            }
            if (!flag && relativePages.Any())
            {
                position = (int)PageEnums.PositionEnums.After;
                relativePageId = relativePages.Last().Id;
            }
            var selectPageId = relativePageId;
            return relativePages.Select(p => new SelectListItem
                  {
                      Text = p.Title,
                      Value = p.Id.ToString(CultureInfo.InvariantCulture),
                      Selected = p.Id == selectPageId
                  });
        }

        /// <summary>
        /// Get page by parent id
        /// </summary>
        /// <param name="pageId"> </param>
        /// <param name="parentId">the parent id</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetRelativePages(int? pageId = null, int? parentId = null)
        {
            return Fetch(p => (!pageId.HasValue || p.Id != pageId) && (parentId.HasValue ? p.ParentId == parentId : p.ParentId == null))
                .OrderBy(p => p.RecordOrder).Select(p => new SelectListItem
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
            return Fetch(p => parentId.HasValue ? p.ParentId == parentId : !p.ParentId.HasValue).OrderBy(p => p.RecordOrder).ToList();
        }

        /// <summary>
        /// Render page content by friendly url
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Check if title is existed
        /// </summary>
        /// <param name="pageId">the page id</param>
        /// <param name="title">the page title</param>
        /// <returns></returns>
        public bool IsTitleExisted(int? pageId, string title)
        {
            return Fetch(u => u.Title.Equals(title) && u.Id != pageId).Any();
        }

        /// <summary>
        /// Check if friendly url is existed
        /// </summary>
        /// <param name="pageId">the page id</param>
        /// <param name="friendlyUrl">the friendly url</param>
        /// <returns></returns>
        public bool IsFriendlyUrlExisted(int? pageId, string friendlyUrl)
        {
            return Fetch(u => u.FriendlyUrl.Equals(friendlyUrl) && u.Id != pageId).Any();
        }
    }
}
