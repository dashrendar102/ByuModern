using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Common.WebServices
{
    // originally based on http://stackoverflow.com/questions/19792274/alternate-property-name-while-deserializing
    //when deserializing JSON, matches JSON properties named in underscore form to camel case (case insensitive), 
    //example - class_schedule => classSchedule, which will also match ClassSchedule
    internal sealed class CamelCaseNameMatchingConverter : JsonConverter
    {
        public sealed override bool CanConvert(Type objectType)
        {
            return objectType.GetTypeInfo().IsClass;
        }

        public sealed override bool CanWrite
        {
            get { return false; }
        }

        public sealed override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var constructor = objectType.GetTypeInfo().DeclaredConstructors.Where<ConstructorInfo>(c => c.GetParameters().Length == 0).FirstOrDefault();
            object instance = null;
            if (constructor != null)
            {
                instance = constructor.Invoke(null);
                //objectType.
            //object instance = objectType.GetConstructor(Type.EmptyTypes).Invoke(null);
                var props = objectType.GetTypeInfo().DeclaredProperties;
            //var foo = objectType.GetCustomAttributes();

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
            }

            return instance;
        }

        private string underScoreToCamelCase(string text)
        {
            string result = Regex.Replace(text, "_([a-z])", match => match.Groups[1].Value.ToUpper());
            return result;
        }

        public sealed override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
