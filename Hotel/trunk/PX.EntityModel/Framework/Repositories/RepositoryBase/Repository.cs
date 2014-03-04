using System;
using System.Linq;
using System.Web;

namespace PX.EntityModel.Framework.Repositories.RepositoryBase
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

        public static T Update(T entity)
        {
            SetProperty(entity, "Updated", DateTime.Now);
            if (User.CurrentUser != null)
            {
                SetProperty(entity, "UpdatedBy", User.CurrentUser.Id);
            }

            var dbSet = DataContext.Set<T>();
            dbSet.Add(entity);
            DataContext.SaveChanges();
            return entity;
        }

        public static T Insert(T entity)
        {
            SetProperty(entity, "Created", DateTime.Now);
            if (User.CurrentUser != null)
            {
                SetProperty(entity, "Created", User.CurrentUser.Id);
            }
            var dbSet = DataContext.Set<T>();
            dbSet.Add(entity);
            DataContext.SaveChanges();
            return entity;
        }

        public static bool Delete(T entity)
        {
            var dbSet = DataContext.Set<T>();
            dbSet.Remove(entity);
            DataContext.SaveChanges();
            return true;
        }

        #endregion

    }
}
