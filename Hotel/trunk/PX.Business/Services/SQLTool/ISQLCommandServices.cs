using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using PX.Business.Models.SQLTool;
using PX.Core.Framework.Mvc.Models;
using PX.EntityModel;

namespace PX.Business.Services.SQLTool
{
    public interface ISQLCommandServices
    {
        #region Base

        IQueryable<SQLCommandHistory> GetAll();
        SQLCommandHistory GetById(object id);
        ResponseModel Insert(SQLCommandHistory sqlCommandHistory);
        ResponseModel Update(SQLCommandHistory sqlCommandHistory);
        ResponseModel Delete(SQLCommandHistory sqlCommandHistory);
        ResponseModel Delete(object id);

        #endregion

        ResponseModel SaveCommand(SQLRequest request);

        IEnumerable<SQLCommandHistory> GetHistories();

        IEnumerable<CommandHistory> GetHistories(int index, int pageSize);

        DbConnection GetConnection();

        string GetConnectionString();
    }
}
