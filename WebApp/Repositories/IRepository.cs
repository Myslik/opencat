namespace OpenCat.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using MongoDB.Driver;

    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity Create(TEntity entity);
        IQueryable<TEntity> Read();
        IEnumerable<TEntity> Read(string[] ids);
        TEntity Read(string id);
        bool Update(string id, TEntity entity);
        bool Delete(string id);

        MongoCollection<TEntity> Collection { get; }
        MongoDatabase Database { get; }
    }
}
