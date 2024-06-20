using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Models;

namespace Service.Services.ProductMaterialServices
{
    public class ProductMaterialService : IProductMaterialService
    {
        private IProductMaterialRepository _repo;
        public ProductMaterialService(IProductMaterialRepository repo)
        {
            _repo = repo;
        }
        public Task AddProductMaterialAsync(ProductMaterial productMaterial)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductMaterialAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductMaterial>> GetProductMaterialsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductMaterialAsync(ProductMaterial productMaterial)
        {
            throw new NotImplementedException();
        }
    }
}
