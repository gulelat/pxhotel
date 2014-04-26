using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using PX.Business.Models.Menus;
using PX.Business.Mvc.Attributes.Authorize;
using PX.Business.Mvc.WorkContext;
using PX.Business.Services.Users;
using PX.Core.Framework.Mvc.Environments;
using PX.Business.Services.Localizes;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.Core.Ultilities;
using PX.EntityModel;
using AutoMapper;
using PX.EntityModel.Repositories;
using PX.EntityModel.Repositories.RepositoryBase.Models;

namespace PX.Business.Services.Menus
{
    public class MenuServices : IMenuServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        private readonly MenuRepository _menuRepository;
        public MenuServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
            _menuRepository = new MenuRepository();
        }

        #region Base
        public IQueryable<Menu> GetAll()
        {
            return _menuRepository.GetAll();
        }
        public IQueryable<Menu> Fetch(Expression<Func<Menu, bool>> expression)
        {
            return _menuRepository.Fetch(expression);
        }
        public Menu GetById(object id)
        {
            return _menuRepository.GetById(id);
        }
        public ResponseModel Insert(Menu menu)
        {
            return _menuRepository.Insert(menu);
        }
        public ResponseModel Update(Menu menu)
        {
            return _menuRepository.Update(menu);
        }
        public ResponseModel HierarchyUpdate(Menu menu)
        {
            return _menuRepository.HierarchyUpdate(menu);
        }
        public ResponseModel HierarchyInsert(Menu menu)
        {
            return _menuRepository.HierarchyInsert(menu);
        }
        public ResponseModel Delete(Menu menu)
        {
            return _menuRepository.Delete(menu);
        }
        public ResponseModel Delete(object id)
        {
            return _menuRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return _menuRepository.InactiveRecord(id);
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
                    menu.ParentId = model.ParentName.ToNullableInt();
                    response = HierarchyUpdate(menu);
                    if (hasUpdatePermission) UpdateMenuPermission(menu);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Menus:::Messages:::UpdateSuccessfully:::Update menu successfully.")
                        : _localizedResourceServices.T("AdminModule:::Menus:::UpdateFailure:::Update menu failed. Please try again later."));

                case GridOperationEnums.Add:
                    menu = Mapper.Map<MenuModel, Menu>(model);
                    menu.ParentId = model.ParentName.ToNullableInt();
                    response = HierarchyInsert(menu);
                    UpdateMenuPermission(menu);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Menus:::Messages:::CreateSuccessfully:::Create menu successfully.")
                        : _localizedResourceServices.T("AdminModule:::Menus:::Messages:::CreateFailure:::Create menu failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::Menus:::Messages:::DeleteSuccessfully:::Delete menu successfully.")
                        : _localizedResourceServices.T("AdminModule:::Menus:::Messages:::DeleteFailure:::Delete menu failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::Menus:::Messages:::ObjectNotFounded:::Menu is not founded.")
            };
        }

        #region Menu Permissions

        /// <summary>
        /// Initialize the menu permissions when application start
        /// </summary>
        public void InitializeMenuPermissions()
        {
            var menus = GetAll().ToList();
            foreach (var menu in menus)
            {
                UpdateMenuPermission(menu);
            }
        }

        /// <summary>
        /// Update permissions for menu
        /// </summary>
        /// <param name="menu"></param>
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
        public IEnumerable<SelectListItem> GetPossibleParents(int? id = null)
        {
            var menus = GetAll();
            int? parentId = null;
            var menu = GetById(id);
            if (menu != null)
            {
                parentId = menu.ParentId;
                menus = _menuRepository.GetPossibleParents(menu);
            }
            var data = menus.Select(m => new HierarchyModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Hierarchy = m.Hierarchy,
                    RecordOrder = m.RecordOrder,
                    Selected = parentId.HasValue && parentId.Value == m.Id
                }).ToList();
            return _menuRepository.BuildSelectList(data);
        }

        /// <summary>
        /// Get menus tree by parent id
        /// </summary>
        /// <param name="parentId">the parent id</param>
        /// <returns></returns>
        public List<AdminMenuModel> GetRenderMenus(int? parentId)
        {
            var userServices = HostContainer.GetInstance<IUserServices>();
            var userPermissions = userServices.GetUserPermissions(WorkContext.CurrentUser.Id);
            var menus = Fetch(m => m.Visible).ToList();
            return GetRenderMenus(menus, userPermissions, parentId);
        }

        public List<AdminMenuModel> GetRenderMenus(List<Menu> data, List<int> userPermissions, int? parentId = null)
        {
            return
                data.Where(
                    m =>
                        (string.IsNullOrEmpty(m.Permissions) ||
                         m.Permissions.Split(',').Select(int.Parse).Intersect(userPermissions).Count() ==
                         m.Permissions.Split(',').Count())
                        && parentId.HasValue
                            ? m.ParentId == parentId.Value
                            : !m.ParentId.HasValue)
                    .OrderBy(m => m.RecordOrder)
                    .Select(m => new AdminMenuModel
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Url = m.Url,
                        Controller = m.Controller,
                        Action = m.Action,
                        MenuIcon = m.MenuIcon,
                        Hierarchy = m.Hierarchy,
                        RecordOrder = m.RecordOrder,
                        ChildMenus = m.Id == parentId
                            ? new List<AdminMenuModel>()
                            : GetRenderMenus(data, userPermissions, m.Id)
                    }).ToList();
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
                            Fetch(m => menu.Hierarchy.Contains(m.Hierarchy) && menu.Id != m.Id).OrderBy(m => m.Hierarchy)
                                .Select(m => new BreadCrumbItem
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
    }
}
