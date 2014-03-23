using PX.Business.Mvc.Attributes;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;

namespace PX.Business.Services.Pages.CurlyBracketResolver
{
    [CurlyBracket(Name = "Page", CurlyBracket = "{Page}")]
    public class PageCurlyBracketResolver : ICurlyBracketResolver
    {
        public string Render(string curlyBracket)
        {
            return curlyBracket;
        }
    }
}
