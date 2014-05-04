using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.SQLTool;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.SQLTool
{
    public interface ISQLCommandServices
    {
        #region Base

        IQueryable<SQLCommandHistory> GetAll();
        IQueryable<SQLCommandHistory> Fetch(Expression<Func<SQLCommandHistory, bool>> expression);
        SQLCommandHistory FetchFirst(Expression<Func<SQLCommandHistory, bool>> expression);
        SQLCommandHistory GetById(object id);
        ResponseModel Insert(SQLCommandHistory sqlCommandHistory);
        ResponseModel Update(SQLCommandHistory sqlCommandHistory);
        ResponseModel Delete(SQLCommandHistory sqlCommandHistory);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search

        JqGridSearchOut SearchCommands(JqSearchIn si);

        #endregion

        #region Grid Manage

        ResponseModel ManageSQLCommandHistory(GridOperationEnums operation, SQLCommandHistoryModel model);
        #endregion

        ResponseModel SaveCommand(SQLRequest request);

        IEnumerable<SQLCommandHistoryModel> GetHistories(int? index = null, int? pageSize = null);

        DbConnection GetConnection();

        string GetConnectionString();
    }
}
