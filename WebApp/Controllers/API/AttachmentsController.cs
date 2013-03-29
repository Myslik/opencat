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
        private DataContext Context { get; set; }
        private Repository<Job> Jobs { get; set; }

        public AttachmentsController()
        {
            Context = new DataContext();
            Jobs = new Repository<Job>(Context);
        }

        public DTO Get([FromUri] string[] ids)
        {
            if (ids.Length > 0)
            {
                var query = Query.In("_id", new BsonArray(ids.Select(id => ObjectId.Parse(id)).AsEnumerable()));
                return new DTO { attachments = Context.Database.GridFS.Find(query).Select(info => Attachment.FromFileInfo(info)) };
            }
            return new DTO { attachments = Context.Database.GridFS.FindAll().Select(info => Attachment.FromFileInfo(info)) };
        }

        public DTO Get(string id)
        {
            ObjectId parsedId;
            if (!ObjectId.TryParse(id, out parsedId))
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            var file = Context.Database.GridFS.FindOne(Query.EQ("_id", parsedId));
            return new DTO { attachment = Attachment.FromFileInfo(file) };
        }

        public void Delete(string id)
        {
            Jobs.Collection.Update(Query.Exists("attachments"), Update.Pull("attachments", new BsonObjectId(ObjectId.Parse(id))));
            Context.Database.GridFS.DeleteById(ObjectId.Parse(id));
        }
    }
}
