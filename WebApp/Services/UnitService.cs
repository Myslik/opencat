using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using OpenCat.Models;

namespace OpenCat.Services
{
    public class UnitService : Service<Unit>
    {
        public UnitService(MongoDatabase database) : base(database) { }
    }
}