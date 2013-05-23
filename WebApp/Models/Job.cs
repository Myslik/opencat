namespace OpenCat.Models
{
    using System;
    using System.Collections.Generic;
    using MongoDB.Bson.Serialization.Attributes;

    public class Job : Entity
    {
        public Job()
        {
            attachment_ids = new List<String>();
            unit_ids = new List<String>();
        }

        public string name { get; set; }
        public string description { get; set; }
        public int words { get; set; }

        public IList<String> attachment_ids { get; private set; }
        public IList<String> unit_ids { get; private set; }
    }
}