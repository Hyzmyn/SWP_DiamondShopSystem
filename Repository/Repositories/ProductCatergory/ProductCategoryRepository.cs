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
    public class ProductCategoryRepository : BaseRepository<ProductCategory>, IProductCategoryRepository
    {
        private DiamondShopContext _db;
        public ProductCategoryRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }


    }
}
