using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.RotatingImageGroups;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.RotatingImageGroups
{
    public interface IRotatingImageGroupServices
    {
        #region Base

        IQueryable<RotatingImageGroup> GetAll();
        IQueryable<RotatingImageGroup> Fetch(Expression<Func<RotatingImageGroup, bool>> expression);
        RotatingImageGroup GetById(object id);
        ResponseModel Insert(RotatingImageGroup rotatingImageGroup);
        ResponseModel Update(RotatingImageGroup rotatingImageGroup);
        ResponseModel Delete(RotatingImageGroup rotatingImageGroup);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchRotatingImageGroups(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageRotatingImageGroup(GridOperationEnums operation, RotatingImageGroupModel model);

        #endregion
        
        IEnumerable<SelectListItem> GetRotatingImageGroups();

        ResponseModel SaveGroupSettings(GroupManageSettingModel model);

        GroupManageSettingModel GetGroupManageSettingModel(int id);

        GroupGalleryModel GetGroupGallery(int id);

        ResponseModel SortImages(GroupImageSortingModel model);
    }
}
