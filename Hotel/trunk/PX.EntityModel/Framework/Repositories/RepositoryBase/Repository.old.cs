using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Reflection;
using System.Web;
using PX.EntityModel.Framework.Repositories.RepositoryBase.Models;

namespace PX.EntityModel.Framework.Repositories.RepositoryBase
{
    public abstract class Repository<T> : IDisposable
    {
        #region Private Methods
        /// <summary>
        /// Attempts to set a named property of an entity to an arbitrary value. The value is set if the property is found.
        /// </summary>
        /// <typeparam name="T">An entity deriving of type EntityObject.</typeparam>
        /// <param name="entityToSet">The instance of the entity whose value will be set.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="value">The value of the property to set.</param>
        private static void SetProperty<T>(T entityToSet, string propertyName, object value) where T : EntityObject
        {
            PropertyInfo targetProperty = entityToSet.GetType().GetProperty(propertyName);
            if (targetProperty != null)
                targetProperty.SetValue(entityToSet, value, null);
        }

        #endregion

        #region Protected Properties

        // this static context is a bad, bad thing, but the only things that hit this will be


        public static HotelEntities StaticContext
        {
            get;
            set;
        }

        protected static HotelEntities DataContext
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return StaticContext ?? (StaticContext = new HotelEntities());
                }

                if (HttpContext.Current.Items["HotelEntities"] == null)
                    HttpContext.Current.Items["HotelEntities"] = new HotelEntities();

                return (HotelEntities)HttpContext.Current.Items["HotelEntities"];
            }
        }

        #endregion

        #region Protected Methods
        /// <summary>
        /// Given a string containing the name of an entity, this method returns the name of the entity collection the entity belongs to.
        /// </summary>
        /// <param name="entityTypeName">Name of the entity type.</param>
        /// <returns></returns>
        protected static string GetEntitySetName(string entityTypeName)
        {
            EntityContainer container = DataContext.MetadataWorkspace.GetEntityContainer(DataContext.DefaultContainerName, DataSpace.CSpace);
            return (from meta in container.BaseEntitySets where meta.ElementType.Name == entityTypeName select meta.Name).First();
        }

        /// <summary>
        /// Given an instance of an entity, this method returns the name of the entity collection the entity belongs to.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        protected static string GetEntitySetName<T>(T entity) where T : EntityObject
        {
            return GetEntitySetName(entity.GetType().Name);
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Saves all the changes to an IEnumerable group of entities.
        /// </summary>
        /// <param name="objectsToSave">An IEnumerable of type EntityObject.</param>
        /// <returns>Returns the list that was passed, allowing for examining individual save status'.</returns>
        public IEnumerable<HotelEntityObject> SaveCollection(IEnumerable<EntityObject> objectsToSave)
        {
            var entities = new List<HotelEntityObject>();
            lock (objectsToSave)
            {
                foreach (var entityObject in objectsToSave)
                    entities.Add(SaveChanges(entityObject));

                return entities;
            }
        }

        /// <summary>
        /// Saves the changes to an Entity of type T.
        /// </summary>
        /// <typeparam name="T">An object that inherits from EntityObject.</typeparam>
        /// <param name="entityToSave">The instance of the Entity to save.</param>
        /// <returns>The original entity that was passed in, now saved.</returns>
        public static HotelEntityObject SaveChanges<T>(T entityToSave) where T : EntityObject
        {
            lock (entityToSave)
            {
                var entitySetName = GetEntitySetName(entityToSave);
                var keyValue = entityToSave.GetType().GetProperty("Id").GetValue(entityToSave, null);
                if (entityToSave.EntityKey == null)
                {
                    entityToSave.EntityKey = new EntityKey("CDLGEntities." + entitySetName, "Id", keyValue);
                }

                if (entityToSave.EntityKey != null && (entityToSave.EntityKey.EntityKeyValues == null || Convert.ToInt32(entityToSave.EntityKey.EntityKeyValues[0].Value) == 0))
                {
                    SetProperty(entityToSave, "Created", DateTime.Now);
                    if (User.CurrentUser != null)
                    {
                        SetProperty(entityToSave, "Created", User.CurrentUser.Id);
                    }

                    DataContext.AddObject(entitySetName, entityToSave);
                }
                else
                {
                    EntityKey key = entityToSave.EntityKey ?? DataContext.CreateEntityKey(entitySetName, entityToSave);

                    try
                    {
                        SetProperty(entityToSave, "DateUpdated", DateTime.Now);
                        if(User.CurrentUser != null)
                        {
                            SetProperty(entityToSave, "UserUpdated", User.CurrentUser.Id);
                        }
                        var attachedEntity = (EntityObject)DataContext.GetObjectByKey(key);
                        DataContext.ApplyCurrentValues(attachedEntity.EntityKey.EntitySetName, entityToSave);
                    }
                    catch (ObjectNotFoundException)
                    {
                        SetProperty(entityToSave, "DateCreated", DateTime.Now);
                        DataContext.AddObject(entitySetName, entityToSave);
                    }
                }

                try
                {
                    DataContext.SaveChanges();

                    return new HotelEntityObject
                        {
                            EntityObject = entityToSave,
                            Success = true
                        };
                }
                catch (Exception exception)
                {
                    return new HotelEntityObject
                    {
                        EntityObject = entityToSave,
                        Success = false,
                        Message = exception.Message
                    };
                }
            }
        }

        public static EntityKey CreateEntityKey(string entitySetName, object entityToSave)
        {
            return DataContext.CreateEntityKey(entitySetName, entityToSave);
        }

        /// <summary>
        /// Deletes an instance of an entity from the database.
        /// </summary>
        /// <param name="objectToDelete"></param>
        public static HotelEntityObject Delete(EntityObject objectToDelete)
        {
            try
            {
                // Take the object in the arg and retrieve it from the repo.
                var attachedEntity = (EntityObject)DataContext.GetObjectByKey(objectToDelete.EntityKey);

                if (attachedEntity != null)
                {
                    DataContext.DeleteObject(attachedEntity);
                    DataContext.SaveChanges();
                }
                return new HotelEntityObject
                    {
                        Success = true
                    };
            }
            catch (Exception exception)
            {
                return new HotelEntityObject
                {
                    Success = false,
                    Message = exception.Message
                };

            }

        }
        #endregion

        #region IDisposable Implementation
        public void Dispose()
        { }
        #endregion
    }
}
