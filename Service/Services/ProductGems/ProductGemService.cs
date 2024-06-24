using Repository.Interface;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{

    public class ProductGemService : IProductGemService
    {
        private IProductGemRepository _repo;


        public ProductGemService(IProductGemRepository repo)
        {
            _repo = repo;
        }
        public Task AddProductGemAsync(ProductGem productGem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductGemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ProductGem>> GetProductGemsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductGemAsync(ProductGem productGem)
        {
            throw new NotImplementedException();
        }
    }
}
