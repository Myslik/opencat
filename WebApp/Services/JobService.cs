using OpenCat.Models;

namespace OpenCat.Services
{
    public class JobService : Service<Job>
    {
        public JobService(IRepository<Job> repository) : base(repository) { }
    }
}