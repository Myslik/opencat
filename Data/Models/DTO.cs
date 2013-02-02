namespace OpenCat.Models
{
    using System.Collections.Generic;

    public class DTO
    {
        // Job
        public Job job { get; set; }
        public IEnumerable<Job> jobs { get; set; }
        // Attachment
        public Attachment attachment { get; set; }
        public IEnumerable<Attachment> attachments { get; set; }
        // Language
        public Language language { get; set; }
        public IEnumerable<Language> languages { get; set; }
        // Errors
        public Dictionary<string, string> errors { get; set; }
    }
}