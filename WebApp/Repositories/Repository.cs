namespace OpenCat.Data
{
    using MongoDB.Bson;
    using MongoDB.Driver;
    using MongoDB.Driver.Builders;
    using UpdateBuilder = MongoDB.Driver.Builders.Update;
    using OpenCat.Models;
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Configuration;

    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        public MongoDatabase Database { get { return Collection.Database; } }
        public MongoCollection<TEntity> Collection { get; private set; }

        public Repository()
        {
            var dbName = ConfigurationManager.AppSettings["dbName"];
            Collection = new MongoClient().GetServer().GetDatabase(dbName).GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public virtual IQueryable<TEntity> Read()
        {
            return Collection.FindAll().AsQueryable();
        }

        public virtual TEntity Read(string id)
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

        public virtual bool Update(string id, TEntity entity)
        {
            IMongoQuery query = Query.EQ("_id", id);
            
            IMongoUpdate update;
            if (entity.fields == null)
            {
                entity.id = id;
                entity.updated_at = DateTime.UtcNow;
                update = UpdateBuilder.Replace<TEntity>(entity);
            }
            else
            {
                var ignored = new string[] { "_id", "id", "created_at", "updated_at" };

                var updates = new List<IMongoUpdate>();
                var document = entity.ToBsonDocument();
                foreach (var field in entity.fields)
                {
                    if (ignored.Contains(field)) continue;

                    updates.Add(UpdateBuilder.Set(field, document[field]));
                }
                updates.Add(UpdateBuilder.Set("updated_at", DateTime.UtcNow));
                update = UpdateBuilder.Combine(updates);
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