using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using PX.Business.Models.Testimonials;
using PX.Business.Models.Testimonials.CurlyBrackets;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;

namespace PX.Business.Services.Testimonials
{
    public interface ITestimonialServices
    {
        #region Base

        IQueryable<Testimonial> GetAll();
        IQueryable<Testimonial> Fetch(Expression<Func<Testimonial, bool>> expression);
        Testimonial FetchFirst(Expression<Func<Testimonial, bool>> expression);
        Testimonial GetById(object id);
        ResponseModel Insert(Testimonial testimonial);
        ResponseModel Update(Testimonial testimonial);
        ResponseModel Delete(Testimonial testimonial);
        ResponseModel Delete(object id);

        #endregion

        #region Grid Search

        JqGridSearchOut SearchTestimonials(JqSearchIn si);

        #endregion

        #region Manage Grid

        ResponseModel ManageTestimonial(GridOperationEnums operation, TestimonialModel model);

        #endregion

        List<TestimonialCurlyBracket> GetRandom(int count);
    }
}
