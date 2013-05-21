using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using OpenCat.Models;
using OpenCat.Services;

namespace WebApp.Specs
{
    public static class BusinessHelper
    {
        private static Lazy<MongoDatabase> database = new Lazy<MongoDatabase>(() =>
        {
            var dbName = ConfigurationManager.AppSettings["dbName"];
            var server = new MongoClient().GetServer();
            return server.GetDatabase(dbName);
        });

        public static MongoDatabase Database
        {
            get { return database.Value; }
        }

        public static AttachmentService GetAttachmentService()
        {
            return new AttachmentService(Database);
        }

        public static JobService GetJobService()
        {
            return new JobService(Database);
        }

        public static void UploadFileToJob(string name, string filename, Stream content)
        {
            var attachments = BusinessHelper.GetAttachmentService();
            var jobs = BusinessHelper.GetJobService();
            var job = jobs.Read().Where(j => j.name == name).First();
            attachments.Upload(job.id, new OpenCat.UploadedFile(filename, content));
        }
    }
}
