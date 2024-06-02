using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IProductRepository
    {
        public List<Product> GetAll();

        public Product? GetById(int ProductID);

        public void Create(Product product);

        public void Update(Product product);

        public void Delete(int ProductID);
        
        
    }
}
