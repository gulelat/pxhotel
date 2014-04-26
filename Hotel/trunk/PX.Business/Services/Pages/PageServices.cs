using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using PX.Business.Models.PageLogs;
using PX.Business.Models.Pages;
using PX.Business.Mvc.WorkContext;
using PX.Business.Services.ClientMenus;
using PX.Business.Services.PageLogs;
using PX.Business.Services.PageTemplates;
using PX.Business.Services.Settings;
using PX.Core.Configurations;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.CurlyBrackets;
using PX.Business.Services.Localizes;
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
        private readonly IPageTemplateServices _pageTemplateServices;
        private readonly IPageLogServices _pageLogServices;
        private readonly ICurlyBracketServices _curlyBracketServices;
        private readonly IClientMenuServices _clientMenuServices;
        private readonly ISettingServices _settingServices;
        private readonly PageRepository _pageRepository;
        public PageServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
            _curlyBracketServices = HostContainer.GetInstance<ICurlyBracketServices>();
            _clientMenuServices = HostContainer.GetInstance<IClientMenuServices>();
            _pageLogServices = HostContainer.GetInstance<IPageLogServices>();
            _settingServices = HostContainer.GetInstance<ISettingServices>();
            _pageRepository = new PageRepository();
        }

        #region Base
        public IQueryable<Page> GetAll()
        {
            return _pageRepository.GetAll();
        }
        public IQueryable<Page> Fetch(Expression<Func<Page, bool>> expression)
        {
            return _pageRepository.Fetch(expression);
        }
        public Page GetById(object id)
        {
            return _pageRepository.GetById(id);
        }
        public ResponseModel Insert(Page page)
        {
            return _pageRepository.Insert(page);
        }
        public ResponseModel Update(Page page)
        {
            return _pageRepository.Update(page);
        }
        public ResponseModel HierarchyUpdate(Page page)
        {
            return _pageRepository.HierarchyUpdate(page);
        }
        public ResponseModel HierarchyInsert(Page page)
        {
            return _pageRepository.HierarchyInsert(page);
        }
        public ResponseModel Delete(Page page)
        {
            return _pageRepository.Delete(page);
        }
        public ResponseModel Delete(object id)
        {
            return _pageRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _pageRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search

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

        #region Grid Manage

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
            if (page != null)
            {
                if (page.Id != homePage.Id)
                {
                    homePage.IsHomePage = false;
                    page.IsHomePage = true;
                    if (Update(homePage).Success)
                    {
                        response = Update(page);
                        response.Message = string.Format(response.Success ?
                            _localizedResourceServices.T("AdminModule:::Pages:::Messages:::ChangeHomePageSuccessfully:::Change page {0} to home page successfully.")
                            : _localizedResourceServices.T("AdminModule:::Pages:::Messages:::ChangeHomePageFailure:::Change page {0} to home page failed. Please try again later.")
                            , page.Title);
                    }
                }
                else
                {
                    response.Success = true;
                    response.Message =
                        string.Format(
                            _localizedResourceServices.T("AdminModule:::Pages:::Messages:::ChangeHomePageSuccessfully:::Change page {0} to home page successfully."),
                            page.Title);
                }
            }
            else
            {
                response.Success = false;
                response.Message = _localizedResourceServices.T("AdminModule:::Pages:::Messages:::ObjectNotFounded:::Page is not founded.");
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
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    var page = GetById(model.Id);
                    page.Title = model.Title;
                    page.Status = model.Status;
                    page.FriendlyUrl = string.IsNullOrWhiteSpace(model.FriendlyUrl)
                                           ? model.Title.ToUrlString()
                                           : model.FriendlyUrl.ToUrlString();
                    page.ParentId = model.ParentName.ToNullableInt();

                    response = HierarchyUpdate(page);

                    _clientMenuServices.SavePageToClientMenu(page.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Pages:::Messages:::UpdateSuccessfully:::Update page successfully.")
                        : _localizedResourceServices.T("AdminModule:::Pages:::Messages:::UpdateFailure:::Update page failed. Please try again later.. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Pages:::Messages:::DeleteSuccessfully:::Delete page successfully.")
                        : _localizedResourceServices.T("AdminModule:::Pages:::Messages:::DeleteFailure:::Delete page failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Pages:::Messages:::ObjectNotFounded:::Page is not founded.")
            };
        }
        #endregion

        #region Manage

        /// <summary>
        /// Get page manage model by id
        /// </summary>
        /// <param name="id">the page id</param>
        /// <returns></returns>
        public PageManageModel GetPageManageModel(int? id = null)
        {
            var page = GetById(id);
            return page != null ? new PageManageModel(page) : new PageManageModel();
        }

        /// <summary>
        /// Get page manage model by id
        /// </summary>
        /// <param name="id">the page id</param>
        /// <returns></returns>
        public PageManageModel GetPageManageModelByLogId(int? id = null)
        {
            var pageLogRepository = new PageLogRepository();
            var log = pageLogRepository.GetById(id);
            return log != null ? new PageManageModel(log) : new PageManageModel();
        }

        /// <summary>
        /// Save page manage model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SavePageManageModel(PageManageModel model)
        {
            var pageTagRepository = new PageTagRepository();
            Page relativePage;
            ResponseModel response;
            var page = GetById(model.Id);

            #region Edit Page
            if (page != null)
            {
                var pageLog = new PageLogManageModel(page);
                page.Title = model.Title;

                page.PageTemplateId = model.PageTemplateId;
                page.FileTemplateId = model.FileTemplateId;

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

                var currentTags = page.PageTags.Select(t => t.TagId).ToList();
                foreach (var id in currentTags.Where(id => !model.Tags.Contains(id)))
                {
                    pageTagRepository.Delete(page.Id, id);
                }
                if (model.Tags != null && model.Tags.Any())
                {
                    foreach (var tagId in model.Tags)
                    {
                        if (currentTags.All(n => n != tagId))
                        {
                            var pageTag = new PageTag
                            {
                                PageId = page.Id,
                                TagId = tagId
                            };
                            pageTagRepository.Insert(pageTag);
                        }
                    }
                }

                page.StartPublishingDate = model.StartPublishingDate;
                page.EndPublishingDate = model.EndPublishingDate;

                //Parse friendly url
                page.FriendlyUrl = string.IsNullOrWhiteSpace(model.FriendlyUrl)
                                       ? model.Title.ToUrlString()
                                       : model.FriendlyUrl.ToUrlString();

                //Get page record order
                relativePage = GetById(model.RelativePageId);
                if (relativePage != null)
                {
                    /*
                     * If position is not changed, donot need to update order of relative pages
                     * If position is changed, check if position is before or after and update the record other of all relative pages
                     */
                    var relativePages = Fetch(p => p.Id != page.Id && relativePage.ParentId.HasValue ? p.ParentId == relativePage.ParentId : p.ParentId == null)
                        .OrderBy(p => p.RecordOrder);
                    if (model.Position == (int)PageEnums.PositionEnums.Before)
                    {
                        if (page.RecordOrder > relativePage.RecordOrder || relativePages.Any(p => p.RecordOrder > page.RecordOrder && p.RecordOrder < relativePage.RecordOrder))
                        {
                            page.RecordOrder = relativePage.RecordOrder;
                            //Set this for keep relative page update in static db context
                            relativePage.RecordOrder = relativePage.RecordOrder + 1;
                            var query =
                                string.Format(
                                    "Update Pages set RecordOrder = RecordOrder + 1 Where {0} And RecordOrder >= {1}",
                                    relativePage.ParentId.HasValue ? string.Format(" ParentId = {0}", relativePage.ParentId) : "ParentId Is NULL", relativePage.RecordOrder);
                            _pageRepository.ExcuteSql(query);
                        }
                    }
                    else
                    {
                        if (page.RecordOrder < relativePage.RecordOrder || relativePages.Any(p => p.RecordOrder < page.RecordOrder && p.RecordOrder > relativePage.RecordOrder))
                        {
                            page.RecordOrder = relativePage.RecordOrder + 1;
                            var query =
                                string.Format(
                                    "Update Pages set RecordOrder = RecordOrder + 1 Where {0} And RecordOrder > {1}",
                                    relativePage.ParentId.HasValue
                                        ? string.Format(" ParentId = {0}", relativePage.ParentId)
                                        : "ParentId Is NULL", relativePage.RecordOrder);
                            _pageRepository.ExcuteSql(query);
                        }
                    }
                }

                page.ParentId = model.ParentId;
                response = HierarchyUpdate(page);
                if (response.Success)
                {
                    _clientMenuServices.SavePageToClientMenu(page.Id);
                    _pageLogServices.SavePageLog(pageLog);
                }

                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::Pages:::Messages:::UpdateSuccessfully:::Update page successfully.")
                    : _localizedResourceServices.T("AdminModule:::Pages:::Messages:::UpdateFailure:::Update page failed. Please try again later."));
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
                FileTemplateId = model.FileTemplateId,
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
                    _pageRepository.ExcuteSql(query);
                }
                else
                {
                    page.RecordOrder = relativePage.RecordOrder + 1;
                    var query =
                        string.Format(
                            "Update Pages set RecordOrder = RecordOrder + 1 Where {0} And RecordOrder > {1}",
                            relativePage.ParentId.HasValue ? string.Format(" ParentId = {0}", relativePage.ParentId) : "ParentId Is NULL", relativePage.RecordOrder);
                    _pageRepository.ExcuteSql(query);
                }
            }

            response = HierarchyInsert(page);

            if (response.Success)
            {
                _clientMenuServices.SavePageToClientMenu(response.Data.ToInt());
            }
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::Pages:::Messages:::CreateSuccessfully:::Create page successfully.")
                : _localizedResourceServices.T("AdminModule:::Pages:::Messages:::CreateFailure:::Create page failed. Please try again later."));
        }

        #endregion

        #region Logs

        /// <summary>
        /// Get page log model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public PageLogsModel GetLogs(int id, int index = 1)
        {
            var pageSize = _settingServices.GetSetting<int>(SettingNames.LogsPageSize);
            var page = GetById(id);
            if (page != null)
            {
                var model = new PageLogsModel
                {
                    Id = page.Id,
                    Title = page.Title,
                    Url = page.FriendlyUrl,
                    Logs = page.PageLogs.OrderByDescending(l => l.Created)
                        .Skip((index - 1) * pageSize).Take(pageSize).Select(l => new PageLogViewModel(l)).ToList(),
                    LoadComplete = (page.PageLogs.Count <= index * pageSize)
                };
                return model;
            }
            return null;
        }
        #endregion

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
                WorkContext.ActivePageId = page.Id;
                var model = new PageRenderModel(page);
                if (model.IsFileTemplate) return model;
                using (var templateService = new TemplateService())
                {
                    var template = _pageTemplateServices.RenderPageTemplate(page.PageTemplateId, model);
                    if (template.IndexOf(Configurations.RenderBody, StringComparison.Ordinal) > -1)
                    {
                        template = template.Replace(Configurations.RenderBody, "@Raw(Model.Content)");
                    }
                    template = templateService.Parse(template, model, null, page.Title);

                    model.Content = _curlyBracketServices.Render(template);
                    return model;
                }
            }
            return null;
        }

        #region Select List

        /// <summary>
        /// Get all tags of page
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPageTags(int? pageId = null)
        {
            var tagRepository = new TagRepository();
            var pageTagIds = new List<int>();
            var page = GetById(pageId);
            if (page != null)
            {
                pageTagIds = page.PageTags.Select(t => t.TagId).ToList();
            }
            return tagRepository.GetAll().Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = SqlFunctions.StringConvert((double)t.Id).Trim(),
                Selected = pageTagIds.Contains(t.Id)
            });
        }

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
                pages = _pageRepository.GetPossibleParents(page);
            }
            var data = pages.Select(m => new HierarchyModel
            {
                Id = m.Id,
                Name = m.Title,
                Hierarchy = m.Hierarchy,
                RecordOrder = m.RecordOrder,
                Selected = parentId.HasValue && parentId.Value == m.Id
            }).ToList();
            return _pageRepository.BuildSelectList(data);
        }

        /// <summary>
        /// Get status of page
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetStatus()
        {
            return EnumUtilities.GetSelectListFromEnum<PageEnums.PageStatusEnums>();
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
            return Fetch(p => (!pageId.HasValue || p.Id != pageId)
                              && (parentId.HasValue ? p.ParentId == parentId : p.ParentId == null))
                .OrderBy(p => p.RecordOrder).Select(p => new SelectListItem
                    {
                        Text = p.Title,
                        Value = SqlFunctions.StringConvert((double)p.Id).Trim()
                    });
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
            if (string.IsNullOrEmpty(friendlyUrl))
            {
                return GetHomePage();
            }

            return GetAll().FirstOrDefault(m => m.IncludeInSiteNavigation
                                       && (!m.StartPublishingDate.HasValue || DateTime.Now > m.StartPublishingDate)
                                       && (!m.EndPublishingDate.HasValue || DateTime.Now < m.StartPublishingDate)
                                       && m.FriendlyUrl.Equals(friendlyUrl, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Get home page
        /// </summary>
        /// <returns></returns>
        public Page GetHomePage()
        {
            return _pageRepository.FetchFirst(p => p.IsHomePage);
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
        /// Check if title is existed
        /// </summary>
        /// <param name="pageId">the page id</param>
        /// <param name="title">the page title</param>
        /// <returns></returns>
        public bool IsTitleExisted(int? pageId, string title)
        {
            return Fetch(u => u.Title.Equals(title) && (!pageId.HasValue || u.Id != pageId)).Any();
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
