
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
    }
}