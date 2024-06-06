using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IProductGemRepository
    {
        List<ProductGem> GetAll();
        ProductGem? GetById(int id);
        public int GetMaxUserId();
        void Create(ProductGem pGem);
        void Update(ProductGem pGem);
        void Delete(int id);
    }
}
