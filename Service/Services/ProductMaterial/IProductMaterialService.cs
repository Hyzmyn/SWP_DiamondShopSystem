using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IProductMaterialService
    {
        Task<List<ProductMaterial>> GetProductMaterialsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy);
        Task DeleteProductMaterialAsync(int id);
        Task AddProductMaterialAsync(ProductMaterial productMaterial);
        Task UpdateProductMaterialAsync(ProductMaterial productMaterial);
        Task<decimal> GetMaterialPrice(decimal Weight);

    }
}
