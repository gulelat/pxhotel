using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PX.Business.Models.CurlyBrackets;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Business.Services.CurlyBrackets
{
    public interface ICurlyBracketServices
    {
        IEnumerable<CurlyBracketModel> GetAllCurlyBracketsOfApplication();

        JqGridSearchOut SearchCurlyBrackets(JqSearchIn si);

        string Render(string content);

        bool IsPageTemplateValid(string content);

        IEnumerable<SelectListItem> GetCurlyBracketSelectListFromObject(Type type);

        IEnumerable<SelectListItem> GetCurlyBracketSelectListFromObject(string type);
    }
}
