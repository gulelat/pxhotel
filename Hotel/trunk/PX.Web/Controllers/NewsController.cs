﻿using System.Web.Mvc;
using PX.Business.Services.News;

namespace PX.Web.Controllers
{
    public class NewsController : Controller
    {
        private readonly INewsServices _newsServices;
        public NewsController(INewsServices newsServices)
        {
            _newsServices = newsServices;
        }

        public ActionResult Details(int id)
        {
            var model = _newsServices.GetNews(id);
            if(model == null)
            {
                return HttpNotFound();
            }
            return View(model);
        }
    }
}
