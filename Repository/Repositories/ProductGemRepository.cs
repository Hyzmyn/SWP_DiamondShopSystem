using Microsoft.EntityFrameworkCore;
using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProductGemRepository : BaseRepository<ProductGem>, IProductGemRepository
    {
        private DiamondShopContext _db;

        public ProductGemRepository(DbContext dbContext, DiamondShopContext context) : base(dbContext)
        {
            _db = context;
        }

    }
}
