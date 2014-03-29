using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using PX.Core.Configurations.Constants;
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

        public static ResponseModel ExcuteSql(string sql)
        {
            var response = new ResponseModel();
            try
            {
                DataContext.Database.ExecuteSqlCommand(sql);
                DataContext.SaveChanges();
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

        public static ResponseModel Update(T entity)
        {
            var response = new ResponseModel();
            entity.SetProperty("Updated", DateTime.Now);
            if (entity.GetPropertyValue("UpdatedBy") == null)
            {
                entity.SetProperty("UpdatedBy", HttpContext.Current.User == null
                                                    ? DefaultConstants.DefaultSystemAccount
                                                    : HttpContext.Current.User.Identity.Name);
            }
            try
            {
                DataContext.SaveChanges();
                response.Data = entity.GetId();
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
            entity.SetProperty("CreatedBy", HttpContext.Current.User == null
                                                ? DefaultConstants.DefaultSystemAccount
                                                : HttpContext.Current.User.Identity.Name);
            entity.SetProperty("RecordActive", true);

            var response = new ResponseModel();
            try
            {
                var dbSet = DataContext.Set<T>();
                dbSet.Add(entity);
                DataContext.SaveChanges();
                response.Data = entity.GetId();
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

        public static IFormatProvider DefaultFormat { get; set; }
    }
}
