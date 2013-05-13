namespace OpenCat.ApiControllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;
    using OpenCat.Models;
    using OpenCat.Services;

    public class JobsController : ApiController
    {
        public JobService Jobs { get; set; }

        public JobsController(JobService service)
        {
            Jobs = service;
        }

        public IEnumerable<Job> Get()
        {
            return Jobs.Read();
        }

        public Job Get(string id)
        {
            var job = Jobs.Read(id);
            if (job == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return job;
        }

        public Job Post(Job job)
        {
            return Jobs.Create(job);
        }

        public void Put(string id, Job job)
        {
            var ok = Jobs.Update(id, job);
            if (!ok) throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        public void Delete(string id)
        {
            var ok = Jobs.Delete(id);
            if (!ok) throw new HttpResponseException(HttpStatusCode.BadRequest);
        }
    }
}
