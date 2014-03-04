using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PX.Business.Helpers
{
    public static class ImageHelpers
    {
        public static IHtmlString Image(this HtmlHelper helper,
                                        string url,
                                        string altText,
                                        object htmlAttributes)
        {
            url = url.Replace("~", string.Empty);
            var builder = new TagBuilder("img");
            builder.Attributes.Add("src", url);
            builder.Attributes.Add("alt", altText);
            builder.Attributes.Add("title", altText);
            builder.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return new HtmlString(builder.ToString(TagRenderMode.SelfClosing));
        }
    }
}
