using Repository.Interface;
using Repository.Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.GemPriceLists
{
    public class GemPriceListService : IGemPriceListService
    {
        private IGemPriceListRepository _repo;
        public GemPriceListService(IGemPriceListRepository repo)
        {
            _repo = repo;
        }

        public Task AddGemPriceListAsync(GemPriceList gemPriceList)
        {
            throw new NotImplementedException();
        }


        public Task DeleteGemPriceListAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GemPriceList>> GetGemPriceListsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGemPriceListAsync(GemPriceList gemPriceList)
        {
            throw new NotImplementedException();
        }


    }
}
