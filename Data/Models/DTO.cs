namespace OpenCat.Models
{
    using System.Collections.Generic;

    public class DTO
    {
        // User
        public User user { get; set; }
        public IEnumerable<User> users { get; set; }
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