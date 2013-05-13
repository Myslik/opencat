using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using OpenCat.Models;

namespace OpenCat.Services
{
    public class Service<TEntity> : IService<TEntity> where TEntity : Entity
    {
        protected IRepository<TEntity> Repository { get; private set; }
        public MongoDatabase Database { get { return Repository.Database; } }
        public MongoCollection<TEntity> Collection { get { return Repository.Collection; } }

        public Service(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual TEntity Create(TEntity entity)
        {
            return Repository.Create(entity);
        }

        public virtual IQueryable<TEntity> Read()
        {
            return Repository.Read();
        }

        public virtual TEntity Read(string id)
        {
            return Repository.Read(id);
        }

        public virtual bool Update(string id, TEntity entity)
        {
            return Repository.Update(id, entity);
        }

        public virtual bool Delete(string id)
        {
            return Repository.Delete(id);
        }
    }
}