using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IProductGemService
    {
        Task<List<ProductGem>> GetProductGemsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy);
        Task DeleteProductGemAsync(int id);
        Task AddProductGemAsync(ProductGem productGem);
        Task UpdateProductGemAsync(ProductGem productGem);  
    }
}
