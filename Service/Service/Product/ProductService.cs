
using Repository.Interface;
using Repository.Models;
using Service.Interface;



namespace Service
{
    public class ProductService : IProductService
    {
        private IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

		public List<Product> GetProducts(string keyword, int pageNumber, int pageSize)
		{
			var products = _repo.Get().ToList();

			if (!string.IsNullOrEmpty(keyword))
			{
				products = products.Where(x =>
					x.ProductID.ToString().Contains(keyword.ToLower()) ||
					x.ProductName.ToLower().Contains(keyword.ToLower()))
					.ToList();
			}

			var totalProducts = products.Count();
			var totalPages = pageSize > 0 ? (int)Math.Ceiling((double)totalProducts / pageSize) : 0;

			if (pageNumber > totalPages)
			{
				pageNumber = totalPages;
			}

			if (pageNumber > 0 && pageSize > 0)
			{
				products = products
					.OrderBy(x => x.ProductID)
					.Skip((pageNumber - 1) * pageSize)
					.Take(pageSize)
					.ToList();
			}

			return products;
		}

		public void DeleteProduct(int id)
        {
            var product = _repo.Get(id);
            if (product != null)
            {
                _repo.Delete(product);
                _repo.Save();
            }
            else
            {
                throw new Exception($"Product with id: {product.ProductID} doesn't exists");
            }

        }


        public void AddProduct(Product product)
        {
            var existingProduct = _repo.Get(product.ProductID);
            if (existingProduct == null)
            {
                _repo.Create(product);
                _repo.Save();
            }
            else
            {
                throw new Exception($"Product already exists");
            }

        }

        public void UpdateProduct(Product product)
        {
            var existingProduct = _repo.Get(product.ProductID);
            if (existingProduct != null)
            {
                product.ProductID = existingProduct.ProductID;
                _repo.Update(product);
                _repo.Save();
            }
            else
            {
                throw new Exception($"Product with id: {product.ProductID} doesn't exists");
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
    }
}
