using System.Web.Mvc;
using PX.Business.Services.CurlyBrackets;

namespace PX.Web.Areas.Admin.Controllers
{
    public class CurlyBracketController : Controller
    {
        private readonly ICurlyBracketServices _curlyBracketServices;
        public CurlyBracketController(ICurlyBracketServices curlyBracketServices)
        {
            _curlyBracketServices = curlyBracketServices;
        }

        //
        // GET: /Admin/CurlyBracket/
        public JsonResult GetCurlyBracketList()
        {
            return Json(_curlyBracketServices.GetAll());
        }

    }
}
