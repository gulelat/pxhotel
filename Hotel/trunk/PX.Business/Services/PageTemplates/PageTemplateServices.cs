using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using PX.Business.Models.PageTemplateLogs;
using PX.Business.Models.PageTemplates;
using PX.Business.Models.Pages;
using PX.Business.Services.CurlyBrackets;
using PX.Business.Services.PageTemplateLogs;
using PX.Business.Services.Settings;
using PX.Business.Services.Templates;
using PX.Core.Configurations;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;
using PX.EntityModel.Repositories.RepositoryBase.Models;
using RazorEngine;
using RazorEngine.Templating;

namespace PX.Business.Services.PageTemplates
{
    public class PageTemplateServices : IPageTemplateServices
    {
        public const string DBTemplate = "DBTemplate";
        private const string DefaultTemplateName = "DefaultMasterTemplateWithRenderContentOnly";
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly IPageTemplateLogServices _pageTemplateLogServices;
        private readonly ISettingServices _settingServices;
        public PageTemplateServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _pageTemplateLogServices = HostContainer.GetInstance<IPageTemplateLogServices>();
            _settingServices = HostContainer.GetInstance<ISettingServices>();
        }

        #region Initialize

        public void InitializeFileTemplates()
        {

        }
        #endregion

        #region Base
        public IQueryable<PageTemplate> GetAll()
        {
            return PageTemplateRepository.GetAll();
        }
        public IQueryable<PageTemplate> Fetch(Expression<Func<PageTemplate, bool>> expression)
        {
            return PageTemplateRepository.Fetch(expression);
        }
        public PageTemplate FetchFirst(Expression<Func<PageTemplate, bool>> expression)
        {
            return PageTemplateRepository.FetchFirst(expression);
        }
        public PageTemplate GetById(object id)
        {
            return PageTemplateRepository.GetById(id);
        }
        public ResponseModel Insert(PageTemplate pageTemplate)
        {
            return PageTemplateRepository.Insert(pageTemplate);
        }
        public ResponseModel Update(PageTemplate pageTemplate)
        {
            return PageTemplateRepository.Update(pageTemplate);
        }
        public ResponseModel HierarchyUpdate(PageTemplate pageTemplate)
        {
            return PageTemplateRepository.HierarchyUpdate(pageTemplate);
        }
        public ResponseModel HierarchyInsert(PageTemplate pageTemplate)
        {
            return PageTemplateRepository.HierarchyInsert(pageTemplate);
        }
        public ResponseModel Delete(PageTemplate pageTemplate)
        {
            return PageTemplateRepository.Delete(pageTemplate);
        }
        public ResponseModel Delete(object id)
        {
            return PageTemplateRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return PageTemplateRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the PageTemplates.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchPageTemplates(JqSearchIn si)
        {
            var pageTemplates = GetAll().Select(u => new PageTemplateModel
            {
                Id = u.Id,
                Name = u.Name,
                ParentId = u.ParentId,
                ParentName = u.ParentId.HasValue ? u.PageTemplate1.Name : string.Empty,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(pageTemplates);
        }

        #endregion

        #region Grid Manage

        /// <summary>
        /// Manage Site Setting
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the page template model</param>
        /// <returns></returns>
        public ResponseModel ManagePageTemplate(GridOperationEnums operation, PageTemplateModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<PageTemplateModel, PageTemplate>();
            PageTemplate pageTemplate;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    pageTemplate = GetById(model.Id);
                    pageTemplate.Name = model.Name;
                    pageTemplate.ParentId = model.ParentName.ToNullableInt();

                    response = HierarchyUpdate(pageTemplate);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::PageTemplates:::Messages:::UpdateSuccessfully:::Update page template successfully.")
                        : _localizedResourceServices.T("AdminModule:::PageTemplates:::Messages:::UpdateFailure:::Update page template failed. Please try again later."));

                case GridOperationEnums.Add:
                    pageTemplate = Mapper.Map<PageTemplateModel, PageTemplate>(model);
                    pageTemplate.ParentId = model.ParentName.ToNullableInt();
                    pageTemplate.Content = Configurations.CurlyBracketRenderBody;
                    response = HierarchyInsert(pageTemplate);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::PageTemplates:::Messages:::CreateSuccessfully:::Create page template successfully.")
                        : _localizedResourceServices.T("AdminModule:::PageTemplates:::Messages:::CreateFailure:::Create page template failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::PageTemplates:::Messages:::DeleteSuccessfully:::Delete page template successfully.")
                        : _localizedResourceServices.T("AdminModule:::PageTemplates:::Messages:::DeleteFailure:::Delete page template failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::PageTemplates:::Messages:::ObjectNotFounded:::Page template is not founded.")
            };
        }
        #endregion

        #region Manage

        /// <summary>
        /// Get page template manage model for edit/create
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PageTemplateManageModel GetTemplateManageModel(int? id = null)
        {
            var template = GetById(id);
            return template != null ? new PageTemplateManageModel(template) : new PageTemplateManageModel();
        }

        /// <summary>
        /// Get page template manage model for edit/create
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PageTemplateManageModel GetTemplateManageModelByLogId(int? id = null)
        {
            var log = PageTemplateLogRepository.GetById(id);
            return log != null ? new PageTemplateManageModel(log) : new PageTemplateManageModel();
        }

        /// <summary>
        /// Save page template
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SavePageTemplate(PageTemplateManageModel model)
        {
            ResponseModel response;
            var pageTemplate = GetById(model.Id);
            if (pageTemplate != null)
            {
                var log = new PageTemplateLogManageModel(pageTemplate);
                var childTemplates = new List<PageTemplate>();
                if (pageTemplate.Name.Equals(DefaultTemplateName))
                {
                    childTemplates = GetAll().Where(t => !t.Name.Equals(DefaultTemplateName)).ToList();
                }
                else if (!pageTemplate.Content.Equals(model.Content) || pageTemplate.ParentId != model.ParentId)
                {
                    childTemplates = PageTemplateRepository.GetHierarcies(pageTemplate).ToList();
                }
                if (childTemplates.Any())
                {
                    foreach (var childTemplate in childTemplates)
                    {
                        Update(childTemplate);
                    }
                }

                pageTemplate.Name = model.Name;
                pageTemplate.Content = model.Content;

                pageTemplate.ParentId = model.ParentId;

                response = HierarchyUpdate(pageTemplate);

                if (response.Success)
                {
                    _pageTemplateLogServices.SavePageTemplateLog(log);
                }

                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::PageTemplates:::Messages:::UpdateSuccessfully:::Update page template successfully.")
                    : _localizedResourceServices.T("AdminModule:::PageTemplates:::Messages:::UpdateFailure:::Update page template failed. Please try again later."));
            }
            Mapper.CreateMap<PageTemplateManageModel, PageTemplate>();
            pageTemplate = Mapper.Map<PageTemplateManageModel, PageTemplate>(model);
            response = HierarchyInsert(pageTemplate);
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::PageTemplates:::Messages:::CreateSuccessfully:::Create page template successfully.")
                : _localizedResourceServices.T("AdminModule:::PageTemplates:::Messages:::CreateFailure:::Create page template failed. Please try again later."));
        }

        #endregion

        #region Logs

        /// <summary>
        /// Get page log model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public PageTemplateLogsModel GetLogs(int id, int index = 1)
        {
            var pageSize = _settingServices.GetSetting<int>(SettingNames.LogsPageSize);
            var pageTemplate = GetById(id);
            if (pageTemplate != null)
            {
                var model = new PageTemplateLogsModel
                {

                    Id = pageTemplate.Id,
                    Name = pageTemplate.Name,
                    Logs = pageTemplate.PageTemplateLogs.OrderByDescending(l => l.Created)
                        .Skip((index - 1) * pageSize).Take(pageSize).Select(l => new PageTemplateLogViewModel(l)).ToList(),
                    LoadComplete = (pageTemplate.PageTemplateLogs.Count <= index * pageSize)
                };
                return model;
            }
            return null;
        }
        #endregion

        /// <summary>
        /// Get possible parent menu
        /// </summary>
        /// <param name="id">the current menu id</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPossibleParents(int? id = null)
        {
            var pageTemplates = GetAll();
            int? parentId = null;
            var template = GetById(id);
            if (template != null)
            {
                parentId = template.ParentId;
                pageTemplates = PageTemplateRepository.GetPossibleParents(template);
            }
            var data = pageTemplates.Select(m => new HierarchyModel
            {
                Id = m.Id,
                Name = m.Name,
                Hierarchy = m.Hierarchy,
                RecordOrder = m.RecordOrder,
                Selected = parentId.HasValue && parentId.Value == m.Id
            }).ToList();
            return PageTemplateRepository.BuildSelectList(data, false);
        }

        /// <summary>
        /// Get page by parent id
        /// </summary>
        /// <param name="parentId">the parent id</param>
        /// <returns></returns>
        public List<PageTemplate> GetPageTemplates(int? parentId = null)
        {
            return GetAll().Where(m => parentId.HasValue ? m.ParentId == parentId : !m.ParentId.HasValue).OrderBy(m => m.RecordOrder).ToList();
        }

        /// <summary>
        /// Gets the user groups.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPageTemplateSelectList(int? id = null)
        {
            var pageTemplates = GetAll();
            int? templateId = null;
            var page = PageRepository.GetById(id);
            if (page != null)
            {
                templateId = page.PageTemplateId;
            }
            var data = pageTemplates.Select(m => new HierarchyModel
            {
                Id = m.Id,
                Name = m.Name,
                Hierarchy = m.Hierarchy,
                RecordOrder = m.RecordOrder,
                Selected = templateId.HasValue && templateId.Value == m.Id
            }).ToList();
            return PageTemplateRepository.BuildSelectList(data);
        }

        /// <summary>
        /// Get page template for file template
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPageTemplateSelectListForFileTemplate(int? id = null)
        {
            var pageTemplates = GetAll();
            int? templateId = null;
            var fileTemplate = FileTemplateRepository.GetById(id);
            if (fileTemplate != null)
            {
                templateId = fileTemplate.PageTemplateId;
            }
            var data = pageTemplates.Select(m => new HierarchyModel
            {
                Id = m.Id,
                Name = m.Name,
                Hierarchy = m.Hierarchy,
                RecordOrder = m.RecordOrder,
                Selected = templateId.HasValue && templateId.Value == m.Id
            }).ToList();
            return PageTemplateRepository.BuildSelectList(data);
        }

        /// <summary>
        /// Check if template name is existed
        /// </summary>
        /// <param name="pageTemplateId">the template id</param>
        /// <param name="name">the template name</param>
        /// <returns></returns>
        public bool IsPageTemplateNameExisted(int? pageTemplateId, string name)
        {
            return Fetch(t => t.Id != pageTemplateId && t.Name.Equals(name)).Any();
        }

        /// <summary>
        /// Check if template existed for virtual path provider
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool IsPageTemplateExisted(string filePath)
        {
            var templates = filePath.Split('/').Last().Split('.');
            if (!templates.First().Equals(DBTemplate, StringComparison.InvariantCultureIgnoreCase) || templates.Count() < 3)
            {
                return false;
            }
            var templateName = templates[1];
            return Fetch(t => t.Name.Equals(templateName)).Any();
        }

        /// <summary>
        /// Check if template existed for virtual path provider
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public PageTemplate FindTemplate(string filePath)
        {
            var templates = filePath.Split('/').Last().Split('.');
            if (!templates.First().Equals(DBTemplate, StringComparison.InvariantCultureIgnoreCase) || templates.Count() < 3)
            {
                return null;
            }
            var templateName = templates[1];
            return Fetch(t => t.Name.Equals(templateName)).FirstOrDefault();
        }

        #region Render Page Template
        public string RenderPageTemplate(int? pageTemplateId, PageRenderModel model)
        {
            var pageTemplate = GetById(pageTemplateId);
            using (var templateService = new TemplateService())
            {
                /* 
                 * Get default master template for all content
                 * This template is used for including some scripts or html for all page contents and file contents
                */
                var defaultTemplate = GetDefaultTemplate();
                var template = defaultTemplate.Content;
                template = CurlyBracketParser.ParseProperties(template);
                template = CurlyBracketParser.ParseRenderBody(template);

                var layout = TemplateServices.GetTemplateCacheName(defaultTemplate.Name, defaultTemplate.Created,
                                                                   defaultTemplate.Updated);
                if (Razor.Resolve(layout) == null)
                {
                    templateService.Compile(template, typeof(PageRenderModel), layout);
                }

                /*
                 * Loop all the parent template to compile and render layout
                */
                if (pageTemplate != null)
                {
                    // Using hierarchy to load all parent templates
                    var pageTemplates =
                        PageTemplateRepository.GetAll().Where(t => pageTemplate.Hierarchy.Contains(t.Hierarchy))
                        .OrderBy(t => t.Hierarchy)
                        .Select(t => new
                        {
                            t.Content,
                            t.Name,
                            t.Updated,
                            t.Created
                        });
                    if (pageTemplates.Any())
                    {
                        foreach (var item in pageTemplates)
                        {
                            //Convert curly bracket properties to razor syntax
                            template = CurlyBracketParser.ParseProperties(item.Content);

                            //Insert master page for child template and parsing
                            template = InsertMasterPage(template, layout);
                            template = FormatMaster(template);
                            template = templateService.Parse(template, model, null, null);
                            template = ReformatMaster(template);

                            //This used for re-cache the template
                            layout = TemplateServices.GetTemplateCacheName(item.Name, item.Created, item.Updated);

                            //Convert {RenderBody} to @RenderBody() for next rendering
                            template = CurlyBracketParser.ParseRenderBody(template);
                            if (Razor.Resolve(layout) == null)
                            {
                                templateService.Compile(template, typeof(PageRenderModel), layout);
                            }
                        }
                    }
                }
                return template;
            }
        }

        /// <summary>
        /// Get default page template
        /// </summary>
        /// <returns></returns>
        private PageTemplate GetDefaultTemplate()
        {
            var defaultTemplate = FetchFirst(t => t.Name.Equals(DefaultTemplateName));
            if (defaultTemplate == null)
            {
                defaultTemplate = new PageTemplate
                {
                    Name = "DefaultMasterTemplateWithRenderContentOnly",
                    Content = Configurations.RenderBody,
                    RecordOrder = 0,
                    RecordActive = true
                };
                HierarchyInsert(defaultTemplate);
                return defaultTemplate;
            }
            return defaultTemplate;
        }

        /// <summary>
        /// Add parent layout to page template
        /// </summary>
        /// <param name="content"></param>
        /// <param name="masterPage"></param>
        /// <returns></returns>
        private string InsertMasterPage(string content, string masterPage)
        {
            return "@{ this.Layout = \"" + masterPage + "\";}" + content;
        }

        /// <summary>
        /// Remove some syntax not valid for child template
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string FormatMaster(string content)
        {
            //Remove render section to render master
            content = content.Replace("@RenderSection", "RenderSection");
            return content;
        }

        /// <summary>
        /// Re-add the syntax for next rendering
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        private string ReformatMaster(string content)
        {
            //Add render section after render master
            content = content.Replace("RenderSection", "@RenderSection");
            return content;
        }

        #endregion
    }
}
