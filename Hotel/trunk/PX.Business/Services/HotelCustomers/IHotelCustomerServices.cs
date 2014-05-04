using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.HotelCustomers;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.HotelCustomers
{
    public interface IHotelCustomerServices
    {
        #region Base

        IQueryable<HotelCustomer> GetAll();
        IQueryable<HotelCustomer> Fetch(Expression<Func<HotelCustomer, bool>> expression);
        HotelCustomer FetchFirst(Expression<Func<HotelCustomer, bool>> expression);
        HotelCustomer GetById(object id);
        ResponseModel Insert(HotelCustomer hotelCustomer);
        ResponseModel Update(HotelCustomer hotelCustomer);
        ResponseModel Delete(HotelCustomer hotelCustomer);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search
        JqGridSearchOut SearchHotelCustomers(JqSearchIn si);

        #endregion

        #region Manage Grid
        ResponseModel ManageHotelCustomer(GridOperationEnums operation, HotelCustomerModel model);

        #endregion
    }
}
