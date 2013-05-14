namespace OpenCat.Controllers
{
    using MongoDB.Bson;
    using MongoDB.Driver.Builders;
    using MongoDB.Driver.GridFS;
    using OpenCat.Models;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Mvc;
    using OpenCat.Services;

    public class AttachmentsController : Controller
    {
        private JobService Jobs { get; set; }
        private AttachmentService Attachments { get; set; }

        public AttachmentsController(JobService jobs, AttachmentService attachments)
        {
            Jobs = jobs;
            Attachments = attachments;
        }

        [HttpPost]
        public ActionResult Upload()
        {
            return this.UploadFiles(file =>
            {
                var job = Jobs.Create(new Job
                {
                    name = DateTime.Now.Ticks.ToString()
                });
                Attachments.Upload(job.id, file);
            });
        }

        [HttpPost]
        public ActionResult UploadToJob(string id)
        {
            return this.UploadFiles(file =>
            {
                Attachments.Upload(id, file);
            });
        }

        [HttpGet]
        public ActionResult Download(string id)
        {
            var attachment = Attachments.Read(id);
            return File(attachment.OpenRead(), "application/octet-stream", attachment.name);
        }
    }
}
