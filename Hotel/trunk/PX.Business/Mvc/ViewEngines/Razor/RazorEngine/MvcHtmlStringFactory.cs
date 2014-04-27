using System.Web.Mvc;
using RazorEngine.Text;

namespace PX.Business.Mvc.ViewEngines.Razor.RazorEngine
{
    public class MvcHtmlStringFactory : IEncodedStringFactory
    {
        public IEncodedString CreateEncodedString(string rawString)
        {
            return new HtmlEncodedString(rawString);
        }

        public IEncodedString CreateEncodedString(object obj)
        {
            if (obj == null)
                return new HtmlEncodedString(string.Empty);

            var htmlString = obj as HtmlEncodedString;
            if (htmlString != null)
                return htmlString;

            var mvcHtmlString = obj as MvcHtmlString;
            if (mvcHtmlString != null)
                return new MvcHtmlStringWrapper(mvcHtmlString);

            return new HtmlEncodedString(obj.ToString());
        }

        public class MvcHtmlStringWrapper : IEncodedString
        {
            private readonly MvcHtmlString _value;

            public MvcHtmlStringWrapper(MvcHtmlString value)
            {
                _value = value;
            }

            public string ToEncodedString()
            {
                return _value.ToString();
            }

            public override string ToString()
            {
                return ToEncodedString();
            }
        }
    }
}
