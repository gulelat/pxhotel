using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.FileTemplates;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.FileTemplates
{
    public interface IFileTemplateServices
    {
        #region Initialize

        void InitializeFileTemplates();
        #endregion

        #region Base

        IQueryable<FileTemplate> GetAll();
        IQueryable<FileTemplate> Fetch(Expression<Func<FileTemplate, bool>> expression);
        FileTemplate FetchFirst(Expression<Func<FileTemplate, bool>> expression);
        FileTemplate GetById(object id);
        ResponseModel Insert(FileTemplate fileTemplate);
        ResponseModel Update(FileTemplate fileTemplate);
        ResponseModel HierarchyInsert(FileTemplate fileTemplate);
        ResponseModel HierarchyUpdate(FileTemplate fileTemplate);
        ResponseModel Delete(FileTemplate fileTemplate);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Manage

        ResponseModel ManageFileTemplate(GridOperationEnums operation, FileTemplateModel model);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchFileTemplates(JqSearchIn si);

        #endregion

        IEnumerable<SelectListItem> GetPossibleParents(int? id = null);

        IEnumerable<SelectListItem> GetFileTemplateSelectList(int? id = null);

        string GetFileTemplateMaster(string controller, string action);

        List<FileTemplate> GetFileTemplates(int? parentId = null);

        FileTemplateManageModel GetTemplateManageModel(int? id = null);

        ResponseModel SaveFileTemplate(FileTemplateManageModel model);

        FileTemplate FindTemplate(string filePath);

        bool IsFileTemplateNameExisted(int? fileTemplateId, string name);

        bool IsFileTemplateExisted(string filePath);
    }
}
