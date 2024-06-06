using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProductGemRepository : IProductGemRepository
    {
        private DiamondShopContext _db;

        public void Create(ProductGem pGem)
        {
            
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductGem> GetAll()
        {
            return _db.ProductGems.ToList();
        }

        public ProductGem? GetById(int id)
        {
            
        }

        public int GetMaxUserId()
        {
            throw new NotImplementedException();
        }

        public void Update(ProductGem pGem)
        {
            throw new NotImplementedException();
        }
    }
}
