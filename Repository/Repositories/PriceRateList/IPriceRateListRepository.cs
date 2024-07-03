

using Repository.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public interface IPriceRateListRepository : IBaseRepository<PriceRateList>
    {
		Task<decimal?> GetPriceRateAsync();

	}
}
