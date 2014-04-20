using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Globalization;
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

        #region Grid Search
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
                StartPublishingDate = u.StartPublishingDate,
                EndPublishingDate = u.EndPublishingDate,
                IsPageMenu = u.PageId.HasValue,
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

        #endregion

        #region Grid Manage

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
                        _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::UpdateSuccessfully:::Update client menu successfully.")
                        : _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::UpdateFailure:::Update client menu failed. Please try again later."));

                case GridOperationEnums.Add:
                    clientMenu = Mapper.Map<ClientMenuModel, ClientMenu>(model);
                    clientMenu.ParentId = model.ParentName.ToNullableInt();
                    response = HierarchyInsert(clientMenu);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::CreateSuccessfully:::Create client menu successfully.")
                        : _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::CreateFailure:::Create client menu failed. Please try again later."));

                case GridOperationEnums.Del:
                    response = Delete(model.Id);
                    return response.SetMessage(response.Success ?
                        _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::DeleteSuccessfully:::Delete client menu successfully.")
                        : _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::DeleteFailure:::Delete client menu failed. Please try again later."));
            }
            return new ResponseModel
            {
                Success = false,
                Message = _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages::ObjectNotFounded:::Client menu is not founded.")
            };
        }

        #endregion

        #region Manage

        /// <summary>
        /// Get client menu manage model by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ClientMenuManageModel GetClientMenuManageModel(int? id = null)
        {
            var clientMenu = GetById(id);
            return clientMenu != null ? new ClientMenuManageModel(clientMenu) : new ClientMenuManageModel();
        }

        /// <summary>
        /// Save client menu manage model
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ResponseModel SaveClientMenuManageModel(ClientMenuManageModel model)
        {
            ClientMenu relativeMenu;
            ResponseModel response;
            var clientMenu = GetById(model.Id);

            #region Edit ClientMenu
            if (clientMenu != null)
            {
                clientMenu.Name = model.Name;

                clientMenu.IncludeInSiteNavigation = model.IncludeInSiteNavigation;
                clientMenu.StartPublishingDate = model.StartPublishingDate;
                clientMenu.EndPublishingDate = model.EndPublishingDate;

                //Parse url
                clientMenu.Url = string.IsNullOrWhiteSpace(model.Url)
                                       ? model.Name.ToUrlString()
                                       : model.Url.ToUrlString();

                //Get page record order
                relativeMenu = GetById(model.RelativeMenuId);
                if (relativeMenu != null)
                {
                    if (model.Position == (int)ClientMenuEnums.PositionEnums.Before)
                    {
                        clientMenu.RecordOrder = relativeMenu.RecordOrder;
                        var query =
                            string.Format(
                                "Update ClientMenus set RecordOrder = RecordOrder + 1 Where {0} And RecordOrder >= {1}",
                                relativeMenu.ParentId.HasValue ? string.Format(" ParentId = {0}", relativeMenu.ParentId) : "ParentId Is NULL", relativeMenu.RecordOrder);
                        PageRepository.ExcuteSql(query);
                    }
                    else
                    {
                        clientMenu.RecordOrder = relativeMenu.RecordOrder + 1;
                        var query =
                            string.Format(
                                "Update ClientMenus set RecordOrder = RecordOrder + 1 Where {0} And RecordOrder > {1}",
                                relativeMenu.ParentId.HasValue ? string.Format(" ParentId = {0}", relativeMenu.ParentId) : "ParentId Is NULL", relativeMenu.RecordOrder);
                        PageRepository.ExcuteSql(query);
                    }
                }


                if (clientMenu.ParentId != model.ParentId)
                {
                    clientMenu.ParentId = model.ParentId;
                    response = HierarchyUpdate(clientMenu);
                }
                else
                {
                    response = Update(clientMenu);
                }

                return response.SetMessage(response.Success ?
                    _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::UpdateSuccessfully:::Update client menu successfully.")
                    : _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::UpdateFailure:::Update client menu failed. Please try again later."));
            }
            #endregion

            clientMenu = new ClientMenu
            {
                Name = model.Name,
                ParentId = model.ParentId,
                IncludeInSiteNavigation = model.IncludeInSiteNavigation,
                StartPublishingDate = model.StartPublishingDate,
                EndPublishingDate = model.EndPublishingDate,
                Url = string.IsNullOrWhiteSpace(model.Url) ? model.Name.ToUrlString() : model.Url.ToUrlString()
            };

            //Get menu record order
            relativeMenu = GetById(model.RelativeMenuId);
            if (relativeMenu != null)
            {
                if (model.Position == (int)ClientMenuEnums.PositionEnums.Before)
                {
                    clientMenu.RecordOrder = relativeMenu.RecordOrder - 1;
                }
                else
                {
                    clientMenu.RecordOrder = relativeMenu.RecordOrder + 1;
                }
            }

            response = HierarchyInsert(clientMenu);
            return response.SetMessage(response.Success ?
                _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::CreateSuccessfully:::Create client menu successfully.")
                : _localizedResourceServices.T("AdminModule:::ClientMenus:::Messages:::CreateFailure:::Create client menu failed. Please try again later."));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ResponseModel SavePageToClientMenu(Page page)
        {
            var clientMenu = page.ClientMenus.FirstOrDefault();
            if (clientMenu != null)
            {
                clientMenu.Name = page.Title;
                clientMenu.PageId = page.Id;
                clientMenu.Url = page.FriendlyUrl;
                if (page.ParentId.HasValue && page.Page1.ClientMenus.Any())
                {
                    clientMenu.ParentId = page.Page1.ClientMenus.First().Id;
                }
                else
                {
                    clientMenu.ParentId = null;
                }
                clientMenu.IncludeInSiteNavigation = page.IncludeInSiteNavigation;
                clientMenu.StartPublishingDate = page.StartPublishingDate;
                clientMenu.EndPublishingDate = page.EndPublishingDate;
                if(page.RecordOrder != clientMenu.RecordOrder)
                {
                    var relativePages = PageRepository.Fetch(p => (page.ParentId.HasValue ? p.ParentId == page.ParentId : !p.ParentId.HasValue) && p.Id != page.Id);
                    foreach (var relativePage in relativePages)
                    {
                        var relativeMenu = relativePage.ClientMenus.First();
                        relativeMenu.RecordOrder = relativePage.RecordOrder * 10;
                        Update(relativeMenu);
                    }
                    clientMenu.RecordOrder = page.RecordOrder * 10;
                }
                clientMenu.RecordActive = page.RecordActive;
                return HierarchyUpdate(clientMenu);
            }
            clientMenu = new ClientMenu(page);
            return HierarchyInsert(clientMenu);
        }

        /// <summary>
        /// Get ClientMenu by parent id
        /// </summary>
        /// <param name="position"> </param>
        /// <param name="relativeClientMenuId"> </param>
        /// <param name="clientMenuId"> </param>
        /// <param name="parentId">the parent id</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetRelativeMenus(out int position, out int relativeClientMenuId, int? clientMenuId = null, int? parentId = null)
        {
            position = (int)ClientMenuEnums.PositionEnums.Before;
            relativeClientMenuId = 0;
            var order = 0;
            var relativeClientMenus = Fetch(p => (!clientMenuId.HasValue || p.Id != clientMenuId) && (parentId.HasValue ? p.ParentId == parentId : p.ParentId == null))
                .OrderBy(p => p.RecordOrder).Select(p => new
                {
                    p.Name,
                    p.Id,
                    p.RecordOrder
                }).ToList();
            var clientMenu = GetById(clientMenuId);
            if (clientMenu != null)
            {
                order = clientMenu.RecordOrder;
            }

            //Get position for relative ClientMenu list

            //Flag to check if current ClientMenu is the bigest order in relative ClientMenu list
            var flag = false;
            for (var i = 0; i < relativeClientMenus.Count(); i++)
            {
                if (relativeClientMenus[i].RecordOrder > order)
                {
                    relativeClientMenuId = relativeClientMenus[i].Id;
                    flag = true;
                    break;
                }
            }
            if (!flag && relativeClientMenus.Any())
            {
                position = (int)ClientMenuEnums.PositionEnums.After;
                relativeClientMenuId = relativeClientMenus.Last().Id;
            }
            var selectClientMenuId = relativeClientMenuId;
            return relativeClientMenus.Select(p => new SelectListItem
            {
                Text = p.Name,
                Value = p.Id.ToString(CultureInfo.InvariantCulture),
                Selected = p.Id == selectClientMenuId
            });
        }

        /// <summary>
        /// Get ClientMenu by parent id
        /// </summary>
        /// <param name="menuId"> </param>
        /// <param name="parentId">the parent id</param>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetRelativeMenus(int? menuId = null, int? parentId = null)
        {
            return Fetch(p => (!menuId.HasValue || p.Id != menuId) && (parentId.HasValue ? p.ParentId == parentId : p.ParentId == null))
                .OrderBy(p => p.RecordOrder).Select(p => new SelectListItem
                {
                    Text = p.Name,
                    Value = SqlFunctions.StringConvert((double)p.Id).Trim()
                });
        }

        #endregion

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
        /// Check if menu name is existed
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool IsMenuNameExisted(int? id, string name)
        {
            return Fetch(u => u.Name.Equals(name) && (!id.HasValue || u.Id != id)).Any();
        }
    }
}
