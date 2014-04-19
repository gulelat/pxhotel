﻿using System.Collections.Generic;
using System.Linq;
using PX.Business.Models.Tags;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.Tags
{
    public interface ITagServices
    {
        #region Base

        IQueryable<Tag> GetAll();
        Tag GetById(object id);
        ResponseModel Insert(Tag tag);
        ResponseModel Update(Tag tag);
        ResponseModel Delete(Tag tag);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchTags(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageTag(GridOperationEnums operation, TagModel model);

        #endregion
    }
}
