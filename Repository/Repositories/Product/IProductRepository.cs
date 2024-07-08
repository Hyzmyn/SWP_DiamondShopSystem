
using Repository.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IProductRepository : IBaseRepository<Product>
    {
		public List<Product> GetProducts();
		public List<Product> GetAllProduct();

		Task<Product> GetProductByIdAsync(int id);
		Task<Gem> GetGemByProductIdAsync(int gemId);
		Task<Product> GetProductByIdAsync2(int id);
		Task<GemPriceList> GetGemPriceListByProductIdAsync(int gemId);

		Task<List<Product>> GetProductByField(string? productCode, string? color, string? clarity, string? cut, decimal? startPrice, decimal? endPrice, int pageNumber, int pageSize);

		int GetTotalProductByField(string? productCode, string? color, string? clarity, string? cut, decimal? startPrice, decimal? endPrice);

    }
}
