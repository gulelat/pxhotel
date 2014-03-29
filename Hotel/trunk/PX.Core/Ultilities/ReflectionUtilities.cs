using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PX.Core.Ultilities
{
    public class ReflectionUtilities
    {
        /// <summary>
        /// Get all types that implement an interface
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        public static IEnumerable<Type> GetAllImplementTypesOfInterface(Type interfaceType)
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(x => interfaceType.IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);
        }


        /// <summary>
        /// Get attribute of member
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static T GetAttribute<T>(MemberInfo memberInfo) where T : class
        {
            var customAttributes = memberInfo.GetCustomAttributes(typeof(T), false);
            var attribute = customAttributes.First(a => a is T) as T;
            return attribute;
        }
    }
}
