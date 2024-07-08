using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(string keyword, int pageNumber, int pageSize, string sortBy);
		Task DeleteProductAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        public List<Product> GetProducts();
        public List<Product> GetAllProducts();

        Task CalculateAndSaveProductPricesAsync();



        Task<Product> GetProductByIdAsync(int id);
        Task<Gem> GetGemByProductIdAsync(int id);
		Task<Product> GetProductByIdAsync2(int id);
        Task<GemPriceList> GetGemPriceListByProductIdAsync(int id);
        Task<List<Product>> GetProductsByFieldAsync(string? productCode, string? color, string? clarity, string? cut, decimal? startPrice, decimal? endPrice, int pageNumber, int pageSize);
        int GetTotalProductsByField(string? productCode, string? color, string? clarity, string? cut, decimal? startPrice, decimal? endPrice);
    }
}
