using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.SQLTool;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.SQLTool;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Environments;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class SQLToolController : AdminController
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

        public ActionResult Histories()
        {
            return View();
        }

        public string _AjaxHistory(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_sqlCommandServices.SearchCommands(si));
        }

        public JsonResult GenerateSelectStatement(string tablename)
        {
            var executor = new SQLExecutor();
            return Json(executor.GenerateSelectCommand(tablename));
        }

        #region Methods
        public JsonResult GenerateInsertStatement(string tablename)
        {
            var executor = new SQLExecutor();
            return Json(executor.GenerateInsertCommand(tablename));
        }

        public JsonResult GenerateUpdateStatement(string tablename)
        {
            var executor = new SQLExecutor();
            return Json(executor.GenerateUpdateCommand(tablename));
        }

        public JsonResult GenerateDeleteStatement(string tablename)
        {
            var executor = new SQLExecutor();
            return Json(executor.GenerateDeleteCommand(tablename));
        }

        public JsonResult GenerateCreateStatement(string tablename)
        {
            var executor = new SQLExecutor();
            return Json(executor.GenerateCreateCommand(tablename));
        }

        public JsonResult GenerateAlterStatement(string tablename)
        {
            var executor = new SQLExecutor();
            return Json(executor.GenerateAlterCommand(tablename));
        }

        #endregion
    }
}
