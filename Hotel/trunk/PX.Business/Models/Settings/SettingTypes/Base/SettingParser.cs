using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Script.Serialization;
using PX.Core.Ultilities;

namespace PX.Business.Models.Settings.SettingTypes.Base
{
    public class SettingParser<T>
    {
        public T DeserializeSetting(string settingString)
        {
            var type = typeof (T);
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Deserialize<T>(settingString);
        }

        public string SerializeSetting(T setting)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(setting);
        }

        public string GetSettingValue(NameValueCollection data)
        {
            var setting = MappingData(data);
            return SerializeSetting(setting);
        }

        public T MappingData(NameValueCollection formData)
        {
            var form = formData.ToString();

            var setting  = Activator.CreateInstance<T>();
            var type = typeof (T);
            var properties = type.GetProperties();
            foreach (var propertyInfo in properties)
            {
                var propertyValue = formData[propertyInfo.Name];
                if(propertyValue != null)
                {
                    var propertyType = propertyInfo.PropertyType.Name;
                    switch (propertyType)
                    {
                        case "Boolean":
                            ReflectionUtilities.SetProperty(setting, propertyInfo.Name, ConvertBooleanData(propertyValue));
                            break;
                        case "Int32":
                            ReflectionUtilities.SetProperty(setting, propertyInfo.Name, Convert.ToInt32(propertyValue));
                            break;
                    }
                }
            }
            return setting;
        }

        public bool ConvertBooleanData(string data)
        {
            var valueString = data.Replace("true,false", "true");
            return valueString.ToType<bool>();
        }
    }
}