﻿namespace OpenCat.ApiControllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using OpenCat.Data;
    using OpenCat.Models;

    public class JobsController : ApiController
    {
        public JobRepository Repository { get; set; }

        public JobsController()
        {
            Repository = new JobRepository();
        }

        public IEnumerable<Job> Get()
        {
            return Repository.Get();
        }

        public Job Get(string id)
        {
            var job = Repository.Get(id);
            if (job == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return job;
        }

        public Job Post(Job job)
        {
            return Repository.Create(job);
        }

        public void Put(string id, Job job)
        {
            var ok = Repository.Edit(id, job);
            if (!ok) throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public void Delete(string id)
        {
            var ok = Repository.Delete(id);
            if (!ok) throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}
