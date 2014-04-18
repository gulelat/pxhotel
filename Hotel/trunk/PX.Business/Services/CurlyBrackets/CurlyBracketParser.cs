using System.Collections.Generic;
using System.Linq;
using System.Text;
using PX.Business.Mvc.WorkContext;

namespace PX.Business.Services.CurlyBrackets
{
    public class CurlyBracketParser
    {
        /// <summary>
        /// Replace curly bracket with razor syntax
        /// </summary>
        /// <returns></returns>
        public static string Parse(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return content;
            }
            content = content.Replace("{RenderBody}", "@RenderBody()");

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
                                var replacement = string.Format("Model.{0}", match);
                                result.Remove(beginPos, i - beginPos + 1);
                                result.Insert(beginPos, replacement);
                                i = beginPos + replacement.Length - 1;
                            }
                        }
                    }
                }
            }
            return result.ToString();
        }
    }
}
