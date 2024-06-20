
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.MaterialPriceLists
{
    public interface IMaterialPriceListService
    {
        Task<List<MaterialPriceList>> GetMaterialPriceListsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy);
        Task DeleteMaterialPriceListAsync(int id);
        Task AddMaterialPriceListAsync(MaterialPriceList materialPriceList);
        Task UpdateMaterialPriceListAsync(MaterialPriceList materialPriceList);

    }
}
