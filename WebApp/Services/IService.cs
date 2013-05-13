using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCat.Models;

namespace OpenCat.Services
{
    public interface IService<TEntity> where TEntity : Entity
    {
        TEntity Create(TEntity entity);
        IQueryable<TEntity> Read();
        TEntity Read(string id);
        bool Update(string id, TEntity entity);
        bool Delete(string id);
    }
}
