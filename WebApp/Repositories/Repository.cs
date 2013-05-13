namespace OpenCat.Models
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using UpdateBuilder = MongoDB.Driver.Builders.Update;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Configuration;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        public MongoDatabase Database { get; private set; }
        public MongoCollection<TEntity> Collection { 
            get { return Database.GetCollection<TEntity>(typeof(TEntity).Name); } 
        }

        public Repository(MongoDatabase database)
        {
            Database = database;
        }

        protected IEnumerable<string> Ignore
        {
            get { return new string[] { "_id", "id", "created_at", "updated_at" }; }
        }

        public virtual IQueryable<TEntity> Read()
        {
            return Collection.FindAll().AsQueryable();
        }

        public virtual TEntity Read(string id)
        {
            var query = Query.EQ("_id", id);
            return Collection.Find(query).FirstOrDefault();
        }

        public virtual TEntity Create(TEntity entity)
        {
            entity.id = ObjectId.GenerateNewId().ToString();
            var now = DateTime.UtcNow;
            entity.created_at = now;
            entity.updated_at = now;
            Collection.Insert(entity);
            return entity;
        }

        public virtual bool Update(string id, TEntity entity)
        {
            var query = Query.EQ("_id", id);

            var updates = new List<IMongoUpdate>();
            var document = entity.ToBsonDocument();
            var properties = (entity.properties ?? document.Elements.Select(e => e.Name)).Except(Ignore);

            foreach (var property in properties)
            {
                updates.Add(UpdateBuilder.Set(property, document[property]));
            }
            if (updates.Count > 0)
            {
                updates.Add(UpdateBuilder.Set("updated_at", DateTime.UtcNow));
            }

            var result = Collection.Update(query, UpdateBuilder.Combine(updates));
            return result.UpdatedExisting;
        }

        public virtual bool Delete(string id)
        {
            var query = Query.EQ("_id", id);
            var result = Collection.Remove(query);
            return result.DocumentsAffected == 1;
        }
    }
}