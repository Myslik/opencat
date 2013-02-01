namespace OpenCat.Models
{
    using MongoDB.Bson;
    using System;
    using System.Collections.Generic;

    public class Document : Entity
    {
        public string name { get; set; }
        public string description { get; set; }
        public int words { get; set; }
        public IList<ObjectId> attachments { get; set; }
    }
}