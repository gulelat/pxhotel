using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using PX.Core.Configurations;
using PX.Core.Framework.Mvc.Models;
using PX.EntityModel.Repositories.RepositoryBase.Extensions;
using PX.EntityModel.Repositories.RepositoryBase.Models;

namespace PX.EntityModel.Repositories.RepositoryBase
{
    public class HierarchyRepository<T> : Repository<T> where T : class
    {
        #region Private Properties

        private const char IdSeparator = '.';
        private const string ParentIdPropertyName = "ParentId";
        private const string HierarchyPropertyName = "Hierarchy";

        #endregion

        #region Hierarchy Repository

        /// <summary>
        /// Insert entity with hierarchy support
        /// </summary>
        /// <param name="entity">the input entity</param>
        /// <returns></returns>
        public ResponseModel HierarchyInsert(T entity)
        {
            entity.SetProperty(HierarchyPropertyName, string.Empty);
            var response = Insert(entity);
            if (response.Success)
            {
                var parentId = entity.GetParentId();
                var parent = GetById(parentId);
                var id = entity.GetId();
                if (parent != null)
                {
                    var menuHierarchy = parent.GetHierarchy();
                    entity.SetProperty(HierarchyPropertyName, string.Format("{0}{1}{2}", menuHierarchy, id.ToString("D5"), IdSeparator));
                }
                else
                {
                    entity.SetProperty(HierarchyPropertyName, string.Format("{0}{1}{0}", IdSeparator, id.ToString("D5")));
                }

                return Update(entity);
            }
            return response;
        }

        /// <summary>
        /// Update entity with hierarchy support 
        /// </summary>
        /// <param name="entity">the input entity</param>
        /// <returns></returns>
        public ResponseModel HierarchyUpdate(T entity)
        {
            var entry = DataContext.Entry(entity);
            if (!Equals(entry.OriginalValues[ParentIdPropertyName], entry.CurrentValues[ParentIdPropertyName]))
            {
                var tableName = entity.GetTableName();

                var parentId = (int?)entry.CurrentValues[ParentIdPropertyName];
                var parent = GetById(parentId);

                var currentPrefix = entity.GetHierarchy();
                var hierarchy = parent == null ? entity.GetHierarchyValueForRoot() : entity.CalculateHierarchyValue(parent);

                var query = string.Format("UPDATE " + tableName +
                                          " SET {2} = '{1}' + RIGHT({2}, LEN({2}) - LEN('{0}')) " +
                                          " WHERE {2} LIKE '{0}%'"
                    , currentPrefix, hierarchy, HierarchyPropertyName);
                DataContext.Database.ExecuteSqlCommand(query);
            }
            return Update(entity);
        }

        /// <summary>
        /// Delete entity with hierarchy support
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ResponseModel HierarchyDelete(T entity)
        {
            var response = new ResponseModel();
            return response;
        }

        /// <summary>
        /// Get possible parents
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IQueryable<T> GetPossibleParents(T entity)
        {
            if (entity == null)
            {
                return null;
            }
            var prefix = entity.GetHierarchy();
            return GetAll().Where(string.Format("!{0}.Contains(\"{1}\")", HierarchyPropertyName, prefix));
        }

        /// <summary>
        /// Get all child items of parent
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IQueryable<T> GetHierarcies(T entity)
        {
            if (entity == null)
            {
                return null;
            }
            var prefix = entity.GetHierarchy();
            return GetAll().Where(string.Format("{0}.StartsWith(\"{1}\")", HierarchyPropertyName, prefix));
        }

        /// <summary>
        /// Build select list from data
        /// </summary>
        /// <param name="data">the input data must be serializable</param>
        /// <param name="levelPrefix">the prefix level</param>
        /// <param name="needReorder"> </param>
        /// <returns></returns>
        public List<SelectListItem> BuildSelectList(List<HierarchyModel> data, bool needReorder = true, string levelPrefix = Configurations.HierarchyLevelPrefix)
        {
            if (needReorder)
            {
                var dic = data.ToDictionary(i => i.GetId(), i => i.RecordOrder);
                foreach (var item in data)
                {
                    var hierarchy = item.Hierarchy;
                    var hierarchyIds = hierarchy.Substring(1, hierarchy.Length - 2).Split(IdSeparator).Select(int.Parse).ToList();
                    var order = string.Empty;
                    foreach (var id in hierarchyIds)
                    {
                        order += string.Format("{0}{1}", IdSeparator, dic.FirstOrDefault(d => d.Key == id).Value.ToString(DefaultFormat));
                    }
                    item.Hierarchy = order;
                }
                data = data.AsQueryable().OrderBy(d => d.Hierarchy).ToList();
            }

            var selectList = new List<SelectListItem>();
            foreach (var item in data)
            {
                var prefix = string.Empty;
                var hierarchy = item.Hierarchy;
                var count = hierarchy.Count(c => c.Equals(IdSeparator));
                for (var i = 0; i < count - 1; i++)
                {
                    prefix += levelPrefix;
                }
                selectList.Add(new SelectListItem
                {
                    Text = string.Format("{0}{1}", prefix, item.Name),
                    Value = item.Id.ToString(DefaultFormat),
                    Selected = item.Selected
                });
            }
            return selectList;
        }
        #endregion
    }
}
