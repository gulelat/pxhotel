using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PX.Core.Ultilities
{
    public static class SerializeUtilities
    {
        private static readonly JavaScriptSerializer JavaScriptSerializer;

        static SerializeUtilities()
        {
            JavaScriptSerializer = new JavaScriptSerializer();
        }

        /// <summary>
        /// Serialize object to json string
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string SerializeObject(object model)
        {
            return JavaScriptSerializer.Serialize(model);
        }

        /// <summary>
        /// Deserialize object from string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T DeserializeString<T>(string json)
        {
            return JavaScriptSerializer.Deserialize<T>(json);
        }
    }
}
