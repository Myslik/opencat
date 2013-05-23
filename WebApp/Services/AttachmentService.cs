using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using OpenCat.Models;
using OpenCat.Parsers;
using OpenCat.Plugins;
using UpdateBuilder = MongoDB.Driver.Builders.Update;

namespace OpenCat.Services
{
    public class AttachmentService : Service<Attachment>
    {
        protected IRepository<Job> Jobs { get { return GetRepository<Job>(); } }
        protected IRepository<Unit> Units { get { return GetRepository<Unit>(); } }

        public AttachmentService(MongoDatabase database)
            : base(database) { }

        private static Attachment FromFileInfo(MongoGridFSFileInfo info)
        {
            var attachment = new Attachment
            {
                id = info.Id.ToString(),
                name = info.Name,
                size = info.Length,
                md5 = info.MD5,
                uploaded_at = info.UploadDate,
                content_type = info.ContentType,
                info = info
            };
            if (info.Metadata.Contains("job_id"))
            {
                attachment.job_id = info.Metadata["job_id"].ToString();
            }
            return attachment;
        }

        public override Attachment Read(string id)
        {
            var info = Repository.Database.GridFS.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
            return FromFileInfo(info);
        }

        public override IEnumerable<Attachment> Read(string[] ids)
        {
            var query = Query.In("_id", new BsonArray(ids.Select(id => ObjectId.Parse(id))));
            return Repository.Database.GridFS.Find(query).Select(info => FromFileInfo(info));
        }

        public override IQueryable<Attachment> Read()
        {
            return Repository.Database.GridFS.FindAll().Select(info => FromFileInfo(info)).AsQueryable();
        }

        public override Attachment Create(Attachment entity)
        {
            throw new NotImplementedException();
        }

        public override bool Update(string id, Attachment entity)
        {
            throw new NotImplementedException();
        }

        public void Upload(string job_id, UploadedFile file)
        {
            var gfs = Repository.Database.GridFS;
            var options = new MongoGridFSCreateOptions
            {
                Metadata = new BsonDocument(new BsonElement("job_id", job_id))
            };
            var info = gfs.Upload(file.InputStream, file.FileName, options);
            var attachment = FromFileInfo(info);

            var query = Query.EQ("_id", ObjectId.Parse(job_id));
            var update = UpdateBuilder.AddToSet("attachment_ids", attachment.id);
            Jobs.Collection.Update(query, update);

            var units = Plugin.TryParse(attachment);
            if (units.Count() > 0)
            {
                var ids = new List<string>();
                foreach (var unit in units)
                {
                    ids.Add(Units.Create(unit).id);
                }
                var units_update = UpdateBuilder.AddToSetEach("unit_ids", new BsonArray(ids));
                Jobs.Collection.Update(query, units_update);
            }
        }

        public override bool Delete(string id)
        {
            var query = Query.Exists("attachment_ids");
            var update = UpdateBuilder.Pull("attachment_ids", id);
            Jobs.Collection.Update(query, update);
            Repository.Database.GridFS.DeleteById(ObjectId.Parse(id));
            return true;
        }
    }
}