using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProductCategoryRepository
    {
        private DiamondShopContext _db;

        public ProductCategoryRepository()
        {
            _db = new();
        }

        public List<ProductCategory> GetAll()
        {
            
            return _db.ProductCategories.ToList();
        }

        public ProductCategory? Get(int id)
        {
            
            return _db.ProductCategories.SingleOrDefault();
        }
        public ProductCategory? GetById(int id)
        {
            
            return _db.ProductCategories.Find(id);
        }

        public void Create(ProductCategory category)
        {
            

            _db.ProductCategories.Add(category);
            _db.SaveChanges();

        }

        public void Update(ProductCategory category)
        {
            
            _db.ProductCategories.Update(category);
            _db.SaveChanges();

        }
        public void Delete(int id)
        {
            
            var productCategory = _db.ProductCategories.FirstOrDefault(x => x.CategoryId == id);

            if (productCategory != null)
            {
                _db.ProductCategories.Remove(productCategory);
                _db.SaveChanges();
            }
        }
    }
}
