using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.EmailAccounts;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.EmailAccounts
{
    public interface IEmailAccountServices
    {
        #region Base

        IQueryable<EmailAccount> GetAll();
        IQueryable<EmailAccount> Fetch(Expression<Func<EmailAccount, bool>> expression);
        EmailAccount FetchFirst(Expression<Func<EmailAccount, bool>> expression);
        EmailAccount GetById(object id);
        ResponseModel Insert(EmailAccount emailAccount);
        ResponseModel Update(EmailAccount emailAccount);
        ResponseModel Delete(EmailAccount emailAccount);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchEmailAccounts(JqSearchIn si);

        #endregion

        #region Manage

        EmailAccountManageModel GetEmailAccountManageModel(int? id = null);

        ResponseModel SaveEmailAccount(EmailAccountManageModel model);

        #endregion

        ResponseModel MarkAsDefault(int id);

        ResponseModel SendTestEmail(TestEmailModel model);
    }
}
