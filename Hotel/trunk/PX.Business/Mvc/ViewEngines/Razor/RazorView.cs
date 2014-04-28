using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace PX.Business.Mvc.ViewEngines.Razor
{
    public class RazorView : System.Web.Mvc.RazorView
    {
        public RazorView(ControllerContext controllerContext, string viewPath, string layoutPath, bool runViewStartPages, IEnumerable<string> viewStartFileExtensions, IViewPageActivator viewPageActivator)
            : base(controllerContext, viewPath, layoutPath, runViewStartPages, viewStartFileExtensions, viewPageActivator)
        {

        }

        protected override void RenderView(ViewContext viewContext, TextWriter writer, object instance)
        {
            base.RenderView(viewContext, writer, instance);
        }
    }
}
