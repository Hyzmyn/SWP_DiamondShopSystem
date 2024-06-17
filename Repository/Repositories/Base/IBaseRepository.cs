using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        public int GetMaxId();
        public void Create(TEntity entity);
        public void Delete(TEntity entity);
        public IQueryable<TEntity> Get();
        public TEntity? Get<TKey>(TKey id);
        public void Update(TEntity entity);
        public void Save();
        public IQueryable<TEntity> QueryStart();

    }
}
