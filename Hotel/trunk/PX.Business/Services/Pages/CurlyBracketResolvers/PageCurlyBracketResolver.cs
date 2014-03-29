using System.Web.Mvc;
using PX.Business.Models.Pages;
using PX.Business.Mvc.Attributes;
using PX.Business.Services.CurlyBrackets.CurlyBracketResolver;

namespace PX.Business.Services.Pages.CurlyBracketResolvers
{
    [CurlyBracket(Name = "Page", CurlyBracket = "Page", Descrition = "Sample Curly Bracket", Type = typeof(PageRenderModel))]
    public class PageCurlyBracketResolver : ICurlyBracketResolver
    {
        public void Initialize()
        {
            
        }

        public string DefaultTemplate()
        {
            return "Default.Page";
        }

        public string Render(string[] parameters)
        {
            return "This is rendering for {Page} curly bracket";
        }
    }
}
