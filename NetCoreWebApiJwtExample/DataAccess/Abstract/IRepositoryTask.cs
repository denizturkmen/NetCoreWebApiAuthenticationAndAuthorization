using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreWebApiJwtExample.DataAccess.Abstract
{
    public interface IRepositoryTask<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();
        Task<TEntity> GetById(int id);

        Task Create(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);

    }
}
