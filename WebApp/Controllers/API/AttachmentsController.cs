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
        private Repository<Document> Documents { get; set; }

        public AttachmentsController()
        {
            Context = new DataContext();
            Documents = new Repository<Document>(Context);
        }

        public DTO Get([FromUri] string[] ids)
        {
            if (ids != null)
            {
                var query = Query.In("_id", BsonArray.Create(ids.Select(id => ObjectId.Parse(id))));
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
            Context.Database.GridFS.DeleteById(ObjectId.Parse(id));
        }
    }
}
