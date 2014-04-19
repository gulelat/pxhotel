using System.Linq;
using PX.Business.Models.Templates;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.Templates
{
    public interface ITemplateServices
    {
        #region Base

        IQueryable<Template> GetAll();
        Template GetById(object id);
        ResponseModel Insert(Template template);
        ResponseModel Update(Template template);
        ResponseModel Delete(Template template);
        ResponseModel Delete(object id);

        #endregion

        #region Initialize

        void InitializeTemplates();
        #endregion

        #region Grid Search
        JqGridSearchOut SearchTemplates(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageTemplate(GridOperationEnums operation, TemplateModel model);

        #endregion

        #region Manage
        TemplateManageModel GetTemplateManageModel(int? id = null);

        TemplateManageModel GetTemplateManageModel(string type);

        TemplateManageModel GetTemplateByName(string name);

        ResponseModel SaveTemplate(TemplateManageModel model);

        #endregion

        string RenderTemplate(string template, dynamic model, string cacheName = "");

        bool IsTemplateNameExisted(int? templateId, string name);
    }
}
