using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using PX.Core.Framework.Enums;
using PX.Core.Framework.Mvc.Models;
using PX.Core.Framework.Mvc.Models.JqGrid;
using PX.EntityModel;
using PX.EntityModel.Models.DTO;
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
        public Menu GetById(int id)
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
        public ResponseModel Delete(Menu menu)
        {
            return MenuRepository.Delete(menu);
        }
        public ResponseModel Delete(int id)
        {
            return MenuRepository.Delete(id);
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
                MenuClass = u.MenuClass,
                ParentId = u.ParentId,
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
        /// Gets the Menu roles.
        /// </summary>
        /// <returns></returns>
        public IQueryable<Role> GetAllRoles()
        {
            return RoleRepository.GetAll();
        }

        /// <summary>
        /// Gets the Menu roles.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetRoles()
        {
            return RoleRepository.GetAll().ToList().Select(r => new SelectListItem
                {
                    Text = r.Name,
                    Value = r.Id.ToString(CultureInfo.InvariantCulture)
                });
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
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    menu = GetById(model.Id);
                    menu.Name = model.Name;
                    menu.Url = model.Url;
                    menu.Controller = model.Controller;
                    menu.Action = model.Action;
                    menu.ParentId = model.ParentId;
                    return Update(menu);
                case GridOperationEnums.Add:
                    menu = Mapper.Map<MenuModel, Menu>(model);
                    return Insert(menu);
                case GridOperationEnums.Del:
                    return Delete(model.Id);
            }
            return new ResponseModel
                {
                    Success = false,
                    Message = "Object not founded"
                };
        }

        public IEnumerable<SelectListItem> GetPossibleParents(int? id)
        {
            var key = string.Empty;
            if (id.HasValue)
            {
                key = string.Format(".{0}.", id.Value.ToString("D5"));
            }
            return GetAll().Where(m => !m.Hierarchy.Contains(key)).Select(m => new SelectListItem
                 {
                     Text = m.Name,
                     Value = SqlFunctions.StringConvert((double)m.Id).Trim()
                 });
        }
    }
}
