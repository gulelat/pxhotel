using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Services.CurlyBrackets;
using PX.Core.Framework.Mvc.Models.JqGrid;

namespace PX.Web.Areas.Admin.Controllers
{
    public class CurlyBracketsController : Controller
    {
        private readonly ICurlyBracketServices _curlyBracketServices;
        public CurlyBracketsController(ICurlyBracketServices curlyBracketServices)
        {
            _curlyBracketServices = curlyBracketServices;
        }

        //
        // GET: /Admin/CurlyBracket/
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_curlyBracketServices.SearchCurlyBrackets(si));
        }

        public JsonResult GetProperties(string type)
        {
            return Json(_curlyBracketServices.GetCurlyBracketSelectListFromObject(type));
        }

        public JsonResult GetCurlyBracketList()
        {
            return Json(_curlyBracketServices.GetAllCurlyBracketsOfApplication());
        }
    }
}
