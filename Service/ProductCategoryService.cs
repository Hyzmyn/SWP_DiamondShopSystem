using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ProductCategoryService
    {
        private ProductCategoryRepository _repo = new ProductCategoryRepository();

        public List<ProductCategory> GetAllProductCate() => _repo.GetAll();

        public List<ProductCategory> SreachProductCate(string keyword) 
            => _repo.GetAll().Where(x => x.CategoryId.ToString().Contains(keyword.ToLower())  ||
                                         x.CategoryName.ToLower().Contains(keyword.ToLower())).ToList();

        public ProductCategory? GetProductCate(int id) => _repo.GetById(id);

        public void UpdateProductCate(ProductCategory category) => _repo.Update(category);
    }
}
