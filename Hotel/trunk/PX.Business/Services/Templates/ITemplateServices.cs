using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.Templates;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using RazorEngine.Configuration;
using RazorEngine.Templating;

namespace PX.Business.Services.Templates
{
    public interface ITemplateServices
    {
        #region Base

        IQueryable<Template> GetAll();
        Template GetById(object id);
        IQueryable<Template> Fetch(Expression<Func<Template, bool>> expression);
        Template FetchFirst(Expression<Func<Template, bool>> expression);
        ResponseModel Insert(Template template);
        ResponseModel Update(Template template);
        ResponseModel Delete(Template template);
        ResponseModel Delete(object id);

        #endregion

        #region Initialize

        void InitializeTemplates();
        #endregion

        #region Grid Search
        JqGridSearchOut SearchTemplates(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageTemplate(GridOperationEnums operation, TemplateModel model);

        #endregion

        #region Manage

        TemplateManageModel GetTemplateManageModelByLogId(int? logId);

        TemplateManageModel GetTemplateManageModel(int? id = null);

        TemplateManageModel GetTemplateManageModel(string type);

        TemplateRenderModel GetTemplateByName(string name);

        ResponseModel SaveTemplateManageModel(TemplateManageModel model);

        ResponseModel DeleteTemplate(int id);

        #endregion

        #region Logs

        TemplateLogsModel GetLogs(int id, int total = 0, int index = 1);
        #endregion

        #region Razor Engine

        TemplateServiceConfiguration GetConfig();

        string Parse(string template, dynamic model, DynamicViewBag viewBag = null, string cacheName = "");

        void Compile(string template, Type type, string cacheName);
        #endregion

        bool IsTemplateNameExisted(int? templateId, string name);
    }
}
