using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.TemplateLogs;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.TemplateLogs
{
    public interface ITemplateLogServices
    {
        #region Base

        IQueryable<TemplateLog> GetAll();
        IQueryable<TemplateLog> Fetch(Expression<Func<TemplateLog, bool>> expression);
        TemplateLog FetchFirst(Expression<Func<TemplateLog, bool>> expression);
        TemplateLog GetById(object id);
        ResponseModel Insert(TemplateLog templateLog);
        ResponseModel Update(TemplateLog templateLog);
        ResponseModel Delete(TemplateLog templateLog);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchTemplateLogs(JqSearchIn si);

        #endregion

        ResponseModel SaveTemplateLog(TemplateLogManageModel model);
    }
}
