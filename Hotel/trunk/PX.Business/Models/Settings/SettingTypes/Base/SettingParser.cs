using System.Collections.Specialized;
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

        public bool ConvertFormDataToBoolean(NameValueCollection data, string key)
        {
            var valueString = data[key].Replace("true,false", "true");
            return valueString.ToType<bool>();
        }
    }
}