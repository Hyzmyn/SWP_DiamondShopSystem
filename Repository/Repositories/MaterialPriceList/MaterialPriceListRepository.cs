
using Repository.Interface;
using Repository.Models;
using Repository.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class MaterialPriceListRepository : BaseRepository<MaterialPriceList>, IMaterialPriceListRepository
    {
        private DiamondShopContext _db;
        public MaterialPriceListRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }
    }
}
