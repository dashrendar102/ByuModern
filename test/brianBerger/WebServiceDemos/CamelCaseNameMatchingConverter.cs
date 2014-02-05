using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebServiceDemos
{
    // originally based on http://stackoverflow.com/questions/19792274/alternate-property-name-while-deserializing
    //when deserializing JSON, matches JSON properties named in underscore form to camel case (case insensitive), 
    //example - class_schedule => classSchedule, which will also match ClassSchedule
    public class CamelCaseNameMatchingConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType.IsClass;
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            object instance = objectType.GetConstructor(Type.EmptyTypes).Invoke(null);
            PropertyInfo[] props = objectType.GetProperties();
            var foo = objectType.GetCustomAttributes();

            JObject jo = JObject.Load(reader);
            foreach (JProperty jp in jo.Properties())
            {
                string camelName = underScoreToCamelCase(jp.Name);

                PropertyInfo prop = props.FirstOrDefault(pi =>
                    {
                        if (pi.CanWrite && string.Equals(pi.Name, camelName, StringComparison.OrdinalIgnoreCase))
                        {
                            return true;
                        }
                        var attr = pi.GetCustomAttribute<JsonPropertyAttribute>();
                        return attr != null && string.Equals(attr.PropertyName, jp.Name);
                    });

                if (prop != null)
                    prop.SetValue(instance, jp.Value.ToObject(prop.PropertyType, serializer));
            }

            return instance;
        }

        private string underScoreToCamelCase(string text)
        {
            string result = Regex.Replace(text, "_([a-z])", match => match.Groups[1].Value.ToUpper());
            return result;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
