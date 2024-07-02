
using Repository.Interface;
using Repository.Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.ProductCatergories
{
    public class ProductCategoryService : IProductCategoryService
    {
        private IProductCategoryRepository _repo;
        public ProductCategoryService(IProductCategoryRepository repo)
        {
            _repo = repo;
        }

        public Task AddProductCatergoryAsync(ProductCategory productCatergory)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductCatergoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductCategory>> GetProductCatergorysAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductCatergoryAsync(ProductCategory productCatergory)
        {
            throw new NotImplementedException();
        }
    }
}
