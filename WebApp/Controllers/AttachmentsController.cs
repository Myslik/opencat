namespace OpenCat.Controllers
{
    using MongoDB.Bson;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.GridFS;
    using OpenCat.Data;
    using OpenCat.Models;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Mvc;

    public class AttachmentsController : Controller
    {
        private DataContext Context { get; set; }
        private Repository<Document> Documents { get; set; }

        public AttachmentsController()
        {
            Context = new DataContext();
            Documents = new Repository<Document>(Context);
        }

        [NonAction]
        private Document CreateDocument()
        {
            var document = new Document
            {
                name = DateTime.Now.Ticks.ToString(),
                words = 0,
                attachments = new List<ObjectId>()
            };
            Documents.Create(document);
            return document;
        }

        [NonAction]
        private Document UploadFile(Document document, UploadedFile file)
        {
            var gfs = Context.Database.GridFS;
            var options = new MongoGridFSCreateOptions
            {
                Metadata = new BsonDocument(new BsonElement ("document_id", document.id))
            };
            var info = gfs.Upload(file.InputStream, file.FileName, options);
            if (document.attachments == null) document.attachments = new List<ObjectId>();
            document.attachments.Add(info.Id.AsObjectId);
            return document;
        }

        [HttpPost]
        public ActionResult Upload()
        {
            return this.UploadFiles(files =>
            {
                var document = CreateDocument();
                foreach (var file in files)
                {
                    document = UploadFile(document, file);
                }
                Documents.Edit(document.id, document);
            });
        }

        [HttpPost]
        public ActionResult UploadToDocument(string id)
        {
            return this.UploadFiles(files =>
            {
                var document = Documents.Get(id);
                foreach (var file in files)
                {
                    document = UploadFile(document, file);
                }
                Documents.Edit(document.id, document);
            });
        }

        [HttpGet]
        public ActionResult Download(string id)
        {
            ObjectId parsedId;
            if (!ObjectId.TryParse(id, out parsedId)) 
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            var file = Context.Database.GridFS.FindOne(Query.EQ("_id", parsedId));
            return File(file.OpenRead(), "application/octet-stream", file.Name);
        }
    }
}
