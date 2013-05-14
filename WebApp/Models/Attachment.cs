namespace OpenCat.Models
{
    using MongoDB.Bson;
    using MongoDB.Driver.GridFS;
    using System;
    using Newtonsoft.Json;
    using System.IO;

    public class Attachment : Entity
    {
        public string name { get; set; }
        public long size { get; set; }
        public string md5 { get; set; }
        public DateTime uploaded_at { get; set; }
        public string content_type { get; set; }
        public string job_id { get; set; }

        [JsonIgnore]
        public MongoGridFSFileInfo info { get; set; }
        public Stream OpenRead()
        {
            return info == null ? Stream.Null : info.OpenRead();
        }
    }
}