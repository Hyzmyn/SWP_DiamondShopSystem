using Repository.Interface;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.MaterialPriceLists
{
    public class MaterialPriceListService : IMaterialPriceListService
    {
        private IMaterialPriceListRepository _repo;
        public MaterialPriceListService(IMaterialPriceListRepository repo)
        {
            _repo = repo;
        }

        public Task AddMaterialPriceListAsync(MaterialPriceList materialPriceList)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMaterialPriceListAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<MaterialPriceList>> GetMaterialPriceListsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMaterialPriceListAsync(MaterialPriceList materialPriceList)
        {
            throw new NotImplementedException();
        }
    }
}
