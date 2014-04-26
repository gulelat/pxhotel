using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.Languages;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.Languages
{
    public interface ILanguageServices
    {
        #region Base

        IQueryable<Language> GetAll();
        IQueryable<Language> Fetch(Expression<Func<Language, bool>> expression);
        Language FetchFirst(Expression<Func<Language, bool>> expression);
        Language GetById(object id);
        ResponseModel Insert(Language language);
        ResponseModel Update(Language language);
        ResponseModel Delete(Language language);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchLanguages(JqSearchIn si);
        
        #endregion

        #region Manage Grid
        ResponseModel ManageLanguage(GridOperationEnums operation, LanguageModel model);

        #endregion
    }
}
