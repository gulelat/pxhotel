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

namespace PX.EntityModel.Repositories.RepositoryBase
{
    public class Repository<T> where T : class
    {
        #region Protected Properties

        // this static context is a bad, bad thing, but the only things that hit this will be


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
            if (HttpContext.Current.Session != null)
            {
                entity.SetProperty("Updated", DateTime.Now);
                if (User.CurrentUser != null)
                {
                    entity.SetProperty("UpdatedBy", User.CurrentUser.Email);
                }
            }
            var response = new ResponseModel();
            try
            {
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

        public static ResponseModel Insert(T entity)
        {
            if (HttpContext.Current.Session != null)
            {
                entity.SetProperty("Created", DateTime.Now);
                entity.SetProperty("CreatedBy", User.CurrentUser != null ? User.CurrentUser.Email : string.Empty);
            }
            
            var response = new ResponseModel();
            try
            {
                var dbSet = DataContext.Set<T>();
                dbSet.Add(entity);
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
        private const string OrderPropertyName = "RecordOrder";

        public static ResponseModel HierarchyInsert(T entity)
        {
            var response = Insert(entity);
            if (response.Success)
            {
                var parentId = entity.GetParentId();
                var parent = GetById(parentId);
                var id = entity.GetId();
                if (parent != null)
                {
                    var menuHierarchy = parent.GetHierarchy();
                    entity.SetProperty("Hierarchy", string.Format("{0}{1}{2}", menuHierarchy, id.ToString("D5"), IdSeparator));
                }
                else
                {
                    entity.SetProperty("Hierarchy", string.Format("{0}{1}{0}", IdSeparator, id.ToString("D5")));
                }

                return Update(entity);
            }
            return response;
        }

        public static ResponseModel HierarchyUpdate(T entity)
        {
            var entry = DataContext.Entry(entity);
            if (!Equals(entry.OriginalValues["ParentId"], entry.CurrentValues["ParentId"]))
            {
                var tableName = entity.GetTableName();

                var parentId = (int?)entry.CurrentValues[ParentIdPropertyName];
                var parent = GetById(parentId);

                var currentPrefix = entity.GetHierarchy();
                var hierarchy = parent == null ? entity.GetAncestorValueForRoot() : entity.CalculateAncestorValue(parent);

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

        public static List<SelectListItem> BuildSelectList(string levelPrefix, string textFieldName)
        {
            return InternalBuildSelectList(null, levelPrefix, textFieldName);
        }

        public static List<SelectListItem> BuildSelectList(List<T> data, string levelPrefix, string textFieldName)
        {
            return InternalBuildSelectList(data, levelPrefix, textFieldName);
        }

        private static List<SelectListItem> InternalBuildSelectList(List<T> data, string levelPrefix, string textFieldName)
        {
            var dic = data.ToDictionary(i => i.GetId(), i => (int)i.GetPropertyValue(OrderPropertyName));

            foreach (var item in data)
            {
                var hierarchy = item.GetHierarchy();
                var hierarchyIds = hierarchy.Substring(1, hierarchy.Length - 2).Split(IdSeparator).Select(int.Parse).ToList();
                var order = string.Empty;
                foreach (var id in hierarchyIds)
                {
                    order += string.Format("{0}{1}", IdSeparator, dic.FirstOrDefault(d => d.Key == id).Value.ToString(DefaultFormat));
                }
                item.SetProperty(HierarchyPropertyName, order);
            }
            data = data.AsQueryable().OrderBy(HierarchyPropertyName).ToList();

            var selectList = new List<SelectListItem>();
            foreach (var menu in data)
            {
                var prefix = string.Empty;
                var hierarchy = menu.GetHierarchy();
                var a = menu.GetId();
                var count = hierarchy.Count(c => c.Equals(IdSeparator));
                for (var i = 0; i < count - 1; i++)
                {
                    prefix += levelPrefix;
                }
                selectList.Add(new SelectListItem
                {
                    Text = string.Format("{0} {1}", prefix, menu.GetPropertyValue(textFieldName)),
                    Value = a.ToString(CultureInfo.InvariantCulture)
                });
            }
            return selectList;
        }
        #endregion

        public static IFormatProvider DefaultFormat { get; set; }
    }
}
