using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OpenCat
{
    public class DataContext
    {
        private MongoServer Server { get; set; }
        private MongoDatabase Database { get; set; }

        public DataContext()
        {
            var client = new MongoClient();
            Server = client.GetServer();
            Database = Server.GetDatabase("OpenCAT");
        }
    }
}