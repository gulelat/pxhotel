using System;
using System.Data;
using System.Data.Common;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using PX.Core.Configurations;
using PX.Core.Framework.Mvc.Models;
using PX.EntityModel.Repositories.RepositoryBase.Extensions;

namespace PX.EntityModel.Repositories.RepositoryBase
{
    public class Repository<T> where T : class
    {
        #region Protected Properties

        protected PXHotelEntities DataContext { get; set; }

        #endregion

        public Repository(PXHotelEntities entities)
        {
            DataContext = entities;
        }

        #region Public Methods

        public DbConnection Connection()
        {
            return DataContext.Database.Connection;
        }

        public IQueryable<T> GetAll()
        {
            return DataContext.Set<T>();
        }

        public IQueryable<T> Fetch(Expression<Func<T, bool>> expression)
        {
            return DataContext.Set<T>().Where(expression);
        }

        public T FetchFirst(Expression<Func<T, bool>> expression)
        {
            return DataContext.Set<T>().FirstOrDefault(expression);
        }

        public T GetById(object id)
        {
            return DataContext.Set<T>().Find(id);
        }

        public ResponseModel ExcuteSql(string sql)
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

        public ResponseModel Update(T entity)
        {
            var response = new ResponseModel();
            entity.SetProperty("Updated", DateTime.Now);
            if (entity.GetPropertyValue("UpdatedBy") == null)
            {
                entity.SetProperty("UpdatedBy", HttpContext.Current.User == null
                                                    ? Configurations.DefaultSystemAccount
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

        public ResponseModel Insert(T entity)
        {
            entity.SetProperty("Created", DateTime.Now);
            entity.SetProperty("CreatedBy", HttpContext.Current.User == null
                                                ? Configurations.DefaultSystemAccount
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

        public ResponseModel Delete(T entity)
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

        public ResponseModel Delete(object id)
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

        public ResponseModel InactiveRecord(int id)
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

        private string BuildEntityValidationError(DbEntityValidationException exception)
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

        public IFormatProvider DefaultFormat { get; set; }
    }
}
