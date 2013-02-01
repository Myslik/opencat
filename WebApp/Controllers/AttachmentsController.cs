namespace OpenCat.Controllers
{
    using MongoDB.Bson;
    using MongoDB.Driver.Builders;
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

        [HttpPost]
        public ActionResult Upload()
        {
            return this.UploadFiles(files =>
            {
                var gfs = Context.Database.GridFS;
                var document = new Document
                {
                    name = DateTime.Now.Ticks.ToString(),
                    words = 0,
                    attachments = new List<ObjectId>()
                };
                foreach (var file in files)
                {
                    var info = gfs.Upload(file.InputStream, file.FileName);
                    document.attachments.Add(info.Id.AsObjectId);
                }
                Documents.Create(document);
            });
        }

        [HttpPost]
        public ActionResult UploadToDocument(string id)
        {
            return this.UploadFiles(files =>
            {
                var gfs = Context.Database.GridFS;
                var document = Documents.Get(id);
                foreach (var file in files)
                {
                    var info = gfs.Upload(file.InputStream, file.FileName);
                    document.attachments.Add(info.Id.AsObjectId);
                }
                Documents.Edit(id, document);
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
