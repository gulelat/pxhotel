using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using AutoMapper;
using PX.Business.Models.FileTemplates;
using PX.Business.Services.PageTemplates;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Configurations.Constants;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using PX.EntityModel.Repositories;
using PX.EntityModel.Repositories.RepositoryBase.Models;

namespace PX.Business.Services.FileTemplates
{
    public class FileTemplateServices : IFileTemplateServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly IPageTemplateServices _pageTemplateServices;
        public FileTemplateServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _pageTemplateServices = HostContainer.GetInstance<IPageTemplateServices>();
        }

        #region Initialize

        public void InitializeFileTemplates()
        {
            
        }
        #endregion

        #region Base
        public IQueryable<FileTemplate> GetAll()
        {
            return FileTemplateRepository.GetAll();
        }
        public IQueryable<FileTemplate> Fetch(Expression<Func<FileTemplate, bool>> expression)
        {
            return FileTemplateRepository.Fetch(expression);
        }
        public FileTemplate FetchFirst(Expression<Func<FileTemplate, bool>> expression)
        {
            return FileTemplateRepository.FetchFirst(expression);
        }
        public FileTemplate GetById(object id)
        {
            return FileTemplateRepository.GetById(id);
        }
        public ResponseModel Insert(FileTemplate fileTemplate)
        {
            return FileTemplateRepository.Insert(fileTemplate);
        }
        public ResponseModel Update(FileTemplate fileTemplate)
        {
            return FileTemplateRepository.Update(fileTemplate);
        }
        public ResponseModel HierarchyUpdate(FileTemplate fileTemplate)
        {
            return FileTemplateRepository.HierarchyUpdate(fileTemplate);
        }
        public ResponseModel HierarchyInsert(FileTemplate fileTemplate)
        {
            return FileTemplateRepository.HierarchyInsert(fileTemplate);
        }
        public ResponseModel Delete(FileTemplate fileTemplate)
        {
            return FileTemplateRepository.Delete(fileTemplate);
        }
        public ResponseModel Delete(object id)
        {
            return FileTemplateRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return FileTemplateRepository.InactiveRecord(id);
        }
        #endregion

        #region Grid Search
        /// <summary>
        /// search the FileTemplates.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchFileTemplates(JqSearchIn si)
        {
            var fileTemplates = GetAll().Select(u => new FileTemplateModel
            {
                Id = u.Id,
                Name = u.Name,
                Controller = u.Controller,
                Action = u.Action,
                Parameters = u.Parameters,
                PageTemplateId = u.PageTemplateId,
                PageTemplateName = u.PageTemplateId.HasValue ? u.PageTemplate.Name : string.Empty,
                ParentId = u.ParentId,
                ParentName = u.ParentId.HasValue ? u.FileTemplate1.Name : string.Empty,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(fileTemplates);
        }

        #endregion

        #region Manage Grid

        /// <summary>
        /// Manage Site Setting
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the file template model</param>
        /// <returns></returns>
        public ResponseModel ManageFileTemplate(GridOperationEnums operation, FileTemplateModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<FileTemplateModel, FileTemplate>();
            FileTemplate fileTemplate;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    fileTemplate = GetById(model.Id);
                    fileTemplate.Name = model.Name;
                    fileTemplate.Controller = model.Controller;
                    fileTemplate.Action = model.Action;
                    fileTemplate.Parameters = model.Parameters;
                    fileTemplate.ParentId = model.ParentName.ToNullableInt();
                    fileTemplate.PageTemplateId = model.PageTemplateName.ToNullableInt();

                    response = HierarchyUpdate(fileTemplate);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::FileTemplates:::Messages:::UpdateSuccessfully:::Update file template successfully.")
                        : _localizedResourceServices.T("AdminModule:::FileTemplates:::Messages:::UpdateFailure:::Update file template failed. Please try again later."));

                case GridOperationEnums.Add:
                    fileTemplate = Mapper.Map<FileTemplateModel, FileTemplate>(model);
                    fileTemplate.ParentId = model.ParentName.ToNullableInt();
                    fileTemplate.PageTemplateId = model.PageTemplateName.ToNullableInt();
                    response = HierarchyInsert(fileTemplate);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::FileTemplates:::Messages:::CreateSuccessfully:::Create file template successfully.")
                        : _localizedResourceServices.T("AdminModule:::FileTemplate:::Messagess:::CreateFailure:::Insert file template failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::FileTemplates:::Messages:::DeleteSuccessfully:::Messages:::Delete file template successfully.")
                        : _localizedResourceServices.T("AdminModule:::FileTemplates:::Messages:::DeleteFailure:::Delete file template failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::FileTemplates:::Messages:::ObjectNotFounded:::File template is not founded.")
            };
        }
        
        #endregion

        #region Manage


        /// <summary>
        /// Get file template manage model for edit/create
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public FileTemplateManageModel GetTemplateManageModel(int? id = null)
        {
            var template = GetById(id);
            if (template != null)
            {
                return new FileTemplateManageModel
                {
                    Id = template.Id,
                    Name = template.Name,
                    Action = template.Action,
                    Controller = template.Controller,
                    Parameters = template.Parameters,
                    PageTemplateId = template.PageTemplateId,
                    PageTemplates = _pageTemplateServices.GetPageTemplateSelectListForFileTemplate(template.Id),
                    ParentId = template.ParentId,
                    Parents = GetPossibleParents(template.Id)
                };
            }
            return new FileTemplateManageModel
            {
                PageTemplates = _pageTemplateServices.GetPageTemplateSelectListForFileTemplate(),
                Parents = GetPossibleParents()
            };
        }

        /// <summary>
        /// Save file template
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SaveFileTemplate(FileTemplateManageModel model)
        {
            ResponseModel response;
            var fileTemplate = GetById(model.Id);
            if (fileTemplate != null)
            {
                fileTemplate.Name = model.Name;
                fileTemplate.Action = model.Action;
                fileTemplate.Controller = model.Controller;
                fileTemplate.Parameters = model.Parameters;
                fileTemplate.ParentId = model.ParentId;

                response = HierarchyUpdate(fileTemplate);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::FileTemplates:::Messages:::UpdateSuccessfully:::Update file template successfully.")
                    : _localizedResourceServices.T("AdminModule:::FileTemplates:::Messages:::UpdateFailure:::Update file template failed. Please try again later."));
            }
            Mapper.CreateMap<FileTemplateManageModel, FileTemplate>();
            fileTemplate = Mapper.Map<FileTemplateManageModel, FileTemplate>(model);
            response = HierarchyInsert(fileTemplate);
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::FileTemplates:::Messages:::CreateSuccessfully:::Create file template successfully.")
                : _localizedResourceServices.T("AdminModule:::FileTemplates:::Messages:::CreateFailure:::Create file template failed. Please try again later."));
        }
        #endregion

        /// <summary>
        /// Get possible parent menu
        /// </summary>
        /// <param name="id">the current menu id</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPossibleParents(int? id = null)
        {
            var fileTemplates = GetAll();
            int? parentId = null;
            var template = GetById(id);
            if (template != null)
            {
                parentId = template.ParentId;
                fileTemplates = FileTemplateRepository.GetPossibleParents(template);
            }
            var data = fileTemplates.Select(m => new HierarchyModel
            {
                Id = m.Id,
                Name = m.Name,
                Hierarchy = m.Hierarchy,
                RecordOrder = m.RecordOrder,
                Selected = parentId.HasValue && parentId.Value == m.Id
            }).ToList();
            return FileTemplateRepository.BuildSelectList(data, DefaultConstants.HierarchyLevelPrefix, false);
        }

        /// <summary>
        /// Gets the user groups.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetFileTemplateSelectList(int? id = null)
        {
            var fileTemplates = GetAll();
            int? templateId = null;
            var file = PageRepository.GetById(id);
            if (file != null)
            {
                templateId = file.FileTemplateId;
            }
            var data = fileTemplates.Select(m => new HierarchyModel
            {
                Id = m.Id,
                Name = m.Name,
                Hierarchy = m.Hierarchy,
                RecordOrder = m.RecordOrder,
                Selected = templateId.HasValue && templateId.Value == m.Id
            }).ToList();
            return FileTemplateRepository.BuildSelectList(data);
        }

        /// <summary>
        /// Get file by parent id
        /// </summary>
        /// <param name="parentId">the parent id</param>
        /// <returns></returns>
        public List<FileTemplate> GetFileTemplates(int? parentId = null)
        {
            return GetAll().Where(m => parentId.HasValue ? m.ParentId == parentId : !m.ParentId.HasValue).OrderBy(m => m.RecordOrder).ToList();
        }

        /// <summary>
        /// Check if template name is existed
        /// </summary>
        /// <param name="fileTemplateId">the template id</param>
        /// <param name="name">the template name</param>
        /// <returns></returns>
        public bool IsFileTemplateNameExisted(int? fileTemplateId, string name)
        {
            return Fetch(t => t.Id != fileTemplateId && t.Name.Equals(name)).Any();
        }
        
        /// <summary>
        /// Check if template existed for virtual path provider
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public bool IsFileTemplateExisted(string filePath)
        {
            var templates = filePath.Split('/').Last().Split('.');
            if (!templates.First().Equals("DBTemplate", StringComparison.InvariantCultureIgnoreCase) || templates.Count() < 3)
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
        public FileTemplate FindTemplate(string filePath)
        {
            var templates = filePath.Split('/').Last().Split('.');
            if (!templates.First().Equals("DBTemplate", StringComparison.InvariantCultureIgnoreCase) || templates.Count() < 3)
            {
                return null;
            }
            var templateName = templates[1];
            return Fetch(t => t.Name.Equals(templateName)).FirstOrDefault();
        }

        /// <summary>
        /// Get master template from db
        /// </summary>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public string GetFileTemplateMaster(string controller, string action)
        {
            var fileTemplate = FetchFirst(t => t.Controller.Equals(controller) && t.Action.Equals(action));
            if(fileTemplate != null)
            {
                return fileTemplate.PageTemplateId.HasValue ? string.Format("{0}.{1}", "DBTemplate", fileTemplate.PageTemplate.Name) : string.Empty;
            }
            return string.Empty;
        }
    }
}
