using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.PageTemplates;
using PX.Business.Models.Pages;
using PX.Business.Models.Pages.ViewModels;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.PageTemplates
{
    public interface IPageTemplateServices
    {
        #region Initialize

        void InitializeFileTemplates();
        #endregion

        #region Base

        IQueryable<PageTemplate> GetAll();
        IQueryable<PageTemplate> Fetch(Expression<Func<PageTemplate, bool>> expression);
        PageTemplate FetchFirst(Expression<Func<PageTemplate, bool>> expression);
        PageTemplate GetById(object id);
        ResponseModel Insert(PageTemplate pageTemplate);
        ResponseModel Update(PageTemplate pageTemplate);
        ResponseModel Delete(PageTemplate pageTemplate);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchPageTemplates(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManagePageTemplate(GridOperationEnums operation, PageTemplateModel model);

        #endregion

        #region Manage

        PageTemplateManageModel GetTemplateManageModel(int? id = null);

        PageTemplateManageModel GetTemplateManageModelByLogId(int? logId = null);

        ResponseModel SavePageTemplate(PageTemplateManageModel model);

        #endregion

        #region Logs

        PageTemplateLogsModel GetLogs(int id, int index = 1);
        #endregion

        PageTemplate FindTemplate(string filePath);

        string RenderPageTemplate(int? pageTemplateId, PageRenderModel model);

        IEnumerable<SelectListItem> GetPossibleParents(int? id = null);

        IEnumerable<SelectListItem> GetPageTemplateSelectList(int? id = null);

        IEnumerable<SelectListItem> GetPageTemplateSelectListForFileTemplate(int? id = null);

        List<PageTemplate> GetPageTemplates(int? parentId = null);

        bool IsPageTemplateNameExisted(int? pageTemplateId, string name);

        bool IsPageTemplateExisted(string filePath);
    }
}
