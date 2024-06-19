using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<int> GetMaxIdAsync();
        Task CreateAsync(TEntity entity);

        void Delete(TEntity entity);

        IQueryable<TEntity> Get();

        Task<TEntity?> GetAsync<TKey>(TKey id);

        void Update(TEntity entity);

        Task SaveAsync();

        IQueryable<TEntity> QueryStart();

    }
}
