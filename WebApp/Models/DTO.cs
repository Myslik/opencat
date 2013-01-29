namespace OpenCat.Models
{
    using System.Collections.Generic;

    public class DTO
    {
        public Document document { get; set; }
        public IEnumerable<Document> documents { get; set; }
        public Dictionary<string, string> errors { get; set; }
    }
}