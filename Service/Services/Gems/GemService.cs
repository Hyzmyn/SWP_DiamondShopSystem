
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
	public class GemService : IGemService
	{
		private IGemRepository _repo;
		private readonly HttpClient _httpClient;

		public GemService(IGemRepository repo, IHttpClientFactory httpClientFactory)
		{
			_repo = repo;
			_httpClient = httpClientFactory.CreateClient();
		}

		public Task AddGemAsync(Gem gem)
		{
			throw new NotImplementedException();
		}

		public Task DeleteGemAsync(int id)
		{
			throw new NotImplementedException();
		}

		public Task<List<Gem>> GetGemsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
		{
			throw new NotImplementedException();
		}

		public Task UpdateGemAsync(Gem gem)
		{
			throw new NotImplementedException();
		}

		public async Task<decimal> GetDiamondPrice(string cut, decimal carat, string color, string clarity)
		{
			RandomizeValue randomValues = RandomNumberStore.RandomValues;
			decimal cutValue = 0m;
			decimal colorValue = 0m;
			decimal clarityValue = 0m;
			switch (cut.ToLower())
			{
				case "excellent":
					cutValue = randomValues.Excellent;
					break;
				case "verygood":
					cutValue = randomValues.VeryGood;
					break;
				case "good":
					cutValue = randomValues.Good;
					break;
				default:
					throw new ArgumentException("Invalid cut value.");
			}
			switch (color.ToUpper())
			{
				case "D":
					colorValue = randomValues.D;
					break;
				case "E":
					colorValue = randomValues.E;
					break;
				case "F":
					colorValue = randomValues.F;
					break;
				case "J":
					colorValue = randomValues.J;
					break;
				default:
					throw new ArgumentException("Invalid color value.");

			}
			switch (clarity.ToUpper())
			{
				case "IF":
					clarityValue = randomValues.IF;
					break;
				case "VVS1":
					clarityValue = randomValues.VVS1;
					break;
				case "VVS2":
					clarityValue = randomValues.VVS2;
					break;
				case "VS1":
					clarityValue = randomValues.VS1;
					break;
				case "VS2":
					clarityValue = randomValues.VS2;
					break;
				default:
					throw new ArgumentException("Invalid clarity value.");
			}
			decimal totalPrice = (cutValue + colorValue + clarityValue + (carat * randomValues.CaratPrice));

			return await Task.FromResult(totalPrice);
		}
	}
}
