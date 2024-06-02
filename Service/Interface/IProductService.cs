using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IProductService
    {
        public List<Product> GetAllProduct();

        public Product? GetProduct(int id);

        public List<Product> SearchProduct(string keyword);

        public void AddProduct(Product product);

        public void UpdateProduct(Product product);

        public void DeleteProduct(int id);
    }
}
