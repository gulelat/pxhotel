using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using RazorEngine.Templating;
using RazorEngine.Text;

namespace PX.Business.Mvc.ViewEngines.Razor.RazorEngine
{
    public abstract class MvcTemplateBase<T> : TemplateBase<T>
    {
        public TemplateWriter RenderPart<T>(string path, T model = default(T))
        {
            return Include(path, model);
        }

        private HtmlHelper _html;
        public HtmlHelper Html
        {
            get
            {
                if (this._html == null)
                {
                    var container = new InternalViewDataContainer<T>(this.Model);
                    var context = new ViewContext(
                        new ControllerContext(),
                        new InternalView(),
                        new ViewDataDictionary(),
                        new TempDataDictionary(),
                        new System.IO.StringWriter(new System.Text.StringBuilder()));
                    this._html = new HtmlHelper<T>(context, container);
                }
                return this._html;
            }
        }

        public override void WriteTo(TextWriter writer, object value)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            if (value == null) return;

            var encodedString = value as IEncodedString;
            if (encodedString != null)
            {
                writer.Write(encodedString);
            }
            else
            {
                var htmlString = value as IHtmlString;
                if (htmlString != null)
                    writer.Write(htmlString.ToHtmlString());
                else
                {
                    //This was the base template's implementation:
                    encodedString = TemplateService.EncodedStringFactory.CreateEncodedString(value);
                    writer.Write(encodedString);
                }
            }
        }

        private class InternalView : IView
        {
            public void Render(ViewContext context, System.IO.TextWriter writer) { }
        }

        private class InternalViewDataContainer<T> : IViewDataContainer
        {
            public InternalViewDataContainer(T model)
            {
                ViewData = new ViewDataDictionary<T>(model);
            }
            public ViewDataDictionary ViewData { get; set; }
        }
    }
}
