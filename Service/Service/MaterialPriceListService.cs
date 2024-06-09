using Repository.Interface;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class MaterialPriceListService : IMaterialPriceListService
    {
        private IMaterialPriceListRepository _repo;
        public MaterialPriceListService(IMaterialPriceListRepository repo)
        {
            _repo = repo;
        }
    }
}
