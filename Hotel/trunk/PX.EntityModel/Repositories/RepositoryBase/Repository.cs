using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using PX.Core.Framework.Mvc.Models;
using PX.EntityModel.Repositories.RepositoryBase.Extensions;
using PX.EntityModel.Repositories.RepositoryBase.Models;

namespace PX.EntityModel.Repositories.RepositoryBase
{
    public class Repository<T> where T : class
    {
        #region Protected Properties

        public static PXHotelEntities StaticContext
        {
            get;
            set;
        }

        protected static PXHotelEntities DataContext
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return StaticContext ?? (StaticContext = new PXHotelEntities());
                }

                if (HttpContext.Current.Items["PXHotelEntities"] == null)
                    HttpContext.Current.Items["PXHotelEntities"] = new PXHotelEntities();

                return (PXHotelEntities)HttpContext.Current.Items["PXHotelEntities"];
            }
        }

        #endregion

        #region Public Methods

        public static IQueryable<T> GetAll()
        {
            return DataContext.Set<T>();
        }

        public static IQueryable<T> Fetch(Expression<Func<T, bool>> expression)
        {
            return DataContext.Set<T>().Where(expression);
        }

        public static T FetchFirst(Expression<Func<T, bool>> expression)
        {
            return DataContext.Set<T>().FirstOrDefault(expression);
        }

        public static T GetById(object id)
        {
            return DataContext.Set<T>().Find(id);
        }

        public static ResponseModel Update(T entity)
        {
            entity.SetProperty("Updated", DateTime.Now);
            if (entity.GetPropertyValue("UpdatedBy") == null)
            {
                entity.SetProperty("UpdatedBy", HttpContext.Current.User.Identity.Name);
            }
            var response = new ResponseModel();
            try
            {
                DataContext.SaveChanges();
                response.Data = entity;
                response.Success = true;
            }
            catch (DbEntityValidationException e)
            {
                response.Success = false;
                response.Message = BuildEntityValidationError(e);
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }

        public static ResponseModel Insert(T entity)
        {
            entity.SetProperty("Created", DateTime.Now);
            entity.SetProperty("CreatedBy", HttpContext.Current.User.Identity.Name);
            entity.SetProperty("RecordActive", true);

            var response = new ResponseModel();
            try
            {
                var dbSet = DataContext.Set<T>();
                dbSet.Add(entity);
                DataContext.SaveChanges();
                response.Data = entity;
                response.Success = true;
            }
            catch (DbEntityValidationException e)
            {
                response.Success = false;
                response.Message = BuildEntityValidationError(e);
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }

        public static ResponseModel Delete(T entity)
        {
            var response = new ResponseModel();
            try
            {
                var dbSet = DataContext.Set<T>();
                dbSet.Remove(entity);
                DataContext.SaveChanges();
                response.Success = true;
            }
            catch (DbEntityValidationException e)
            {
                response.Success = false;
                response.Message = BuildEntityValidationError(e);
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }

        public static ResponseModel Delete(object id)
        {
            var response = new ResponseModel();
            try
            {
                var entity = GetById(id);
                var dbSet = DataContext.Set<T>();
                dbSet.Remove(entity);
                DataContext.SaveChanges();
                response.Success = true;
            }
            catch (DbEntityValidationException e)
            {
                response.Success = false;
                response.Message = BuildEntityValidationError(e);
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }

        public static ResponseModel InactiveRecord(int id)
        {
            var response = new ResponseModel();
            try
            {
                var entity = GetById(id);
                entity.SetProperty("RecordActive", false);
                return Update(entity);
            }
            catch (DbEntityValidationException e)
            {
                response.Success = false;
                response.Message = BuildEntityValidationError(e);
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }

        private static string BuildEntityValidationError(DbEntityValidationException exception)
        {
            var message = string.Empty;
            foreach (var eve in exception.EntityValidationErrors)
            {
                message += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                foreach (var ve in eve.ValidationErrors)
                {
                    message += string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                }
            }
            return message;
        }

        #endregion

        #region Hierarchy Repository

        private const char IdSeparator = '.';
        private const string ParentIdPropertyName = "ParentId";
        private const string HierarchyPropertyName = "Hierarchy";

        /// <summary>
        /// Insert entity with hierarchy support
        /// </summary>
        /// <param name="entity">the input entity</param>
        /// <returns></returns>
        public static ResponseModel HierarchyInsert(T entity)
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
        public static ResponseModel HierarchyUpdate(T entity)
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

        public IQueryable<T> GetHierarchyTree(T entity)
        {
            if (entity == null)
            {
                return null;
            }
            var prefix = entity.GetHierarchy();
            return GetAll().Where(string.Format("{0} LIKE '{1}%'", HierarchyPropertyName, prefix));
        }

        public IQueryable<T> GetAncestors(T entity)
        {
            if (entity == null)
            {
                return null;
            }
            var prefix = entity.GetHierarchy();
            return GetAll().Where(string.Format("{0} LIKE '{1}%'", HierarchyPropertyName, prefix));
        }

        /// <summary>
        /// Build select list from data
        /// </summary>
        /// <param name="data">the input data must be serializable</param>
        /// <param name="levelPrefix">the prefix level</param>
        /// <param name="needReorder"> </param>
        /// <returns></returns>
        public static List<SelectListItem> BuildSelectList(List<HierarchyModel> data, string levelPrefix, bool needReorder = true)
        {
            if(needReorder)
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
                    Value = item.Id.ToString(DefaultFormat)
                });
            }
            return selectList;
        }
        #endregion

        public static IFormatProvider DefaultFormat { get; set; }
    }
}
