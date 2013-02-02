namespace OpenCat.ApiControllers
{
    using OpenCat.Data;
    using OpenCat.Models;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class JobsController : ApiController
    {
        public Repository<Job> Repository { get; set; }

        public JobsController()
        {
            Repository = new Repository<Job>();
        }

        public DTO Get()
        {
            return new DTO { jobs = Repository.Get() };
        }

        public DTO Get(string id)
        {
            var job = Repository.Get(id);
            if (job == null) throw new HttpResponseException(HttpStatusCode.NotFound);
            return new DTO { job = job };
        }

        public DTO Post(DTO dto)
        {
            Repository.Create(dto.job);
            return dto;
        }

        public DTO Put(string id, DTO dto)
        {
            var ok = Repository.Edit(id, dto.job);
            if (!ok) throw new HttpResponseException(HttpStatusCode.BadRequest);
            return dto;
        }

        public void Delete(string id)
        {
            Repository.Delete(id);
        }
    }
}
