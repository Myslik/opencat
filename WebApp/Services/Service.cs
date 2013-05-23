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
        protected MongoDatabase Database { get; private set; }
        protected IRepository<TEntity> Repository
        {
            get { return GetRepository<TEntity>(); }
        }

        protected IRepository<T> GetRepository<T>() where T : Entity
        {
            return new Repository<T>(Database);
        }

        public Service(MongoDatabase database)
        {
            Database = database;
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

        public virtual IEnumerable<TEntity> Read(string[] ids)
        {
            return Repository.Read(ids);
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