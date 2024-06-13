using Repository.Entities;
using Repository.Interface;
using Service.Interface;
using Microsoft.AspNetCore.Hosting;


namespace Service
{
    public class ProductService :IProductService
    {
        private IProductRepository _repo;
        public ProductService(IProductRepository repo)
        {
            _repo = repo;
        }

        public List<Product> GetProducts(string keyword, int pageNumber, int pageSize)
        {
            var product = _repo.Get().ToList();

            if (keyword != null && keyword.Length > 0)
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    product = product.Where(x =>
                    x.ProductId.ToString().Contains(keyword.ToLower()) ||
                    x.ProductName.ToLower().Contains(keyword.ToLower())).ToList();
                }
            }

            var totalProducts = product.Count();
            var totalPages = pageSize > 0 ? (int)Math.Ceiling((double)totalProducts / pageSize) : 0;

            if (pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }

            if (pageNumber > 0 && pageSize > 0)
            {
                product = product.OrderBy(x => x.ProductId).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }

            return product;
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
                throw new Exception($"Product with id: {product.ProductId} doesn't exists");
            }

        }


        public void AddProduct(Product product)
        {
            var existingProduct = _repo.Get(product.ProductId);
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
            var existingProduct = _repo.Get(product.ProductId);
            if (existingProduct != null)
            {
                product.ProductId = existingProduct.ProductId;
                _repo.Update(product);
                _repo.Save();
            }
            else
            {
                throw new Exception($"Product with id: {product.ProductId} doesn't exists");
            }

        }
    }
}
