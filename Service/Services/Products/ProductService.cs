
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Models;

namespace Service.Services.Products
{
    public class ProductService : IProductService
    {
        private IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
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
	}
}
