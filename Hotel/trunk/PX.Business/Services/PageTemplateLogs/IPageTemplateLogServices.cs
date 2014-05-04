using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.PageTemplateLogs;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.PageTemplateLogs
{
    public interface IPageTemplateLogServices
    {
        #region Base

        IQueryable<PageTemplateLog> GetAll();
        IQueryable<PageTemplateLog> Fetch(Expression<Func<PageTemplateLog, bool>> expression);
        PageTemplateLog FetchFirst(Expression<Func<PageTemplateLog, bool>> expression);
        PageTemplateLog GetById(object id);
        ResponseModel Insert(PageTemplateLog pageTemplateLog);
        ResponseModel Update(PageTemplateLog pageTemplateLog);
        ResponseModel Delete(PageTemplateLog pageTemplateLog);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchPageTemplateLogs(JqSearchIn si);

        #endregion

        ResponseModel SavePageTemplateLog(PageTemplateLogManageModel model);
    }
}
