using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Business.Models.Pages;
using PX.Business.Mvc.WorkContext;
using PX.Core.Configurations;
using PX.Core.Ultilities;

namespace PX.Business.Services.CurlyBrackets
{
    public class CurlyBracketParser
    {
        /// <summary>
        /// Parse curly bracket properties to razor syntax
        /// </summary>
        /// <returns></returns>
        public static string ParseProperties(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }

            var modelProperties = ReflectionUtilities.GetAllPropertyNamesOfType(typeof(PageRenderModel));

            var result = new StringBuilder(content);
            var positions = new Stack<int>();
            for (var i = 0; i < result.Length; i++)
            {
                if (result[i] == '{')
                {
                    positions.Push(i);
                }
                else if (result[i] == '}')
                {
                    if (positions.Count > 0)
                    {
                        var beginPos = positions.Pop();
                        if (i > beginPos + 1)
                        {
                            var match = result.ToString(beginPos + 1, i - beginPos - 1);
                            if (!WorkContext.CurlyBrackets.Any(c => c.CurlyBracket.Equals(match)))
                            {
                                if (modelProperties.Contains(match))
                                {
                                    var replacement = string.Format("@Model.{0}", match);
                                    result.Remove(beginPos, i - beginPos + 1);
                                    result.Insert(beginPos, replacement);
                                    i = beginPos + replacement.Length - 1;
                                }
                            }
                        }
                    }
                }
            }
            return result.ToString();
        }

        /// <summary>
        /// Parse {RenderBody} curly bracket to razor syntax
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string ParseRenderBody(string content)
        {
            content = content.Replace(Configurations.CurlyBracketRenderBody, Configurations.RenderBody);
            return content;
        }
    }
}
