﻿using System;
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
        RotatingImage FetchFirst(Expression<Func<RotatingImage, bool>> expression);
        RotatingImage GetById(object id);
        ResponseModel Insert(RotatingImage rotatingImage);
        ResponseModel Update(RotatingImage rotatingImage);
        ResponseModel Delete(RotatingImage rotatingImage);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchRotatingImages(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageRotatingImage(GridOperationEnums operation, RotatingImageModel model);

        #endregion

        RotatingImageManageModel GetRotatingImageManageModel(int? id = null);

        ResponseModel SaveRotatingImage(RotatingImageManageModel model);

        ResponseModel UpdateRotatingImageUrl(int id, string url);
    }
}
