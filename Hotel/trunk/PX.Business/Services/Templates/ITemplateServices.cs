using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.Templates;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

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
        TemplateManageModel GetTemplateManageModel(int? id = null);

        TemplateManageModel GetTemplateManageModel(string type);

        TemplateRenderModel GetTemplateByName(string name);

        ResponseModel SaveTemplateManageModel(TemplateManageModel model);

        ResponseModel DeleteTemplate(int id);

        #endregion

        #region Logs

        TemplateLogsModel GetLogs(int id, int index = 1);
        #endregion

        string RenderTemplate(string template, dynamic model, string cacheName = "");

        bool IsTemplateNameExisted(int? templateId, string name);
    }
}
