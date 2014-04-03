using System;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.RotatingImages;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.RotatingImages
{
    public interface IRotatingImageServices
    {
        #region Base

        IQueryable<RotatingImage> GetAll();
        IQueryable<RotatingImage> Fetch(Expression<Func<RotatingImage, bool>> expression);
        RotatingImage GetById(object id);
        ResponseModel Insert(RotatingImage rotatingImage);
        ResponseModel Update(RotatingImage rotatingImage);
        ResponseModel Delete(RotatingImage rotatingImage);
        ResponseModel Delete(object id);

        #endregion

        ResponseModel ManageRotatingImage(GridOperationEnums operation, RotatingImageModel model);

        JqGridSearchOut SearchRotatingImages(JqSearchIn si);

        RotatingImageManageModel GetRotatingImageManageModel(int? id = null);

        ResponseModel SaveRotatingImage(RotatingImageManageModel model);
    }
}
