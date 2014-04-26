using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.ClientMenus;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.ClientMenus
{
    public interface IClientMenuServices
    {
        #region Base

        IQueryable<ClientMenu> GetAll();
        IQueryable<ClientMenu> Fetch(Expression<Func<ClientMenu, bool>> expression);
        ClientMenu GetById(object id);
        ResponseModel Insert(ClientMenu clientMenu);
        ResponseModel Update(ClientMenu clientMenu);
        ResponseModel Delete(ClientMenu clientMenu);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search

        JqGridSearchOut SearchClientMenus(JqSearchIn si);

        #endregion

        #region Grid Manage

        ResponseModel ManageClientMenu(GridOperationEnums operation, ClientMenuModel model);

        #endregion

        #region Manage

        ClientMenuManageModel GetClientMenuManageModel(int? id = null);

        ResponseModel SaveClientMenuManageModel(ClientMenuManageModel model);

        ResponseModel SavePageToClientMenu(int pageId);

        IEnumerable<SelectListItem> GetRelativeMenus(out int position, out int relativeClientMenuId,
                                                     int? clientMenuId = null, int? parentId = null);

        IEnumerable<SelectListItem> GetRelativeMenus(int? menuId = null, int? parentId = null);
        #endregion

        bool IsMenuNameExisted(int? id, string name);

        IEnumerable<SelectListItem> GetPossibleParents(int? id = null);

        List<ClientMenu> GetClientMenus(int? parentId = null);

        ClientBreadCrumbModel GetBreadCrumbs(string url);
    }
}
