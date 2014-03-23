using System.Collections.Generic;
using PX.Business.Models.CurlyBrackets;

namespace PX.Business.Services.CurlyBrackets
{
    public interface ICurlyBracketServices
    {
        List<CurlyBracketModel> GetAll();

        string Render(string content);
    }
}
