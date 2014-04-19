using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Models.Testimonials;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.Controllers;
using PX.Business.Services.Testimonials;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    [PxAuthorize(Permissions = new[] { PermissionEnums.ManageContent })]
    public class TestimonialsController : AdminController
    {
        private readonly ITestimonialServices _testimonialServices;
        public TestimonialsController(ITestimonialServices testimonialServices)
        {
            _testimonialServices = testimonialServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_testimonialServices.SearchTestimonials(si));
        }

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(TestimonialModel model, GridManagingModel manageModel)
        {
            return Json(_testimonialServices.ManageTestimonial(manageModel.Operation, model));
        }
    }
}
