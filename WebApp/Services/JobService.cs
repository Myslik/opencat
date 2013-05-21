using MongoDB.Driver;
using OpenCat.Models;

namespace OpenCat.Services
{
    public class JobService : Service<Job>
    {
        public JobService(MongoDatabase database) : base(database) { }
    }
}