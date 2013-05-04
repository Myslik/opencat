namespace OpenCat.ApiControllers
{
    using MongoDB.Bson;
    using MongoDB.Driver.Builders;
    using OpenCat.Data;
    using OpenCat.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Http;

    public class AttachmentsController : ApiController
    {
        private Repository<Job> Jobs { get; set; }

        public AttachmentsController()
        {
            Jobs = new Repository<Job>();
        }

        public IEnumerable<Attachment> Get([FromUri] string[] ids)
        {
            if (ids.Length > 0)
            {
                var query = Query.In("_id", new BsonArray(ids.Select(id => ObjectId.Parse(id))));
                return Jobs.Database.GridFS.Find(query).Select(info => Attachment.FromFileInfo(info));
            }
            return Jobs.Database.GridFS.FindAll().Select(info => Attachment.FromFileInfo(info));
        }

        public Attachment Get(string id)
        {
            var file = Jobs.Database.GridFS.FindOne(Query.EQ("_id", id));
            return Attachment.FromFileInfo(file);
        }

        public void Delete(string id)
        {
            Jobs.Collection.Update(Query.Exists("attachment_ids"), Update.Pull("attachment_ids", id));
            Jobs.Database.GridFS.DeleteById(ObjectId.Parse(id));
        }
    }
}
