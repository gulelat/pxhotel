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
        public TemplateWriter RenderPart<TModel>(string path, TModel model = default(TModel))
        {
            return Include(path, model);
        }

        private HtmlHelper _html;
        public HtmlHelper Html
        {
            get
            {
                if (_html == null)
                {
                    var container = new InternalViewDataContainer<T>(Model);
                    var context = new ViewContext(
                        new ControllerContext(),
                        new InternalView(),
                        new ViewDataDictionary(),
                        new TempDataDictionary(),
                        new StringWriter(new System.Text.StringBuilder()));
                    _html = new HtmlHelper<T>(context, container);
                }
                return _html;
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
            public void Render(ViewContext context, TextWriter writer) { }
        }

        private class InternalViewDataContainer<TModel> : IViewDataContainer
        {
            public InternalViewDataContainer(TModel model)
            {
                ViewData = new ViewDataDictionary<TModel>(model);
            }
            public ViewDataDictionary ViewData { get; set; }
        }
    }
}
