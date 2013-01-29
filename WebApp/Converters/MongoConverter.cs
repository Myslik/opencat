namespace OpenCat.Converters
{
    using Newtonsoft.Json;

    public class MongoConverter : JsonConverter
    {
        public override bool CanConvert(System.Type objectType)
        {
            return objectType == typeof(MongoDB.Bson.ObjectId);
        }

        public override object ReadJson(JsonReader reader, System.Type objectType, object existingValue, JsonSerializer serializer)
        {
            return existingValue == typeof(string) ? MongoDB.Bson.ObjectId.Parse((string)existingValue) : existingValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}