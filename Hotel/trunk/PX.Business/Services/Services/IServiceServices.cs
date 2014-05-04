using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.Services;
using PX.Business.Models.Services.CurlyBrackets;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.Services
{
    public interface IServiceServices
    {
        #region Base

        IQueryable<Service> GetAll();
        IQueryable<Service> Fetch(Expression<Func<Service, bool>> expression);
        Service FetchFirst(Expression<Func<Service, bool>> expression);
        Service GetById(object id);
        ResponseModel Insert(Service service);
        ResponseModel Update(Service service);
        ResponseModel Delete(Service service);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchService(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageService(GridOperationEnums operation, ServiceModel model);

        #endregion

        #region Manage

        ServiceManageModel GetServiceManageModel(int? id = null);

        ResponseModel SaveServiceManageModel(ServiceManageModel model);

        #endregion

        IEnumerable<SelectListItem> GetStatus();

        bool IsTitleExisted(int? serviceId, string title);

        List<ServiceCurlyBracket> GetServices(int count);
    }
}
