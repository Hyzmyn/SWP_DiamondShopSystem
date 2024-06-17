using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DiamondShopContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(DiamondShopContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public int GetMaxId()
        {
            if (_dbSet.Any())
            {
                int maxId = _dbSet.Max(u => EF.Property<int>(u, "ID"));
                return maxId;
            }
            return 0;
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

        public void Save()
        {
            _dbContext.SaveChanges();
        }

        public IQueryable<TEntity> QueryStart()
        {
            return _dbSet.AsQueryable();
        }
    }
}
