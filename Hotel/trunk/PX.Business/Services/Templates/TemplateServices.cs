using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.Templates;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Mvc.WorkContext;
using PX.Business.Services.CurlyBrackets;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using AutoMapper;
using PX.EntityModel.Repositories;
using RazorEngine.Templating;

namespace PX.Business.Services.Templates
{
    public class TemplateServices : ITemplateServices
    {
        #region Private Properties
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly ICurlyBracketServices _curlyBracketServices;

        #endregion

        #region Constructor
        public TemplateServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _curlyBracketServices = HostContainer.GetInstance<ICurlyBracketServices>();
        }
        #endregion

        #region Initialize
        /// <summary>
        /// Initialize all default template
        /// </summary>
        public void InitializeTemplates()
        {
            var types = ReflectionUtilities.GetAllImplementTypesOfInterface(typeof (ICurlyBracketResolver));
            foreach (var type in types)
            {
                var instance = (ICurlyBracketResolver)Activator.CreateInstance(type);
                instance.Initialize();
            }
            WorkContext.CurlyBrackets = _curlyBracketServices.GetAllCurlyBracketsOfApplication();
        }
        #endregion

        #region Base
        public IQueryable<Template> GetAll()
        {
            return TemplateRepository.GetAll();
        }
        public IQueryable<Template> Fetch(Expression<Func<Template, bool>> expression)
        {
            return TemplateRepository.Fetch(expression);
        }
        public Template GetById(object id)
        {
            return TemplateRepository.GetById(id);
        }
        public ResponseModel Insert(Template template)
        {
            return TemplateRepository.Insert(template);
        }
        public ResponseModel Update(Template template)
        {
            return TemplateRepository.Update(template);
        }
        public ResponseModel Delete(Template template)
        {
            return TemplateRepository.Delete(template);
        }
        public ResponseModel Delete(object id)
        {
            return TemplateRepository.Delete(id);
        }
        #endregion

        #region Grid Search

        /// <summary>
        /// search the user groups.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchTemplates(JqSearchIn si)
        {
            var templates = GetAll().Select(u => new TemplateModel
            {
                Id = u.Id,
                Name = u.Name,
                DataType = u.DataType,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(templates);
        }

        #endregion

        #region Grid Manage

        /// <summary>
        /// Manage user group
        /// </summary>
        /// <param name="operation">the operation</param>
        /// <param name="model">the user group model</param>
        /// <returns></returns>
        public ResponseModel ManageTemplate(GridOperationEnums operation, TemplateModel model)
        {
            ResponseModel response;
            Mapper.CreateMap<TemplateModel, Template>();
            Template template;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    template = TemplateRepository.GetById(model.Id);
                    template.Name = model.Name;
                    template.DataType = model.DataType;
                    template.RecordOrder = model.RecordOrder;
                    template.RecordActive = model.RecordActive;
                    response = Update(template);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Templates:::Messages:::UpdateSuccessfully:::Update template successfully.")
                        : _localizedResourceServices.T("AdminModule:::Templates:::Messages:::UpdateFailure:::Update template failed. Please try again later."));

                case GridOperationEnums.Add:
                    template = Mapper.Map<TemplateModel, Template>(model);
                    template.Content = string.Empty;
                    response = Insert(template);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Templates:::Messages:::CreateSuccessfully:::Create template successfully.")
                        : _localizedResourceServices.T("AdminModule:::Templates:::Messages:::CreateFailure:::Create template failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Templates:::Messages:::DeleteSuccessfully:::Delete template successfully.")
                        : _localizedResourceServices.T("AdminModule:::Templates:::Messages:::DeleteFailure:::Delete template failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Templates:::Messages:::ObjectNotFounded:::Template is not founded")
            };
        }

        #endregion

        #region Manage
        /// <summary>
        /// Get template by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TemplateManageModel GetTemplateByName(string name)
        {
            var template = TemplateRepository.FetchFirst(t => t.Name.Equals(name));
            if (template != null)
            {
                return new TemplateManageModel
                {
                    Id = template.Id,
                    Name = template.Name,
                    Content = template.Content,
                    DataType = template.DataType
                };
            }
            return null;
        }

        /// <summary>
        /// Get template manage model from id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TemplateManageModel GetTemplateManageModel(int? id = null)
        {
            var template = GetById(id);
            if (template != null)
            {
                return new TemplateManageModel
                {
                    Id = template.Id,
                    Name = template.Name,
                    Content = template.Content,
                    DataType = template.DataType
                };
            }
            return new TemplateManageModel();
        }

        /// <summary>
        /// Get template manage model from type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public TemplateManageModel GetTemplateManageModel(string type)
        {
            var model = new TemplateManageModel
            {
                DataType = type
            };
            return model;
        }

        public ResponseModel SaveTemplate(TemplateManageModel model)
        {
            ResponseModel response;
            var pageTemplate = GetById(model.Id);
            if (pageTemplate != null)
            {
                pageTemplate.Name = model.Name;
                pageTemplate.Content = model.Content;
                pageTemplate.DataType = model.DataType;

                response = Update(pageTemplate);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::Templates:::Messages:::UpdateSuccessfully:::Update template successfully.")
                    : _localizedResourceServices.T("AdminModule:::Templates:::Messages:::UpdateFailure:::Update template failed. Please try again later."));
            }
            Mapper.CreateMap<TemplateManageModel, Template>();
            pageTemplate = Mapper.Map<TemplateManageModel, Template>(model);
            response = Insert(pageTemplate);
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::Templates:::Messages:::CreateSuccessfully:::Create template successfully.")
                : _localizedResourceServices.T("AdminModule:::Templates:::Messages:::CreateFailure:::Create template failed. Please try again later."));
        }

        #endregion

        /// <summary>
        /// Render template using Razor engine
        /// </summary>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <param name="cacheName"></param>
        /// <returns></returns>
        public string RenderTemplate(string template, dynamic model, string cacheName = "")
        {
            var templateService = new TemplateService();
            return templateService.Parse(template, model, null, cacheName);
        }

        /// <summary>
        /// Check if template name is existed
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsTemplateNameExisted(int? templateId, string name)
        {
            return Fetch(t => t.Id != templateId && t.Name.Equals(name)).Any();
        }
    }
}
