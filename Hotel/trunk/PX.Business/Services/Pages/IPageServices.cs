using System.Collections.Generic;
using System.Linq;
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
        Page GetById(object id);
        ResponseModel Insert(Page page);
        ResponseModel Update(Page page);
        ResponseModel Delete(Page page);
        ResponseModel Delete(object id);

        #endregion

        Page GetPage(string friendlyUrl);

        ResponseModel ManagePage(GridOperationEnums operation, PageModel model);

        JqGridSearchOut SearchPages(JqSearchIn si);

        IEnumerable<SelectListItem> GetPossibleParents(int? id = null);

        IEnumerable<SelectListItem> GetStatus();

        List<Page> GetPages(int? parentId = null);

        PageRenderModel RenderContent(string url);

        PageManageModel GetPageManageModel(int? id = null);

        ResponseModel SavePageManageModel(PageManageModel model);
    }
}
