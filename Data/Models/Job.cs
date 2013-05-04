namespace OpenCat.Models
{
    using System;
    using System.Collections.Generic;

    public class Job : Entity
    {
        public string name { get; set; }
        public string description { get; set; }
        public int words { get; set; }
        public List<string> attachment_ids { get; set; }
    }
}