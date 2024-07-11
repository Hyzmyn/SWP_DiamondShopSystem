using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IGemPriceListService
    {
        Task<List<GemPriceList>> GetGemPriceListsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy);
        Task DeleteGemPriceListAsync(int id);
        Task AddGemPriceListAsync(GemPriceList gemPriceList);
        Task UpdateGemPriceListAsync(GemPriceList gemPriceList);
        decimal CalculatePrice(string color, string cut, string clarity, decimal weight);
		Task CalculateAndSavePricesAsync();


	}
}
