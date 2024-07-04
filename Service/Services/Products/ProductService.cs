
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Repository.Repositories;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Service.Services
{

	public class ProductService : IProductService
	{
		private IProductRepository _repo;
		private IGemPriceListRepository _gemPriceListRepo;
		private IMaterialPriceListRepository _materialPriceListRepo;
		private IPriceRateListService _priceRateListService;


		public ProductService(IProductRepository repo, IGemPriceListRepository gemPriceListRepository, IMaterialPriceListRepository materialPriceList, IPriceRateListService priceRateListService)
		{
			_repo = repo;
			_gemPriceListRepo = gemPriceListRepository;
			_materialPriceListRepo = materialPriceList;
			_priceRateListService = priceRateListService;
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
			var existingProduct = await _repo.GetAsync(product.ProductID);
			if (existingProduct != null)
			{
				product.ProductID = existingProduct.ProductID;
				_repo.Update(product);
				await _repo.SaveAsync();
			}
			else
			{
				product = null;
			}
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

		public async Task CalculateAndSaveProductPricesAsync()
		{
			var products = await _repo.Get().ToListAsync();
			var materialPriceLists = await _materialPriceListRepo.Get().ToListAsync();
			var priceRate = await _priceRateListService.GetPriceRate();
			var gemPrices = await _gemPriceListRepo.Get().ToListAsync();

			foreach (var product in products)
			{
				// Get the applicable material price
				var applicableMaterialPrice = materialPriceLists
					.Where(mpl => mpl.EffDate <= DateTime.Now)
					.OrderByDescending(mpl => mpl.EffDate)
					.FirstOrDefault();

				// Get the gem price by GemID
				var applicableGemPrice = gemPrices
					.Where(gp => gp.GemID == product.GemID)
					.FirstOrDefault();

				if (applicableMaterialPrice != null && applicableGemPrice != null && priceRate.HasValue)
				{
					product.TotalCost = (applicableMaterialPrice.SellPrice + applicableGemPrice.Price + product.ProductionCost) + 
						((applicableMaterialPrice.SellPrice + applicableGemPrice.Price + product.ProductionCost) * (priceRate.Value / 100));
				}
			}

			await _repo.SaveAsync();
		}

	}
}
