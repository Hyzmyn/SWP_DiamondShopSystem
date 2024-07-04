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
		public async Task<decimal?> GetPriceRateAsync()
		{
			var today = DateTime.Today;
			var priceRate = await _db.PriceRateLists
				.Where(pr => pr.EffDate <= today)
				.OrderByDescending(pr => pr.EffDate)
				.FirstOrDefaultAsync();

			return priceRate?.PriceRate;
		}

	}
}
