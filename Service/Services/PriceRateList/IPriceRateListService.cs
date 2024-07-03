using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IPriceRateListService
    {
        Task<List<PriceRateList>> GetProductCatergorysAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy);
        Task DeleteProductCatergoryAsync(int id);
        Task AddProductCatergoryAsync(PriceRateList productCatergory);
        Task UpdateProductCatergoryAsync(PriceRateList productCatergory);
        Task<decimal?> GetPriceRate();

	}
}
