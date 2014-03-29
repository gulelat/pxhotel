using System.Web.Mvc;

namespace PX.Business.Services.CurlyBrackets.CurlyBracketResolver
{
    public interface ICurlyBracketResolver
    {
        string DefaultTemplate();

        void Initialize();

        string Render(string[] parameters);
    }
}
