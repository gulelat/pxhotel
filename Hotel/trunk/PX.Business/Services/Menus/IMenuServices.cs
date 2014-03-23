using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PX.Business.Models.Menus;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.Menus
{
    public interface IMenuServices
    {
        #region Initialize

        void InitializeMenuPermissions();
        #endregion

        #region Base

        IQueryable<Menu> GetAll();
        Menu GetById(object id);
        ResponseModel Insert(Menu menu);
        ResponseModel Update(Menu menu);
        ResponseModel Delete(Menu menu);
        ResponseModel Delete(object id);

        #endregion

        ResponseModel ManageMenu(GridOperationEnums operation, MenuModel model);

        JqGridSearchOut SearchMenus(JqSearchIn si);

        IEnumerable<SelectListItem> GetPossibleParents(int? id);

        List<Menu> GetMenus(int? parentId = null);

        BreadCrumbModel GetBreadCrumbs(string controller, string action);
    }
}
