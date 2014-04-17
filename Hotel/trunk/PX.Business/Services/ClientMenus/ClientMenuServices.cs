using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using PX.Business.Models.ClientMenus;
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

namespace PX.Business.Services.ClientMenus
{
    public class ClientMenuServices : IClientMenuServices
    {
        private readonly ILocalizedResourceServices _localizedResourceServices;
        public ClientMenuServices()
        {
            _localizedResourceServices = HostContainer.GetInstance<ILocalizedResourceServices>();
        }

        #region Base
        public IQueryable<ClientMenu> GetAll()
        {
            return ClientMenuRepository.GetAll();
        }
        public IQueryable<ClientMenu> Fetch(Expression<Func<ClientMenu, bool>> expression)
        {
            return ClientMenuRepository.Fetch(expression);
        }
        public ClientMenu GetById(object id)
        {
            return ClientMenuRepository.GetById(id);
        }
        public ResponseModel Insert(ClientMenu clientMenu)
        {
            return ClientMenuRepository.Insert(clientMenu);
        }
        public ResponseModel Update(ClientMenu clientMenu)
        {
            return ClientMenuRepository.Update(clientMenu);
        }
        public ResponseModel HierarchyUpdate(ClientMenu clientMenu)
        {
            return ClientMenuRepository.HierarchyUpdate(clientMenu);
        }
        public ResponseModel HierarchyInsert(ClientMenu clientMenu)
        {
            return ClientMenuRepository.HierarchyInsert(clientMenu);
        }
        public ResponseModel Delete(ClientMenu clientMenu)
        {
            return ClientMenuRepository.Delete(clientMenu);
        }
        public ResponseModel Delete(object id)
        {
            return ClientMenuRepository.Delete(id);
        }
        public ResponseModel InactiveRecord(int id)
        {
            return ClientMenuRepository.InactiveRecord(id);
        }
        #endregion

        /// <summary>
        /// search the ClientMenus.
        /// </summary>
        /// <returns></returns>
        public JqGridSearchOut SearchClientMenus(JqSearchIn si)
        {
            var clientMenus = GetAll().Select(u => new ClientMenuModel
            {
                Id = u.Id,
                Url = u.Url,
                Name = u.Name,
                Hierarchy = u.Hierarchy,
                ParentId = u.ParentId,
                ParentName = u.ClientMenu1.Name,
                RecordActive = u.RecordActive,
                RecordOrder = u.RecordOrder,
                Created = u.Created,
                CreatedBy = u.CreatedBy,
                Updated = u.Updated,
                UpdatedBy = u.UpdatedBy
            });

            return si.Search(clientMenus);
        }

        /// <summary>
        /// Manage ClientMenu
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel ManageClientMenu(GridOperationEnums operation, ClientMenuModel model)
        {
            Mapper.CreateMap<ClientMenuModel, ClientMenu>();
            ClientMenu clientMenu;
            ResponseModel response;
            switch (operation)
            {
                case GridOperationEnums.Edit:
                    clientMenu = GetById(model.Id);
                    clientMenu.Name = model.Name;
                    clientMenu.Url = model.Url;
                    clientMenu.ParentId = model.ParentId;
                    clientMenu.RecordActive = model.RecordActive;
                    clientMenu.RecordOrder = model.RecordOrder;
                    clientMenu.ParentId = model.ParentName.ToNullableInt();
                    response = HierarchyUpdate(clientMenu);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::Update client menu successfully")
                        : _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::Update client menu failure. Please try again later."));

                case GridOperationEnums.Add:
                    clientMenu = Mapper.Map<ClientMenuModel, ClientMenu>(model);
                    clientMenu.ParentId = model.ParentName.ToNullableInt();
                    response = HierarchyInsert(clientMenu);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::Create client menu successfully")
                        : _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::Create client menu failure. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::Delete client menu successfully")
                        : _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::Delete client menu failure. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::Client menu not founded.")
            };
        }

        /// <summary>
        /// Get possible parent ClientMenu
        /// </summary>
        /// <param name="id">the current ClientMenu id</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetPossibleParents(int? id = null)
        {
            var clientMenus = GetAll();
            int? parentId = null;
            var clientMenu = GetById(id);
            if (clientMenu != null)
            {
                parentId = clientMenu.ParentId;
                clientMenus = ClientMenuRepository.GetPossibleParents(clientMenu);
            }
            var data = clientMenus.Select(m => new HierarchyModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Hierarchy = m.Hierarchy,
                    RecordOrder = m.RecordOrder,
                    Selected = parentId.HasValue && parentId.Value == m.Id
                }).ToList();
            return ClientMenuRepository.BuildSelectList(data);
        }

        /// <summary>
        /// Get client menu by parent id
        /// </summary>
        /// <param name="parentId">the parent id</param>
        /// <returns></returns>
        public List<ClientMenu> GetClientMenus(int? parentId = null)
        {
            return Fetch(m => parentId.HasValue ? m.ParentId == parentId : !m.ParentId.HasValue).ToList();
        }

        /// <summary>
        /// Get breadcrumbs
        /// </summary>
        /// <param name="url"> </param>
        /// <returns></returns>
        public ClientBreadCrumbModel GetBreadCrumbs(string url)
        {
            var clientMenu =
                GetAll().FirstOrDefault(m => m.Url.ToLower().Equals(url));
            if (clientMenu != null)
            {
                return new ClientBreadCrumbModel
                    {
                        BreadCrumbs =
                            Fetch(m => clientMenu.Hierarchy.Contains(m.Hierarchy) && clientMenu.Id != m.Id).OrderBy(
                                m => m.Hierarchy)
                                .Select(m => new ClientBreadCrumbItem
                                    {
                                        Name = m.Name,
                                        Url = m.Url
                                    }).ToList(),
                        CurrentBreadCrumbItem = new ClientBreadCrumbItem
                            {
                                Name = clientMenu.Name,
                                Url = clientMenu.Url
                            }
                    };
            }
            return new ClientBreadCrumbModel();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ResponseModel SavePageToClientMenu(Page page)
        {
            ResponseModel response;
            var clientMenu = page.ClientMenus.FirstOrDefault();
            if (clientMenu != null)
            {
                if(!page.IncludeInSiteNavigation)
                {
                    response = Delete(clientMenu);
                }
                else
                {
                    clientMenu.Url = page.FriendlyUrl;
                    clientMenu.Name = page.Title;
                    clientMenu.RecordOrder = page.RecordOrder;

                }
            }
            clientMenu = new ClientMenu
                {
                    Name = page.Title,
                    PageId = page.Id,
                    Url = page.FriendlyUrl,
                    RecordOrder = page.RecordOrder,
                    RecordActive = true,
                };
            return new ResponseModel();
        }
    }
}
