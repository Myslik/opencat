using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Formatting;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenCat.Models;

namespace OpenCat.Formatters
{
    public class EmberJsonMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public override System.Threading.Tasks.Task<object> ReadFromStreamAsync(Type type, System.IO.Stream readStream, System.Net.Http.HttpContent content, IFormatterLogger formatterLogger)
        {
            return base.ReadFromStreamAsync(typeof(JObject), readStream, content, formatterLogger).ContinueWith<object>((task) =>
            {
                var data = task.Result as JObject;
                var prefix = type.Name.ToLower();

                if (data[prefix] == null)
                {
                    return GetDefaultValueForType(type);
                }

                var fields = (data[prefix] as JObject).Properties().Select(p => p.Name);

                var serializer = JsonSerializer.Create(SerializerSettings);

                Entity entity = data[prefix].ToObject(type, serializer) as Entity;
                entity.fields = fields.ToArray();
                return entity;
            });
        }

        public override System.Threading.Tasks.Task WriteToStreamAsync(Type type, object value, System.IO.Stream writeStream, System.Net.Http.HttpContent content, System.Net.TransportContext transportContext)
        {
            var wrapper = new Dictionary<string, object>();
            if (value is IEnumerable)
            {
                wrapper.Add(type.GenericTypeArguments.First().Name.ToLower().Pluralize(), value);
            }
            else
            {
                wrapper.Add(type.Name.ToLower(), value);
            }
            return base.WriteToStreamAsync(type, wrapper, writeStream, content, transportContext);
        }
    }

    public static class StringExtension
    {
        public static string Pluralize(this string text)
        {
            return text + "s";
        }
    }
}