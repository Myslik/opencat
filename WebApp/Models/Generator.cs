namespace OpenCat.Models
{
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;

    public class Counter
    {
        public string id { get; set; }
        public int seed { get; set; }
    }

    public class Generator
    {
        private DataContext context;
        public Generator(DataContext context)
        {
            this.context = context;
        }

        private MongoCollection<Counter> collection;
        private MongoCollection<Counter> Collection
        {
            get
            {
                if (collection == null)
                {
                    if (!context.Database.CollectionExists("Counters"))
                    {
                        context.Database.CreateCollection("Counters");
                    }
                    collection = context.Database.GetCollection<Counter>("Counters");
                }
                return collection;
            }
        }

        public int Get<T>()
        {
            var result = Collection.FindAndModify(Query.EQ("_id", typeof(T).Name), SortBy.Null, Update.Inc("seed", 1), true, true);
            return result.GetModifiedDocumentAs<Counter>().seed;
        }
    }
}