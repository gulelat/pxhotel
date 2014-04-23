using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.PageLogs;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.PageLogs
{
    public interface IPageLogServices
    {
        #region Base

        IQueryable<PageLog> GetAll();
        IQueryable<PageLog> Fetch(Expression<Func<PageLog, bool>> expression);
        PageLog FetchFirst(Expression<Func<PageLog, bool>> expression);
        PageLog GetById(object id);
        ResponseModel Insert(PageLog pageLog);
        ResponseModel Update(PageLog pageLog);
        ResponseModel Delete(PageLog pageLog);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search

        JqGridSearchOut SearchPageLogs(JqSearchIn si);

        #endregion

        ResponseModel SavePageLog(PageLogManageModel model);
    }
}
