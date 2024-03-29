﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using PX.Core.Framework.Mvc.Attributes;
using PX.Core.Ultilities;

namespace PX.EntityModel.Repositories.RepositoryBase.Extensions
{
    public static class EntityExtensions
    {
        public static char IdSeparator = '.';
        public static string IdPropertyName = "Id";
        public static string ParentIdPropertyName = "ParentId";
        public static string HierarchyPropertyName = "Hierarchy";
        public static string DefaultDigitFormat = "D5";

        /// <summary>
        /// Get table name from entity
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GetTableName<T>(this T entity)
        {
            var type = typeof(T);
            var at = ReflectionUtilities.GetAttribute<TableAttribute>(type);
            return at.Name;
        }

        #region Hierarchy Methods

        #endregion

        /// <summary>
        /// Attempts to set a named property of an entity to an arbitrary value. The value is set if the property is found.
        /// </summary>
        /// <typeparam name="T">An entity deriving of type EntityObject.</typeparam>
        /// <param name="entityToSet">The instance of the entity whose value will be set.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="value">The value of the property to set.</param>
        public static void SetProperty<T>(this T entityToSet, string propertyName, object value)
        {
            var targetProperty = entityToSet.GetType().GetProperty(propertyName);
            if (targetProperty != null)
                targetProperty.SetValue(entityToSet, value, null);
        }

        /// <summary>
        /// Get value of property by name
        /// </summary>
        /// <param name="entity">the entity</param>
        /// <param name="propertyName">the property name</param>
        /// <returns></returns>
        public static object GetPropertyValue<T>(this T entity, string propertyName)
        {
            var targetProperty = entity.GetType().GetProperty(propertyName);
            if (targetProperty != null)
                return targetProperty.GetValue(entity);
            return new object();
        }

        /// <summary>
        /// Get Id property of entity
        /// </summary>
        /// <typeparam name="T">the entity type</typeparam>
        /// <param name="entity">the entity</param>
        /// <returns></returns>
        public static int GetId<T>(this T entity)
        {
            return (int)GetPropertyValue(entity, IdPropertyName);
        }

        /// <summary>
        /// Get Parent Id property of entity
        /// </summary>
        /// <typeparam name="T">the entity type</typeparam>
        /// <param name="entity">the entity</param>
        /// <returns></returns>
        public static int? GetParentId<T>(this T entity)
        {
            return (int?)GetPropertyValue(entity, ParentIdPropertyName);
        }

        /// <summary>
        /// Get Hierarchy property of entity
        /// </summary>
        /// <typeparam name="T">the entity type</typeparam>
        /// <param name="entity">the entity</param>
        /// <returns></returns>
        public static string GetHierarchy<T>(this T entity)
        {
            return (string)GetPropertyValue(entity, HierarchyPropertyName);
        }

        /// <summary>
        /// Get new hierarchy string
        /// </summary>
        /// <typeparam name="T">the entity type</typeparam>
        /// <param name="item">the item</param>
        /// <param name="parent">the parent item</param>
        /// <returns></returns>
        public static string CalculateHierarchyValue<T>(this T item, T parent)
        {
            return string.Concat(parent.GetHierarchy(), item.GetId().ToString(DefaultDigitFormat), IdSeparator);
        }

        /// <summary>
        /// Get hierarchy item of entity
        /// </summary>
        /// <typeparam name="T">the entity type</typeparam>
        /// <param name="item">the item</param>
        /// <returns></returns>
        public static string GetHierarchyValueForRoot<T>(this T item)
        {
            return string.Concat(IdSeparator, item.GetId().ToString(DefaultDigitFormat), IdSeparator);
        }

        /// <summary>
        /// Get hierarchy item of entity with id
        /// </summary>
        /// <param name="item">the item id</param>
        /// <returns></returns>
        public static string GetHierarchyValueForRoot(this int item)
        {
            return string.Concat(IdSeparator, item.ToString(DefaultDigitFormat), IdSeparator);
        }
    }
}
