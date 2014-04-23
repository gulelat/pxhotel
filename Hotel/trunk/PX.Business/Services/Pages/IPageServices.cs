using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.Pages;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.Pages
{
    public interface IPageServices
    {
        #region Base

        IQueryable<Page> GetAll();
        IQueryable<Page> Fetch(Expression<Func<Page, bool>> expression);
        Page GetById(object id);
        ResponseModel Insert(Page page);
        ResponseModel Update(Page page);
        ResponseModel Delete(Page page);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchPages(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManagePage(GridOperationEnums operation, PageModel model);

        #endregion

        #region Manage

        PageManageModel GetPageManageModel(int? id = null);

        PageManageModel GetPageManageModelByLogId(int? id = null);

        ResponseModel SavePageManageModel(PageManageModel model);
        #endregion

        PageRenderModel RenderContent(string url);

        #region Select List
        IEnumerable<SelectListItem> GetPossibleParents(int? id = null);

        IEnumerable<SelectListItem> GetStatus();

        IEnumerable<SelectListItem> GetRelativePages(int? pageId = null, int? parentId = null);

        IEnumerable<SelectListItem> GetRelativePages(out int position, out int relativePageId, int? pageId = null, int? parentId = null);

        IEnumerable<SelectListItem> GetPageTags(int? pageId = null);

        #endregion

        #region Logs

        PageLogsModel GetLogs(int id);
        #endregion

        Page GetPage(string friendlyUrl);

        ResponseModel ChangeHomePage(int id);

        List<Page> GetPages(int? parentId = null);

        bool IsTitleExisted(int? pageId, string title);

        bool IsFriendlyUrlExisted(int? pageId, string friendlyUrl);
    }
}
