namespace OpenCat.Models
{
    using MongoDB.Bson;
    using System;

    public class Entity
    {
        public ObjectId id { get; set; }
        public DateTime created_at { get; set; }
    }
}