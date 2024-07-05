using Microsoft.EntityFrameworkCore;

using Repository.Models;

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

        public GemPriceListRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }

	}
}
