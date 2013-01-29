namespace OpenCat.Models
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using System.Collections.Generic;
    using System.Linq;

    public class Repository<T> where T : Entity
    {
        public DataContext Context { get; set; }
        private MongoCollection<T> collection;
        public MongoCollection<T> Collection
        {
            get
            {
                if (collection == null)
                {
                    if (!Context.Database.CollectionExists(typeof(T).Name))
                    {
                        Context.Database.CreateCollection(typeof(T).Name);
                    }
                    collection = Context.Database.GetCollection<T>(typeof(T).Name);
                }
                return collection;
            }
        }

        public Repository()
        {
            Context = new DataContext();
        }

        public Repository(DataContext context)
        {
            Context = context;
        }

        public T Get(ObjectId id)
        {
            return Collection.FindOneById(id);
        }

        public T Get(string id)
        {
            return Get(ObjectId.Parse(id));
        }

        public IEnumerable<T> Get()
        {
            var result = Collection.FindAll().AsQueryable();

            return result;
        }

        public void Create(T entity)
        {
            Collection.Insert(entity);
        }

        public void Edit(ObjectId id, T entity)
        {
            entity.id = id;
            var query = Query.EQ("_id", id);
            var update = Update.Replace<T>(entity);
            Collection.Update(query, update);
        }

        public void Edit(string id, T entity)
        {
            Edit(ObjectId.Parse(id), entity);
        }

        public void Delete(ObjectId id)
        {
            var query = Query.EQ("_id", id);
            Collection.Remove(query);
        }

        public void Delete(string id)
        {
            Delete(ObjectId.Parse(id));
        }
    }
}