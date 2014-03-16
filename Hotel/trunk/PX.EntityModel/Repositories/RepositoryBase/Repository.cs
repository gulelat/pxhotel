using System;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using PX.Core.Framework.Mvc.Models;

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

        #region Private Methods
        /// <summary>
        /// Attempts to set a named property of an entity to an arbitrary value. The value is set if the property is found.
        /// </summary>
        /// <typeparam name="T">An entity deriving of type EntityObject.</typeparam>
        /// <param name="entityToSet">The instance of the entity whose value will be set.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="value">The value of the property to set.</param>
        private static void SetProperty<T>(T entityToSet, string propertyName, object value)
        {
            var targetProperty = entityToSet.GetType().GetProperty(propertyName);
            if (targetProperty != null)
                targetProperty.SetValue(entityToSet, value, null);
        }

        #endregion

        #region Public Methods

        public static IQueryable<T> GetAll()
        {
            return DataContext.Set<T>();
        }

        public static T GetById(int id)
        {
            return DataContext.Set<T>().Find(id);
        }


        public static ResponseModel Update(T entity)
        {
            SetProperty(entity, "Updated", DateTime.Now);
            if (User.CurrentUser != null)
            {
                SetProperty(entity, "UpdatedBy", User.CurrentUser.Id);
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
                var message = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    message += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        message += string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                response.Message = message;
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
            SetProperty(entity, "Created", DateTime.Now);
            if (User.CurrentUser != null)
            {
                SetProperty(entity, "CreatedBy", User.CurrentUser.Id);
            }
            else
            {
                SetProperty(entity, "CreatedBy", string.Empty);
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
                var message = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    message += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        message += string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                response.Message = message;
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
                var message = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    message += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        message += string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                response.Message = message;
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }

        public static ResponseModel Delete(int id)
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
                var message = string.Empty;
                foreach (var eve in e.EntityValidationErrors)
                {
                    message += string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        message += string.Format("- Property: \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage);
                    }
                }
                response.Message = message;
            }
            catch (Exception exception)
            {
                response.Success = false;
                response.Message = exception.Message;
            }
            return response;
        }

        #endregion

    }
}
