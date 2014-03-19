using System.Linq;
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
        Language GetById(object id);
        ResponseModel Insert(Language language);
        ResponseModel Update(Language language);
        ResponseModel Delete(Language language);
        ResponseModel Delete(object id);

        #endregion

        ResponseModel ManageLanguage(GridOperationEnums operation, LanguageModel model);

        JqGridSearchOut SearchLanguages(JqSearchIn si);
    }
}
