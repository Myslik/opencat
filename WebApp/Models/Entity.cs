namespace OpenCat.Models
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization.Attributes;

    public interface IPartialUpdate
    {
        IEnumerable<String> properties { get; }
    }

    public class Entity : IPartialUpdate
    {
        public String id { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

        [BsonIgnore]
        public IEnumerable<String> properties { get; set; }
    }
}