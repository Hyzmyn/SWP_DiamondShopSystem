
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
        public async Task<List<Product>> GetProductByField(string? productCode, string? origin, string? color, string? clarity, string? cut, decimal? startPrice, decimal? endPrice, int pageNumber, int pageSize)
        {
            var query = _db.Products
                .Include(p => p.Gems)
                    .ThenInclude(g => g.GemPriceList)
                .AsQueryable();

            if (!string.IsNullOrEmpty(productCode))
            {
                query = query.Where(p => p.ProductCode.Contains(productCode));
            }

            if (!string.IsNullOrEmpty(origin))
            {
                query = query.Where(p => p.Gems.GemPriceList.Origin.Contains(origin));
            }

            if (!string.IsNullOrEmpty(color))
            {
                query = query.Where(p => p.Gems.GemPriceList.Color.Contains(color));
            }

            if (!string.IsNullOrEmpty(clarity))
            {
                query = query.Where(p => p.Gems.GemPriceList.Clarity.Contains(clarity));
            }

            if (!string.IsNullOrEmpty(cut))
            {
                query = query.Where(p => p.Gems.GemPriceList.Cut.Contains(cut));
            }

            if (startPrice.HasValue)
            {
                query = query.Where(p => p.TotalCost >= startPrice.Value);
            }

            if (endPrice.HasValue)
            {
                query = query.Where(p => p.TotalCost <= endPrice.Value);
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public int GetTotalProductByField(string? productCode, string? origin, string? color, string? clarity, string? cut, decimal? startPrice, decimal? endPrice)
        {
            var query = _db.Products
                .Include(p => p.Gems)
                    .ThenInclude(g => g.GemPriceList)
                .AsQueryable();

            if (!string.IsNullOrEmpty(productCode))
            {
                query = query.Where(p => p.ProductCode.Contains(productCode));
            }

            if (!string.IsNullOrEmpty(origin))
            {
                query = query.Where(p => p.Gems.GemPriceList.Origin.Contains(origin));
            }

            if (!string.IsNullOrEmpty(color))
            {
                query = query.Where(p => p.Gems.GemPriceList.Color.Contains(color));
            }

            if (!string.IsNullOrEmpty(clarity))
            {
                query = query.Where(p => p.Gems.GemPriceList.Clarity.Contains(clarity));
            }

            if (!string.IsNullOrEmpty(cut))
            {
                query = query.Where(p => p.Gems.GemPriceList.Cut.Contains(cut));
            }

            if (startPrice.HasValue)
            {
                query = query.Where(p => p.TotalCost >= startPrice.Value);
            }

            if (endPrice.HasValue)
            {
                query = query.Where(p => p.TotalCost <= endPrice.Value);
            }

            return query.Count();
        }

    }
}
