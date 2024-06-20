﻿
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Repository.Models;
using Repository.Repositories.Base;
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
    }
}