using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
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

        public async Task<int> GetMaxIdAsync()
        {
            if (await _dbSet.AnyAsync())
            {
                int maxId = await _dbSet.MaxAsync(u => EF.Property<int>(u, "ID"));
                return maxId;
            }
            return 0;
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<TEntity> Get()
        {
            return _dbSet;
        }

        public async Task<TEntity?> GetAsync<TKey>(TKey id)
        {
            return await _dbSet.FindAsync(id);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<TEntity> QueryStart()
        {
            return _dbSet.AsQueryable();
        }
    }
}
