
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repositories;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Service.Services
{

	public class ProductService : IProductService
	{
		private IProductRepository _repo;
		private IGemPriceListService _gemPriceListService;
		private IMaterialPriceListRepository _materialPriceListRepository;


		public ProductService(IProductRepository repo, IGemPriceListService gemPriceListService, IMaterialPriceListRepository materialPriceList)
		{
			_repo = repo;
			_gemPriceListService = gemPriceListService;
			_materialPriceListRepository = materialPriceList;
		}

		public async Task AddProductAsync(Product product)
		{
			throw new NotImplementedException();
		}

		public async Task DeleteProductAsync(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Product>> GetProductsAsync(string keyword, int pageNumber, int pageSize, string sortBy)
		{
			int defaultPageSize = 10;
			if (pageNumber <= 0 && pageSize <= 0)
			{
				pageSize = defaultPageSize;
				pageNumber = 1;
			}
			IQueryable<Product> product = _repo.QueryStart();
			if (!string.IsNullOrEmpty(keyword))
			{
				product = product.Where(x =>
					x.ProductID.ToString().Contains(keyword.ToLower()) ||
					x.ProductName.ToLower().Contains(keyword.ToLower()));
			}
			product = product.OrderBy(x => x.ProductName);
			if (!string.IsNullOrEmpty(sortBy))
			{
				switch (sortBy)
				{
					case "product_desc":
						product = product.OrderByDescending(x => x.ProductName);
						break;
				}
			}
			var productlist = await product.OrderBy(x => x.ProductID)
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
			var totalUsers = productlist.Count;
			var totalPages = pageSize > 0 ? (int)Math.Ceiling((double)totalUsers / pageSize) : 0;
			if (pageNumber > totalPages)
			{
				pageNumber = totalPages;
			}
			return productlist;
		}

		public async Task UpdateProductAsync(Product product)
		{
			throw new NotImplementedException();
		}

		public List<Product> GetProducts()
		{
			return _repo.GetProducts();
		}
		public List<Product> GetAllProducts()
		{
			return _repo.GetAllProduct();
		}
		public async Task<Product> GetProductByIdAsync(int id)
		{
			return await _repo.GetProductByIdAsync(id);

		}
		public async Task<Gem> GetGemByProductIdAsync(int id)
		{
			return await _repo.GetGemByProductIdAsync(id);
		}
        public async Task<GemPriceList> GetGemPriceListByProductIdAsync(int id)
        {
            return await _repo.GetGemPriceListByProductIdAsync(id);
        }
        public async Task<Product> GetProductByIdAsync2(int id)
		{
			return await _repo.GetProductByIdAsync2(id);

		}
        public async Task<List<Product>> GetProductsByFieldAsync(string? productCode, string? origin, string? color, string? clarity, string? cut, decimal? startPrice, decimal? endPrice, int pageNumber, int pageSize)
        {
            int defaultPageSize = 10;

            if (pageNumber <= 0 || pageSize <= 0)
            {
                pageSize = defaultPageSize;
                pageNumber = 1;
            }

            List<Product> productList;

            if (!string.IsNullOrEmpty(productCode) || !string.IsNullOrEmpty(origin) || !string.IsNullOrEmpty(color) || !string.IsNullOrEmpty(clarity) || !string.IsNullOrEmpty(cut) || startPrice.HasValue || endPrice.HasValue)
            {
                productList = await _repo.GetProductByField(productCode, origin, color, clarity, cut, startPrice, endPrice, pageNumber, pageSize);
            }
            else
            {
                productList = _repo.GetAllProduct();
            }
            return productList;
        }

        public int GetTotalProductsByField(string? productCode, string? origin, string? color, string? clarity, string? cut, decimal? startPrice, decimal? endPrice)
        {
            return _repo.GetTotalProductByField(productCode, origin, color, clarity, cut, startPrice, endPrice);
        }

        //public async Task<decimal> GetProductPrice(decimal Weight, string cut, decimal carat, string color, string clarity)
        //{
        //	var gemPrice = await _gemPriceListService.GetDiamondPrice(cut, carat, color, clarity);
        //	var materialPrice = await _materialPriceListRepository.GetMaterialPrice(Weight);

        //	return gemPrice + materialPrice;
        //}
    }
}
