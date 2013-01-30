namespace OpenCat.Models
{
    using System.Collections.Generic;

    public class DTO
    {
        public Document document { get; set; }
        public IEnumerable<Document> documents { get; set; }
        public Attachment attachment { get; set; }
        public IEnumerable<Attachment> attachments { get; set; }
        public Dictionary<string, string> errors { get; set; }
    }
}