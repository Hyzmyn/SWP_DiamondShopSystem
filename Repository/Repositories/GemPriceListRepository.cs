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
    public class GemPriceListRepository : BaseRepository<GemPriceList>, IGemPriceListRepository
    {
        private DiamondShopContext _db;

        public GemPriceListRepository(DbContext dbContext, DiamondShopContext context) : base(dbContext)
        {
            _db = context;
        }
    }
}
