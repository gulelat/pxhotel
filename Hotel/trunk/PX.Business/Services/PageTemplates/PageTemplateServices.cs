using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using PX.Business.Models.PageTemplates;
using PX.Business.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Configurations.Constants;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;
using PX.EntityModel.Repositories.RepositoryBase.Extensions;
using PX.EntityModel.Repositories.RepositoryBase.Models;

namespace PX.Business.Services.PageTemplates
{
    public class PageTemplateServices : IPageTemplateServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public PageTemplateServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base
        public IQueryable<PageTemplate> GetAll()
        {
            return PageTemplateRepository.GetAll();
        }
        public IQueryable<PageTemplate> Fetch(Expression<Func<PageTemplate, bool>> expression)
        {
            return PageTemplateRepository.Fetch(expression);
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
                ParentName = u.PageTemplate1 != null ? u.PageTemplate1.Name : string.Empty,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(pageTemplates);
        }

        #region Manage Page Template

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
                        _localizedResourceServices.T("AdminModule:::PageTemplates:::Update page template successfully")
                        : _localizedResourceServices.T("AdminModule:::PageTemplates:::Update page template failure"));

                case GridOperationEnums.Add:
                    pageTemplate = Mapper.Map<PageTemplateModel, PageTemplate>(model);
                    pageTemplate.ParentId = model.ParentName.ToNullableInt();
                    pageTemplate.Content = string.Empty;
                    response = HierarchyInsert(pageTemplate);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::PageTemplates:::Insert page template successfully")
                        : _localizedResourceServices.T("AdminModule:::PageTemplates:::Insert page template failure"));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::PageTemplates:::Delete page template successfully")
                        : _localizedResourceServices.T("AdminModule:::PageTemplates:::Delete page template failure"));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::PageTemplates:::Page template not founded")
            };
        }

        /// <summary>
        /// Get page template manage model for edit/create
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PageTemplateManageModel GetTemplateManageModel(int? id = null)
        {
            var template = GetById(id);
            if (template != null)
            {
                return new PageTemplateManageModel
                {
                    Id = template.Id,
                    Name = template.Name,
                    Content = template.Content,
                    ParentId = template.ParentId,
                    Parents = GetPossibleParents(template.Id)
                };
            }
            return new PageTemplateManageModel
            {
                Parents = GetPossibleParents()
            };
        }

        /// <summary>
        /// Save page template
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SaveTemplates(PageTemplateManageModel model)
        {
            ResponseModel response;
            var pageTemplate = GetById(model.Id);
            if (pageTemplate != null)
            {
                pageTemplate.Name = model.Name;
                pageTemplate.ParentId = model.ParentId;
                pageTemplate.Content = model.Content;

                response = HierarchyUpdate(pageTemplate);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::PageTemplates:::Update page template successfully")
                    : _localizedResourceServices.T("AdminModule:::PageTemplates:::Update page template failure"));
            }
            Mapper.CreateMap<PageTemplateManageModel, PageTemplate>();
            pageTemplate = Mapper.Map<PageTemplateManageModel, PageTemplate>(model);
            response = HierarchyInsert(pageTemplate);
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::PageTemplates:::Create page template successfully")
                : _localizedResourceServices.T("AdminModule:::PageTemplates:::Create page template failure"));
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
            if (id.HasValue)
            {
                var key = id.Value.GetHierarchyValueForRoot();
                pageTemplates = pageTemplates.Where(m => !m.Hierarchy.Contains(key));
            }

            var data = pageTemplates.ToList().Select(p => new HierarchyModel
            {
                Id = p.Id,
                Name = p.Name,
                Hierarchy = p.Hierarchy,
                RecordOrder = p.RecordOrder
            }).OrderBy(p => p.Hierarchy).ToList();
            return PageTemplateRepository.BuildSelectList(data, DefaultConstants.HierarchyLevelPrefix, false);
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
            return GetAll().ToList().Select(r => new SelectListItem
            {
                Text = r.Name,
                Value = r.Id.ToString(CultureInfo.InvariantCulture),
                Selected = r.Id == id
            });
        }

        /// <summary>
        /// Check if template title is existed
        /// </summary>
        /// <param name="pageTemplateId">the template id</param>
        /// <param name="title">the title</param>
        /// <returns></returns>
        public bool IsPageTemplateTitleExisted(int? pageTemplateId, string title)
        {
            return Fetch(t => t.Id != pageTemplateId && !t.Name.Equals(title)).Any();
        }
    }
}
