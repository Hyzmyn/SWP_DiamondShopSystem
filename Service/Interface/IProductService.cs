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
        public List<Product> GetProducts(string keyword, int pageNumber, int pageSize);
        public void DeleteProduct(int id);
        public void AddProduct(Product product);
        public void UpdateProduct(Product product);
    }
}
