using System.Linq;
using OpenCat.Models;

namespace OpenCat.Data
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        IQueryable<TEntity> Get();
        TEntity Get(string id);
        TEntity Create(TEntity entity);
        bool Edit(string id, TEntity entity);
        bool Delete(string id);
    }
}
