using System.Collections.Generic;
using System.Linq;
using PX.Business.Models.Testimonials;
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

        List<TestimonialModel> GetRandom(int count);
    }
}
