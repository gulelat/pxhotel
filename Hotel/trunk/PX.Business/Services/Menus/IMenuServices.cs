using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PX.Business.Models.MenuModels;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Models.DTO;

namespace PX.Business.Services.Menus
{
    public interface IMenuServices
    {
        #region Base

        IQueryable<Menu> GetAll();
        Menu GetById(int? id);
        ResponseModel Insert(Menu menu);
        ResponseModel Update(Menu menu);
        ResponseModel Delete(Menu menu);
        ResponseModel Delete(int id);

        #endregion

        ResponseModel ManageMenu(GridOperationEnums operation, MenuModel model);

        JqGridSearchOut SearchMenus(JqSearchIn si);

        IEnumerable<SelectListItem> GetPossibleParents(int? id);

        BreadCrumbModel GetBreadCrumbs(string controller, string action);

        List<Menu> GetMenus();
    }
}
