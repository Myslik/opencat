using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using OpenCat.Models;

namespace OpenCat.Services
{
    public class UnitService : Service<Unit>
    {
        public UnitService(IRepository<Unit> repository) : base(repository) { }

        public IEnumerable<Unit> Read(string[] ids)
        {
            var query = Query.In("_id", new BsonArray(ids.Select(id => ObjectId.Parse(id))));
            return Repository.Collection.Find(query).AsEnumerable();
        }
    }
}