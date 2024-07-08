
using Repository.Repositories;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class PriceRateListService : IPriceRateListService
    {
        private IPriceRateListRepository _repo;
        public PriceRateListService(IPriceRateListRepository repo)
        {
            _repo = repo;
        }

        public Task AddProductCatergoryAsync(PriceRateList productCatergory)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductCatergoryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<PriceRateList>> GetProductCatergorysAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProductCatergoryAsync(PriceRateList productCatergory)
        {
            throw new NotImplementedException();
        }
		public async Task<decimal?> GetPriceRate()
		{
			return await _repo.GetPriceRateAsync();
		}
	}
}
