using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.PageAudits;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.PageAudits
{
    public interface IPageAuditServices
    {
        #region Base

        IQueryable<PageAudit> GetAll();
        IQueryable<PageAudit> Fetch(Expression<Func<PageAudit, bool>> expression);
        PageAudit FetchFirst(Expression<Func<PageAudit, bool>> expression);
        PageAudit GetById(object id);
        ResponseModel Insert(PageAudit pageAudit);
        ResponseModel Update(PageAudit pageAudit);
        ResponseModel Delete(PageAudit pageAudit);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search

        JqGridSearchOut SearchPageAudits(JqSearchIn si);

        #endregion

        ResponseModel SaveAuditPage(PageAuditViewModel pageAudit);
    }
}
