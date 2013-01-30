namespace OpenCat.Models
{
    using MongoDB.Driver.GridFS;
    using System;

    public class Attachment : Entity
    {
        public string name { get; set; }
        public long size { get; set; }
        public string md5 { get; set; }
        public DateTime uploaded_at { get; set; }
        public string content_type { get; set; }

        public static Attachment FromFileInfo(MongoGridFSFileInfo info)
        {
            return new Attachment
            {
                id = info.Id.AsObjectId,
                name = info.Name,
                size = info.Length,
                md5 = info.MD5,
                uploaded_at = info.UploadDate,
                content_type = info.ContentType
            };
        }
    }
}