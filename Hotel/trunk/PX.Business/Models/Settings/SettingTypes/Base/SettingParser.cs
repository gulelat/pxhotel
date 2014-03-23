using System.Web.Script.Serialization;

namespace PX.Business.Models.Settings.SettingTypes.Base
{
    public class SettingParser<T>
    {
        public T DeserializeSetting(string settingString)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Deserialize<T>(settingString);
        }

        public string SerializeSetting(T setting)
        {
            var jsonSerializer = new JavaScriptSerializer();
            return jsonSerializer.Serialize(setting);
        }
    }
}