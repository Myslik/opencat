namespace OpenCat.Models
{
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

        public T Get(int id)
        {
            return Collection.FindOneById(id);
        }

        public IEnumerable<T> Get()
        {
            return Collection.FindAll().AsQueryable();
        }

        public void Create(T entity)
        {
            entity.id = Context.Generator.Get<T>();
            Collection.Insert(entity);
        }

        public void Edit(T entity)
        {
            var query = Query.EQ("_id", entity.id);
            var update = Update.Replace<T>(entity);
            Collection.Update(query, update, UpdateFlags.None);
        }

        public void Delete(int id)
        {
            var query = Query.EQ("_id", id);
            Collection.Remove(query);
        }
    }
}