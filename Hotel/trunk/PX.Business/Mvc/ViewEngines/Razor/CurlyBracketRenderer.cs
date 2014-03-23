using System;
using System.Collections.Generic;
using System.Data.Objects;
using System.Text;
using System.Web.Mvc;
using PX.Business.Mvc.ViewEngines.Razor.Display;

namespace PX.Business.Mvc.ViewEngines.Razor
{
    public class CurlyBracketRenderer
    {
        private const int MaxDepth = 5;
        public static string ResolveCurlyBracket(string content, Func<string, string> processor)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }
            var result = new StringBuilder(content);
            var positions = new Stack<int>();
            for (int i = 0; i < result.Length; i++)
            {
                if (result[i] == '{')
                {
                    positions.Push(i);
                }
                else if (result[i] == '}')
                {
                    if (positions.Count > 0)
                    {
                        int beginPos = positions.Pop();
                        if (positions.Count < MaxDepth && i > beginPos + 1)
                        {
                            var match = result.ToString(beginPos + 1, i - beginPos - 1);
                            try
                            {
                                var replacement = processor(match);
                                result.Remove(beginPos, i - beginPos + 1);
                                result.Insert(beginPos, replacement);
                                i = beginPos + replacement.Length - 1;
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                    }
                }
            }
            return result.ToString();
        }

        public static string ProcessZonesAndTokens<TModel>(string key,
            dynamic zones,
            HtmlHelper<TModel> htmlHelper,
            List<string> renderedZones,
            TokenResolver<TModel> tokenResolver)
        {
            var original = "{" + key + "}";

            // leave script context and style context for rendering later 
            // as we will need to render all scripts & styles from widgets also.
            if (key.Equals(LayoutView.ScriptContextKey) || key.Equals(LayoutView.StyleContextKey))
                return original;

            var keys = key.Split(new[] { "_" }, StringSplitOptions.RemoveEmptyEntries);

            if (keys.Length > 0)
            {
                switch (keys[0])
                {
                    case "Widget":
                        var contentKey = original.Replace("Widget_", string.Empty);

                        var widgetResolved = tokenResolver.Display(contentKey);

                        return widgetResolved.ToHtmlString();
                    case "Zones":
                        // assume the zonename is the first param
                        var zoneName = original.Replace("{Zones_", string.Empty).Replace("}", string.Empty);
                        // if zone had rendered => fallback
                        if (renderedZones.Contains(zoneName))
                            return original;

                        // if the zone is not available, then it may be a token widget => resolve it
                        if (zones[zoneName] == null)
                            break;

                        // render the zone's content first
                        var zoneOutPut = RenderZone(htmlHelper, zones[zoneName], tokenResolver).ToString();

                        // resolve the token in zoneOutPut if any
                        var output = tokenResolver.Display(zoneOutPut).ToString();

                        renderedZones.Add(zoneName);

                        return output;
                    default:
                        var tagoutput = tokenResolver.Display(original).ToString();

                        return !string.IsNullOrEmpty(tagoutput) ? tagoutput : original;
                }
            }
            return string.Empty;
        }

        public static MvcHtmlString RenderZone<TModel>(HtmlHelper<TModel> htmlHelper, dynamic model, TokenResolver<TModel> tokenResolver)
        {
            var htmlString = string.Empty;

            foreach (var item in model)
            {
                if (item is string)
                    htmlString += item.ToString();
                else
                {
                    htmlString += Display(htmlHelper, item, tokenResolver).ToString();
                }
            }
            return new MvcHtmlString(htmlString);
        }

        public static MvcHtmlString Display<TModel>(HtmlHelper<TModel> htmlHelper, dynamic model, TokenResolver<TModel> tokenResolver)
        {
            var typeOfModel = ObjectContext.GetObjectType(model.GetType());

            var baseTypeName = typeOfModel.BaseType != null ? typeOfModel.BaseType.Name : "";

            var className = typeOfModel.IsSubclassOf(typeof(Widget))
                                ? string.Format("widget widget-{0} {1}", baseTypeName.ToLower(), ((Widget)model).CustomCssClasses)
                                : "";

            var html = tokenResolver.Display(model).ToString();

            if (!string.IsNullOrEmpty(className))
            {
                var tag = new TagBuilder("div");

                tag.AddCssClass(className);
                tag.InnerHtml = html;

                return new MvcHtmlString(tag.ToString(TagRenderMode.Normal));
            }

            return new MvcHtmlString(html);

        }
    }
}
