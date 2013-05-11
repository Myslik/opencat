using System.Linq;
using OpenCat.Models;

namespace OpenCat.Data
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        TEntity Create(TEntity entity);
        IQueryable<TEntity> Read();
        TEntity Read(string id);
        bool Update(string id, TEntity entity);
        bool Delete(string id);
    }
}
