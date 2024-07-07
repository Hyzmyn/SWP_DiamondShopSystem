
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private DiamondShopContext _db;

        public ProductRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }
        public List<Product> GetProducts()
        {
            var products = _db.Products
                                     .OrderByDescending(p => p.ProductID)
                                     .Take(10)
                                     .ToList();
            return products;
        }
        public List<Product> GetAllProduct()
        {
            var products = _db.Products.ToList();
            return products;
        }
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _db.Products.FindAsync(id);
        }
        public async Task<Product> GetProductByIdAsync2(int id)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.ProductID == id);
        }

        public async Task<Gem> GetGemByProductIdAsync(int gemId)
        {
            return await _db.Gems.FirstOrDefaultAsync(g => g.GemID == gemId);
        }
		public async Task<GemPriceList> GetGemPriceListByProductIdAsync(int gemId)
		{
			var gemPriceList = await _db.GemPriceLists.FirstOrDefaultAsync(g => g.GemID == gemId);
			if (gemPriceList != null)
			{
			
				Console.WriteLine($"GemPriceList Data: {gemPriceList.CaratWeight}, {gemPriceList.Color}, {gemPriceList.Clarity}, {gemPriceList.Cut}");
			}
			else
			{
				Console.WriteLine("GemPriceList Data: null");
			}
			return gemPriceList;
		}
		public async Task<List<Product>> GetProductByNameAndOrigin(string keyword, int pageNumber, int pageSize)
		{
			return await _db.Products
				.Include(p => p.Gems)
					.ThenInclude(g => g.GemPriceList)
				.Where(p => p.ProductName.Contains(keyword) ||
							p.Gems.GemPriceList.Origin.Contains(keyword) ||
							p.Gems.GemPriceList.Color.Contains(keyword) ||
							p.Gems.GemPriceList.Clarity.Contains(keyword) ||
							p.Gems.GemPriceList.Cut.Contains(keyword))
				.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize)
				.ToListAsync();
		}

		public int GetTotalProductsByNameAndOrigin(string keyword)
		{
			return _db.Products
				.Include(p => p.Gems)
					.ThenInclude(g => g.GemPriceList)
				.Where(p => p.ProductName.Contains(keyword) ||
							p.Gems.GemPriceList.Origin.Contains(keyword) ||
							p.Gems.GemPriceList.Color.Contains(keyword) ||
							p.Gems.GemPriceList.Clarity.Contains(keyword) ||
							p.Gems.GemPriceList.Cut.Contains(keyword))
				.Count();
		}
	}
}
