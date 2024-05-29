using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class ProductRepository
    {
        private DiamondShopContext _db;
        public List<Product> GetAll()
        {
            _db = new();
            return _db.Products.ToList();
        }
        public Product? GetById(int ProductID)
        {
            _db = new();
            return _db.Products.FirstOrDefault();
        }
        public void Create(Product product)
        {
            _db = new();
            _db.Products.Add(product);
            _db.SaveChanges();
        }
        public void Update(Product product)
        {
            _db = new();
            _db.Products.Update(product);
            _db.SaveChanges();
        }
        public void Delete(int ProductID)
        {
            _db = new();
            var Product = _db.Products.FirstOrDefault(x => x.ProductId == ProductID);
            if (Product != null)
            {
                _db.Products.Remove(Product);
                _db.SaveChanges();
            }
        }
    }
}