using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _dbContext;

        private readonly DbSet<TEntity> _dbSet;

        private bool _disposed = false;
        public BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<TEntity> Get()
        {
            return _dbSet;
        }

        public TEntity? Get<TKey>(TKey id)
        {
            return _dbSet.Find(id);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }
    }
}
