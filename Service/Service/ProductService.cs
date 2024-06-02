using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductService
    {
        private ProductRepository _repo = new ProductRepository();

        public List<Product> GetAllProduct() => _repo.GetAll();

        public Product? GetProduct(int id) => _repo.GetById(id);

        public List<Product> SearchProduct(string keyword) => _repo.GetAll().Where(x => x.ProductId.ToString().Contains(keyword.ToLower())).ToList();

        public void AddProduct(Product product) => _repo.Create(product);

        public void UpdateProduct(Product product) => _repo.Update(product);

        public void DeleteProduct(int id) => _repo.Delete(id);



    }
}
