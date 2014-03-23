using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using PX.Business.Mvc.Environments;

namespace PX.Business.Mvc.ViewEngines.Razor.Display
{
    /// <summary>
    ///     Content rendering service that support rendering widget inside the content item
    /// </summary>
    public class TokenResolver<TModel>
    {
        private HtmlHelper<TModel> _helper;

        public TokenResolver(HtmlHelper<TModel> helper)
        {
            _helper = helper;
        }

        public void SetHelper(HtmlHelper<TModel> helper)
        {
            _helper = helper;
        }

        /// <summary>
        ///     Display a model with request to render widgets inside
        /// </summary>
        /// <param name="model">Model that requests rendering widgets inside</param>
        /// <returns>IHtmlString of the rendered model</returns>
        public IHtmlString Display(dynamic model)
        {
            string contentHtml = (model.GetType() == typeof (HtmlStringWriter) || model is string)
                                     ? model.ToString()
                                     : _helper.DisplayFor(m => model).ToHtmlString();

            //contentHtml = Regex.Replace(contentHtml, "\\{([^}]+)}", ProcessWidgetRenderingToken);
            contentHtml = CurlyBracketRenderer.ResolveCurlyBracket(contentHtml, ProcessWidgetRenderingToken);

            return new MvcHtmlString(contentHtml);
        }

        private string ProcessWidgetRenderingToken(string widgetRenderingParamsString)
        {
            // function key is the first parameter in the rendering param from {} tags
            var widgetRenderingParams = widgetRenderingParamsString.Split(new[] {"_"},
                                                                          StringSplitOptions.RemoveEmptyEntries);

            var functionKey = widgetRenderingParams.FirstOrDefault();
            if (functionKey == null)
            {
                return "{" + widgetRenderingParamsString + "}";
            }
            IWidgetResolver instance;

            if (TryResolve(functionKey, out instance))
            {
                try
                {
                    // render the widget in html string with found resolver
                    var widgetRenderedHtml = instance.RenderWidget(_helper, widgetRenderingParams)
                        .ToHtmlString();

                    return !string.IsNullOrEmpty(widgetRenderedHtml)
                               ? widgetRenderedHtml
                               : string.Empty;
                }
                catch (Exception ex)
                {
                    //TODO check why repeately resolve the same curly bracket
                    return "{!" + widgetRenderingParamsString + "!Error(" + ex.Message + ")}";
                }
            }

            // If there is no resolver found, then return back the widgetRenderingParams
            return "{" + widgetRenderingParamsString + "}";
        }

        private static bool TryResolve<T>(string functionKey, out T instance)
        {
            //Resolve token case-insensitive
            functionKey = functionKey.ToLowerInvariant();
            var key = new KeyedService(functionKey, typeof (T));
            object value;
            if (HostContainer.TryResolveService(key, out value))
            {
                instance = (T) value;
                return true;
            }
            instance = default(T);
            return false;
        }
    }
}
