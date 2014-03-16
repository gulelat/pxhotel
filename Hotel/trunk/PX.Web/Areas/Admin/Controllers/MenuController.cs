using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Newtonsoft.Json;
using PX.Business.Services.Menus;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel.Models.DTO;

namespace PX.Web.Areas.Admin.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuServices _menuServices;
        public MenuController(IMenuServices menuServices)
        {
            _menuServices = menuServices;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public string _AjaxBinding(JqSearchIn si)
        {
            return JsonConvert.SerializeObject(_menuServices.SearchMenus(si));
        }

        #region Ajax Methods
        public JsonResult GetParents(int? id)
        {
            return Json(_menuServices.GetPossibleParents(id), JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpPost]
        [HandleJsonException]
        public JsonResult Manage(MenuModel model, GridManagingModel manageModel)
        {
            switch (manageModel.Operation)
            {
                case GridOperationEnums.Edit:
                case GridOperationEnums.Add:
                    var context = new ValidationContext(model, serviceProvider: null, items: null);
                    var results = new List<ValidationResult>();
                    
                    if (Validator.TryValidateObject(model, context, results, true))
                    {
                        var validationErrors =
                            results.Where(r => !r.MemberNames.Contains("Created") && !r.MemberNames.Contains("CreatedBy"));
                        if (validationErrors.Any()){
                            throw new Exception(results.First().ErrorMessage);
                        }
                    }
                    break;
            }
            return Json(_menuServices.ManageMenu(manageModel.Operation, model));
        }
    }
}
