using Repository.Repositories;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service.Services
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

		public async Task CalculateAndSavePricesAsync()
		{
			var gemPriceLists =  _repo.Get().ToList();

			foreach (var gemPrice in gemPriceLists)
			{
				decimal colorValue = gemPrice.Color switch
				{
					"D" => RandomNumberStore.RandomValues.D,
					"E" => RandomNumberStore.RandomValues.E,
					"F" => RandomNumberStore.RandomValues.F,
					"J" => RandomNumberStore.RandomValues.J,
					_ => 0
				};

				decimal clarityValue = gemPrice.Clarity switch
				{
					"IF" => RandomNumberStore.RandomValues.IF,
					"VVS1" => RandomNumberStore.RandomValues.VVS1,
					"VVS2" => RandomNumberStore.RandomValues.VVS2,
					"VS1" => RandomNumberStore.RandomValues.VS1,
					"VS2" => RandomNumberStore.RandomValues.VS2,
					_ => 0
				};

				decimal cutValue = gemPrice.Cut switch
				{
					"Excellent" => RandomNumberStore.RandomValues.Excellent,
					"VeryGood" => RandomNumberStore.RandomValues.VeryGood,
					"Good" => RandomNumberStore.RandomValues.Good,
					_ => 0
				};

				decimal caratValue = gemPrice.CaratWeight * RandomNumberStore.RandomValues.CaratPrice;

				gemPrice.Price = colorValue + clarityValue + cutValue + caratValue;
			}

			await _repo.SaveAsync();
		}
	}
}
