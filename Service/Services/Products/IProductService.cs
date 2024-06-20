﻿
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Products
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy);
        Task DeleteProductAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
    }
}
