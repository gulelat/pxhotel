﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using PX.Business.Mvc.Attributes;
using PX.Business.Mvc.WorkContext;
using PX.Core.Logging;
using PX.Core.Ultilities;

namespace PX.Business.Services.CurlyBrackets.CurlyBracketResolver
{
    public class CurlyBracketRenderer
    {
        private static readonly ILogger Logger = new Logger(MethodBase.GetCurrentMethod().DeclaringType);

        private const int MaxDepth = 5;

        public static string ResolverContent(string content, int maxLoop = 5)
        {
            bool hasCurlyBrackets = true;
            while (hasCurlyBrackets && maxLoop > 0)
            {
                content = ResolveCurlyBracket(content, out hasCurlyBrackets);
                maxLoop--;
            }
            return content;
        }

        public static string ResolveCurlyBracket(string content, out bool hasCurlyBrackets)
        {
            hasCurlyBrackets = false;
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
                                var replacement = ProcessWidgetRenderingToken(match, out hasCurlyBrackets);
                                result.Remove(beginPos, i - beginPos + 1);
                                result.Insert(beginPos, replacement);
                                i = beginPos + replacement.Length - 1;
                            }
                            catch (Exception ex)
                            {
                                Logger.Warn(ex);
                            }
                        }
                    }
                }
            }
            return result.ToString();
        }

        public static string ProcessWidgetRenderingToken(string widgetRenderingParamsString, out bool hasCurlyBrackets)
        {
            hasCurlyBrackets = false;
            // function key is the first parameter in the rendering param from {} tags
            var widgetRenderingParams = widgetRenderingParamsString.Split(new[] { "_" },
                                                                          StringSplitOptions.RemoveEmptyEntries);

            var functionKey = widgetRenderingParams.FirstOrDefault();
            if (functionKey == null)
            {
                return "{" + widgetRenderingParamsString + "}";
            }

            if (WorkContext.CurlyBrackets.Any(c => c.CurlyBracket.Equals(functionKey)))
            {
                ICurlyBracketResolver instance;

                if (TryResolve(functionKey, out instance))
                {
                    try
                    {
                        // render the widget in html string with found resolver
                        var widgetRenderedHtml = instance.Render(widgetRenderingParams);
                        hasCurlyBrackets = true;

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
            }

            // If there is no resolver found, then return back the widgetRenderingParams
            return "{" + widgetRenderingParamsString + "}";
        }

        private static bool TryResolve(string functionKey, out ICurlyBracketResolver instance)
        {
            var type = ReflectionUtilities.GetAllImplementTypesOfInterface(typeof(ICurlyBracketResolver))
                .FirstOrDefault(t => ReflectionUtilities.GetAttribute<CurlyBracketAttribute>(t).CurlyBracket.Equals(functionKey, StringComparison.InvariantCultureIgnoreCase));

            if (type != null)
            {
                instance = (ICurlyBracketResolver)Activator.CreateInstance(type);
                return true;
            }
            instance = default(ICurlyBracketResolver);
            return false;
        }
    }
}
