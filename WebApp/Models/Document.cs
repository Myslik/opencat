namespace OpenCat.Models
{
    using System;

    public class Document : Entity
    {
        public string name { get; set; }
        public int words { get; set; }
    }
}