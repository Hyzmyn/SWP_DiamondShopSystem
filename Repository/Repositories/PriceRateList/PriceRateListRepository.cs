using Microsoft.EntityFrameworkCore;


using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class PriceRateListRepository : BaseRepository<PriceRateList>, IPriceRateListRepository
    {
        private DiamondShopContext _db;
        public PriceRateListRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }


    }
}
