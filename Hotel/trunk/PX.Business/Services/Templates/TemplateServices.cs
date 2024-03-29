﻿using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.TemplateLogs;
using PX.Business.Models.Templates;
using PX.Business.Models.Templates.Logs;
using PX.Business.Mvc.ViewEngines.Razor.RazorEngine;
using PX.Business.Services.Settings;
using PX.Business.Services.TemplateLogs;
using PX.Business.Services.Users;
using PX.Core.Configurations;
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
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace PX.Business.Services.Templates
{
    public class TemplateServices : ITemplateServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly ITemplateLogServices _templateLogServices;
        private readonly IUserServices _userServices;
        private readonly ICurlyBracketServices _curlyBracketServices;
        private readonly ISettingServices _settingServices;
        private readonly TemplateRepository _templateRepository;
        private readonly TemplateLogRepository _templateLogRepository;
        public TemplateServices(PXHotelEntities entities)
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _templateLogServices = HostContainer.GetInstance<ITemplateLogServices>();
            _curlyBracketServices = HostContainer.GetInstance<ICurlyBracketServices>();
            _userServices = HostContainer.GetInstance<IUserServices>();
            _settingServices = HostContainer.GetInstance<ISettingServices>();
            _templateRepository = new TemplateRepository(entities);
            _templateLogRepository = new TemplateLogRepository(entities);
        }

        #region Initialize
        /// <summary>
        /// Initialize all default template
        /// </summary>
        public void InitializeTemplates()
        {
            var types = ReflectionUtilities.GetAllImplementTypesOfInterface(typeof(ICurlyBracketResolver));
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
            return _templateRepository.GetAll();
        }
        public IQueryable<Template> Fetch(Expression<Func<Template, bool>> expression)
        {
            return _templateRepository.Fetch(expression);
        }
        public Template FetchFirst(Expression<Func<Template, bool>> expression)
        {
            return _templateRepository.FetchFirst(expression);
        }
        public Template GetById(object id)
        {
            return _templateRepository.GetById(id);
        }
        public ResponseModel Insert(Template template)
        {
            return _templateRepository.Insert(template);
        }
        public ResponseModel Update(Template template)
        {
            return _templateRepository.Update(template);
        }
        public ResponseModel Delete(Template template)
        {
            return _templateRepository.Delete(template);
        }
        public ResponseModel Delete(object id)
        {
            return _templateRepository.Delete(id);
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
                    template = _templateRepository.GetById(model.Id);
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
                Message = _localizedResourceServices.T("AdminModule:::Templates:::Messages:::ObjectNotFounded:::Template is not founded.")
            };
        }

        #endregion

        #region Manage

        /// <summary>
        /// Get template manage model by log id
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public TemplateManageModel GetTemplateManageModelByLogId(int? logId)
        {
            var log = _templateLogRepository.GetById(logId);
            return log != null ? new TemplateManageModel(log) : new TemplateManageModel();
        }

        /// <summary>
        /// Get template by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TemplateRenderModel GetTemplateByName(string name)
        {
            var template = _templateRepository.FetchFirst(t => t.Name.Equals(name));
            if (template != null)
            {
                return new TemplateRenderModel(template);
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
            return template != null ? new TemplateManageModel(template) : new TemplateManageModel();
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

        /// <summary>
        /// Save template
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SaveTemplateManageModel(TemplateManageModel model)
        {
            ResponseModel response;
            var template = GetById(model.Id);
            if (template != null)
            {
                var log = new TemplateLogManageModel(template);
                template.Name = model.Name;
                template.Content = model.Content;

                response = Update(template);
                if (response.Success)
                {
                    _templateLogServices.SaveTemplateLog(log);
                }
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::Templates:::Messages:::UpdateSuccessfully:::Update template successfully.")
                    : _localizedResourceServices.T("AdminModule:::Templates:::Messages:::UpdateFailure:::Update template failed. Please try again later."));
            }
            Mapper.CreateMap<TemplateManageModel, Template>();
            template = Mapper.Map<TemplateManageModel, Template>(model);
            response = Insert(template);
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::Templates:::Messages:::CreateSuccessfully:::Create template successfully.")
                : _localizedResourceServices.T("AdminModule:::Templates:::Messages:::CreateFailure:::Create template failed. Please try again later."));
        }

        /// <summary>
        /// Delete template
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ResponseModel DeleteTemplate(int id)
        {
            var template = GetById(id);
            if (template != null)
            {
                if (template.IsDefaultTemplate)
                {
                    return new ResponseModel
                    {
                        Success = false,
                        Message = _localizedResourceServices.T("AdminModule:::Templates:::ValidationMessages:::DeleteDefaultTemplate:::Cannot delete default template.")
                    };
                }
                var response = Delete(template);
                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::Templates:::Messages:::DeleteSuccessfully:::Delete template successfully.")
                    : _localizedResourceServices.T("AdminModule:::Templates:::Messages:::DeleteFailure:::Delete template failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Templates:::Messages:::ObjectNotFounded:::Template is not founded.")
            };
        }

        #endregion

        #region Logs

        /// <summary>
        /// Get page log model
        /// </summary>
        /// <param name="id"></param>
        /// <param name="total"> </param>
        /// <param name="index"></param>
        /// <returns></returns>
        public TemplateLogListingModel GetLogs(int id, int total = 0, int index = 1)
        {
            var pageSize = _settingServices.GetSetting<int>(SettingNames.LogsPageSize);
            var template = GetById(id);
            if (template != null)
            {
                var logs = template.TemplateLogs.OrderByDescending(l => l.Created)
                    .GroupBy(l => l.SessionId)
                    .Skip((index - 1) * pageSize)
                    .Take(pageSize)
                    .ToList()
                    .Select(l => new TemplateLogsModel
                    {
                        SessionId = l.First().SessionId,
                        Creator = _userServices.GetUser(l.First().CreatedBy),
                        From = l.Last().Created,
                        To = l.First().Created,
                        Total = l.Count(),
                        Logs = l.Select(i => new TemplateLogItem(i)).ToList()
                    }).ToList();
                total = total + logs.Sum(l => l.Logs.Count);
                var model = new TemplateLogListingModel
                {
                    Id = template.Id,
                    Name = template.Name,
                    Total = total,
                    Logs = logs,
                    LoadComplete = total == template.TemplateLogs.Count
                };
                return model;
            }
            return null;
        }

        #endregion

        #region Razor Engine

        public TemplateServiceConfiguration GetConfig()
        {
            return new TemplateServiceConfiguration
            {
                BaseTemplateType = typeof(RazorEngineTemplateBase<>),
                EncodedStringFactory = new MvcHtmlStringFactory()
            };
        }

        /// <summary>
        /// Render template using Razor engine
        /// </summary>
        /// <param name="template"></param>
        /// <param name="model"></param>
        /// <param name="viewBag"> </param>
        /// <param name="cacheName"></param>
        /// <returns></returns>
        public string Parse(string template, dynamic model, DynamicViewBag viewBag = null, string cacheName = "")
        {
            var config = GetConfig();
            using (var templateService = new TemplateService(config))
            {
                return templateService.Parse(template, model, viewBag, cacheName);
            }
        }

        public void Compile(string template, Type type, string cacheName)
        {
            var config = GetConfig();
            var templateService = new TemplateService(config);
            templateService.Compile(template, type, cacheName);
        }

        /// <summary>
        /// Get cache template name
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="created"></param>
        /// <param name="updated"></param>
        /// <returns></returns>
        public static string GetTemplateCacheName(string templateName, DateTime created, DateTime? updated)
        {
            return string.Format("{0}-{1}", templateName,
                                 updated.HasValue
                                     ? updated.Value.ToString(Configurations.DateTimeFormat)
                                     : created.ToString(Configurations.DateTimeFormat));
        }

        #endregion

        #region Validation

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

        #endregion
    }
}
