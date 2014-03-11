using System.Web.Mvc;
using PX.Business.Services.Users;
using PX.Web.ViewModels.BackEnd.UserModels;

namespace PX.Web.Areas.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserServices _userServices;
        public UserController(IUserServices userServices)
        {
            _userServices = userServices;
        }

        public ActionResult Index(int? page, string search)
        {
            var userListViewModel = new UserListingViewModel(_userServices, page, search);
            userListViewModel.Search();
            return View(userListViewModel);
        }

        [HttpPost]
        public ActionResult Index(UserListingViewModel model)
        {
            model.Search();
            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }
    }
}
