using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Westwind.Utilities;

namespace PX.Library.Common
{

    public static class CastingUtilities
    {
        /// <summary>
        /// Cast an object into dynamic object
        /// </summary>
        /// <typeparam name="TIn">Type of the source object</typeparam>
        /// <param name="entityIn">Object to cast</param>
        /// <param name="serializedProperties">List of property names to serialize</param>
        /// <param name="ignoreProperties">List of property names to ignore</param>
        /// <returns></returns>
        public static dynamic CastObjects<TIn>(TIn entityIn, IEnumerable<string> serializedProperties = null, IEnumerable<string> ignoreProperties = null)
        {
            dynamic clay = new Expando();

            var sourceProperties = entityIn.GetType().GetProperties();

            var propertiesList = serializedProperties != null
                                     ? sourceProperties.Where(x => serializedProperties.Contains(x.Name))
                                     : sourceProperties;

            foreach (var sourceProp in propertiesList)
            {
                if (ignoreProperties != null && ignoreProperties.Contains(sourceProp.Name))
                    continue;

                var propName = sourceProp.Name;

                try
                {
                    clay[propName] = sourceProp.GetValue(entityIn);
                }
                catch
                {
                    // do nothing
                }
            }

            return clay;
        }
    }
}
