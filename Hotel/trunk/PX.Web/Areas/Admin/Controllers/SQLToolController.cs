using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.EntityClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PX.Business.Models.SQLTool;
using PX.Business.Services.SQLTool;
using PX.Core.Framework.Mvc.Environments;

namespace PX.Web.Areas.Admin.Controllers
{
    public class SQLToolController : Controller
    {

        private readonly ISQLCommandServices _sqlCommandServices;

        public SQLToolController()
        {
            _sqlCommandServices = HostContainer.GetInstance<ISQLCommandServices>();
        }
        public ActionResult Index()
        {
            var executor = new SQLExecutor();
            var model = new SQLResult
            {
                ConnectionString = _sqlCommandServices.GetConnectionString(),
                History = _sqlCommandServices.GetHistories(SQLExecutor.DefaultHistoryStart,
                                                        SQLExecutor.DefaultHistoryLength),
                ReadOnly = true,
                Tables = executor.GetTableNames()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(SQLRequest request, bool exportToFile = false)
        {
            var executor = new SQLExecutor();
            var result = executor.Execute(request);
            if (exportToFile)
            {
                return new FileContentResult(Encoding.UTF8.GetBytes(result.ToString()), "text/plain")
                {
                    FileDownloadName = "QueryResult.txt"
                };
            }
            return View(result);
        }

        public ActionResult History(bool inFancybox = false)
        {
            return View(new HistoryViewModel { InFancybox = inFancybox });
        }

        public JsonResult AjaxHistory(DataTablesParamModel param)
        {
            var result = new DataTablesDataResult { sEcho = param.sEcho };

            var histories = _sqlCommandServices.GetHistories();
            result.iTotalRecords = histories.Count();
            result.iTotalDisplayRecords = result.iTotalRecords;

            var data = histories.Skip(param.iDisplayStart)
                                .Take(param.iDisplayLength)
                                .ToArray();
            result.aaData = data;
            return Json(result);
        }

        public ActionResult GenerateSelectStatement(string tablename)
        {
            var executor = new SQLExecutor();
            return Json(executor.GenerateSelectCommand(tablename));
        }

        public ActionResult GenerateInsertStatement(string tablename)
        {
            var executor = new SQLExecutor();
            return Json(executor.GenerateInsertCommand(tablename));
        }

        public ActionResult GenerateUpdateStatement(string tablename)
        {
            var executor = new SQLExecutor();
            return Json(executor.GenerateUpdateCommand(tablename));
        }

        public ActionResult GenerateDeleteStatement(string tablename)
        {
            var executor = new SQLExecutor();
            return Json(executor.GenerateDeleteCommand(tablename));
        }

        public ActionResult GenerateCreateStatement(string tablename)
        {
            var executor = new SQLExecutor();
            return Json(executor.GenerateCreateCommand(tablename));
        }

        public ActionResult GenerateAlterStatement(string tablename)
        {
            var executor = new SQLExecutor();
            return Json(executor.GenerateAlterCommand(tablename));
        }
    }
}
