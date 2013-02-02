namespace OpenCat
{
    using MongoDB.Driver;
    using OpenCat.Data;
    using OpenCat.Models;

    public class DataContext
    {
        public MongoServer Server { get; private set; }
        public MongoDatabase Database { get; private set; }
        public Generator Generator { get; private set; }

        public DataContext()
        {
            var client = new MongoClient();
            Server = client.GetServer();
            Database = Server.GetDatabase("OpenCAT");
            Generator = new Generator(this);
        }
    }
}