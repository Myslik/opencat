namespace OpenCat.Data
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using OpenCat.Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected MongoServer Server { get; set; }
        public MongoDatabase Database { get; set; }
        public MongoCollection<TEntity> Collection { get; set; }

        public Repository()
        {
            Server = new MongoClient().GetServer();
            Database = Server.GetDatabase("OpenCAT");
            Collection = Database.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual IQueryable<TEntity> Get()
        {
            return Collection.FindAll().AsQueryable();
        }

        public virtual TEntity Get(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            return Collection.Find(query).FirstOrDefault();
        }

        public virtual TEntity Create(TEntity entity)
        {
            entity.id = ObjectId.GenerateNewId().ToString();
            entity.created_at = DateTime.UtcNow;
            entity.updated_at = DateTime.UtcNow;
            Collection.Insert(entity);
            return entity;
        }

        public virtual bool Edit(string id, TEntity entity)
        {
            IMongoQuery query = Query.EQ("_id", id);
            
            IMongoUpdate update;
            if (entity.fields == null)
            {
                entity.id = id;
                entity.updated_at = DateTime.UtcNow;
                update = Update.Replace<TEntity>(entity);
            }
            else
            {
                var updates = new List<IMongoUpdate>();
                var document = entity.ToBsonDocument();
                foreach (var field in entity.fields)
                {                    
                    updates.Add(Update.Set(field, document[field]));
                }
                updates.Add(Update.Set("updated_at", DateTime.UtcNow));
                update = Update.Combine(updates);
            }

            WriteConcernResult result = Collection.Update(query, update);
            return result.UpdatedExisting;
        }

        public virtual bool Delete(string id)
        {
            IMongoQuery query = Query.EQ("_id", id);
            WriteConcernResult result = Collection.Remove(query);
            return result.DocumentsAffected == 1;
        }
    }
}