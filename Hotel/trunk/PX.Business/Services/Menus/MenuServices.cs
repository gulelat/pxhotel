using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using PX.Business.Models.Menus;
using PX.Business.Mvc.Attributes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using AutoMapper;
using PX.EntityModel.Repositories;

namespace PX.Business.Services.Menus
{
    public class MenuServices : IMenuServices
    {
        #region Base
        public IQueryable<Menu> GetAll()
        {
            return MenuRepository.GetAll();
        }
        public Menu GetById(object id)
        {
            return MenuRepository.GetById(id);
        }
        public ResponseModel Insert(Menu menu)
        {
            return MenuRepository.Insert(menu);
        }
        public ResponseModel Update(Menu menu)
        {
            return MenuRepository.Update(menu);
        }
        public ResponseModel HierarchyUpdate(Menu menu)
        {
            return MenuRepository.HierarchyUpdate(menu);
        }
        public ResponseModel HierarchyInsert(Menu menu)
        {
            return MenuRepository.HierarchyInsert(menu);
        }
        public ResponseModel Delete(Menu menu)
        {
            return MenuRepository.Delete(menu);
        }
        public ResponseModel Delete(object id)
        {
            return MenuRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return MenuRepository.InactiveRecord(id);
        }
        #endregion

        /// <summary>
        /// search the Menus.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchMenus(JqSearchIn si)
        {
            var menus = GetAll().Select(u => new MenuModel
            {
                Id = u.Id,
                Url = u.Url,
                Controller = u.Controller,
                Action = u.Action,
                Name = u.Name,
                Hierarchy = u.Hierarchy,
                MenuIcon = u.MenuIcon,
                ParentId = u.ParentId,
                ParentName = u.Menu1.Name,
                Visible = u.Visible,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(menus);
        }

        /// <summary>
        /// Manage Menu
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel ManageMenu(GridOperationEnums operation, MenuModel model)
        {
            Mapper.CreateMap<MenuModel, Menu>();
            Menu menu;
            ResponseModel response;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    menu = GetById(model.Id);
                    menu.Name = model.Name;
                    menu.Url = model.Url;
                    var hasUpdatePermission = !(model.Controller == menu.Controller && model.Action == menu.Action);
                    menu.Controller = model.Controller;
                    menu.Action = model.Action;
                    menu.ParentId = model.ParentId;
                    menu.MenuIcon = model.MenuIcon;
                    menu.Visible = model.Visible;
                    menu.RecordActive = model.RecordActive;
                    menu.RecordOrder = model.RecordOrder;
                    int parentId;
                    if (int.TryParse(model.ParentName, out parentId))
                    {
                        menu.ParentId = parentId;
                    }
                    else
                    {
                        menu.ParentId = null;
                    }
                    response = HierarchyUpdate(menu);
                    if (hasUpdatePermission) UpdateMenuPermission(menu);
                    return response;
                case GridOperationEnums.Add:
                    menu = Mapper.Map<MenuModel, Menu>(model);
                    if (int.TryParse(model.ParentName, out parentId))
                    {
                        menu.ParentId = parentId;
                    }
                    else
                    {
                        menu.ParentId = null;
                    }
                    menu.Hierarchy = string.Empty;
                    response = HierarchyInsert(menu);
                    UpdateMenuPermission(menu);
                    return response;
                case GridOperationEnums.Del:
                    return Delete(model.Id);
            }
            return new ResponseModel
            {
                Success = false,
                Message = "Object not founded"
            };
        }

        #region Menu Permissions

        public void InitializeMenuPermissions()
        {
            var menus = GetAll().ToList();
            foreach (var menu in menus)
            {
                UpdateMenuPermission(menu);
            }
        }

        private void UpdateMenuPermission(Menu menu)
        {
            var permissions = new List<PermissionEnums>();
            if (string.IsNullOrEmpty(menu.Controller))
            {
                menu.Permissions = string.Empty;
            }
            else
            {
                var assemblies = AppDomain.CurrentDomain.GetAssemblies(); // currently loaded assemblies
                var controllerType = assemblies
                    .SelectMany(a => a.GetTypes())
                    .FirstOrDefault(t => t != null
                        && t.IsPublic
                        && t.Name.Equals(string.Format("{0}Controller", menu.Controller), StringComparison.OrdinalIgnoreCase)
                        && !t.IsAbstract
                        && typeof(IController).IsAssignableFrom(t));
                if (controllerType != null)
                {

                    //Get permission from controller
                    var controllerAuthorizationAttributes = controllerType.GetCustomAttributes(typeof(PxAuthorizeAttribute), false)
                                    .Cast<PxAuthorizeAttribute>();
                    foreach (var attribute in controllerAuthorizationAttributes)
                    {
                        if (attribute.Permissions != null && attribute.Permissions.Any())
                            permissions.AddRange(attribute.Permissions);
                    }

                    //Get permission from actions
                    var methods = controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                        .Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType)
                                    && m.Name.Equals(menu.Action)).ToList();
                    if (methods.Any())
                    {
                        foreach (var methodInfo in methods)
                        {
                            var actionAuthorizationAttributes = methodInfo
                                    .GetCustomAttributes(typeof(PxAuthorizeAttribute), false)
                                    .Cast<PxAuthorizeAttribute>();
                            foreach (var attribute in actionAuthorizationAttributes)
                            {
                                permissions.AddRange(attribute.Permissions);
                            }
                        }
                    }
                }
            }
            menu.Permissions = permissions.Any() ? string.Join(",", permissions.Select(i => (int)i)) : string.Empty;
            Update(menu);
        }
        #endregion

        /// <summary>
        /// Get possible parent menu
        /// </summary>
        /// <param name="id">the current menu id</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPossibleParents(int? id)
        {
            var menus = GetAll();
            if (id.HasValue)
            {
                var key = string.Format(".{0}.", id.Value.ToString("D5"));
                menus = menus.Where(m => !m.Hierarchy.Contains(key));
            }
            return MenuRepository.BuildSelectList(menus.ToList(), "--", "Name");
        }

        /// <summary>
        /// Get breadcrumbs
        /// </summary>
        /// <param name="controller">the current controller name</param>
        /// <param name="action">the current action name</param>
        /// <returns></returns>
        public BreadCrumbModel GetBreadCrumbs(string controller, string action)
        {
            var menu =
                GetAll().FirstOrDefault(m => m.Controller.ToLower().Equals(controller) && m.Action.ToLower().Equals(action));
            if (menu != null)
            {
                return new BreadCrumbModel
                {
                    BreadCrumbs =
                        GetAll().Where(m => menu.Hierarchy.Contains(m.Hierarchy) && menu.Id != m.Id).OrderBy(m => m.Hierarchy).Select(m => new BreadCrumbItem
                        {
                            Name = m.Name,
                            Url = m.Url,
                            Action = m.Action,
                            Controller = m.Controller,
                            MenuIcon = m.MenuIcon
                        }).ToList(),
                    CurrentBreadCrumbItem = new BreadCrumbItem
                    {
                        Name = menu.Name,
                        Url = menu.Url,
                        Action = menu.Action,
                        Controller = menu.Controller,
                        MenuIcon = menu.MenuIcon
                    }
                };
            }
            return new BreadCrumbModel();
        }

        public List<Menu> GetMenus(int? parentId = null)
        {
            var permissions = User.CurrentUser.UserGroup.GroupPermissions.Where(p => p.HasPermission).Select(p => p.PermissionId);
            var memus = GetAll().Where(m => m.Visible && parentId.HasValue ? m.ParentId == parentId : !m.ParentId.HasValue).ToList();

            return memus.Where(m => string.IsNullOrEmpty(m.Permissions) ||  m.Permissions.Split(',').Select(int.Parse).Intersect(permissions).Count() == m.Permissions.Split(',').Count()).OrderBy(m => m.RecordOrder).ToList();
        }
    }
}
