using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PX.Business.Models.PageTemplates;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.PageTemplates
{
    public interface IPageTemplateServices
    {
        #region Base

        IQueryable<PageTemplate> GetAll();
        PageTemplate GetById(object id);
        ResponseModel Insert(PageTemplate pageTemplate);
        ResponseModel Update(PageTemplate pageTemplate);
        ResponseModel Delete(PageTemplate pageTemplate);
        ResponseModel Delete(object id);

        #endregion

        ResponseModel ManagePageTemplate(GridOperationEnums operation, PageTemplateModel model);

        JqGridSearchOut SearchPageTemplates(JqSearchIn si);

        IEnumerable<SelectListItem> GetPossibleParents(int? id = null);

        List<PageTemplate> GetPageTemplates(int? parentId = null);

        IEnumerable<SelectListItem> GetPageTemplateSelectList(int? id = null);

        PageTemplateManageModel GetTemplateManageModel(int? id = null);

        ResponseModel SaveTemplates(PageTemplateManageModel model);
    }
}
