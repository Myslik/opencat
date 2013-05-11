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
        private JobRepository Jobs { get; set; }

        public AttachmentsController()
        {
            Jobs = new JobRepository();
        }

        [NonAction]
        private Job CreateJob()
        {
            var job = new Job
            {
                name = DateTime.Now.Ticks.ToString(),
                words = 0
            };
            Jobs.Create(job);
            return job;
        }

        [NonAction]
        private Job UploadFile(Job job, UploadedFile file)
        {
            var gfs = Jobs.Database.GridFS;
            var options = new MongoGridFSCreateOptions
            {
                Metadata = new BsonDocument(new BsonElement ("job_id", job.id))
            };
            var info = gfs.Upload(file.InputStream, file.FileName, options);
            job.attachment_ids.Add(info.Id.ToString());
            return job;
        }

        [HttpPost]
        public ActionResult Upload()
        {
            return this.UploadFiles(file =>
            {
                var job = CreateJob();
                job = UploadFile(job, file);
                Jobs.Update(job.id, job);
            });
        }

        [HttpPost]
        public ActionResult UploadToJob(string id)
        {
            return this.UploadFiles(file =>
            {
                var job = Jobs.Read(id);
                job = UploadFile(job, file);
                Jobs.Update(job.id, job);
            });
        }

        [HttpGet]
        public ActionResult Download(string id)
        {
            var file = Jobs.Database.GridFS.FindOne(Query.EQ("_id", ObjectId.Parse(id)));
            return File(file.OpenRead(), "application/octet-stream", file.Name);
        }
    }
}
