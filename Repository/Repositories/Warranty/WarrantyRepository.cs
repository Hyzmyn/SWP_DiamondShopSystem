
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class WarrantyRepository : BaseRepository<Warranty>, IWarrantyRepository
    {
        private DiamondShopContext _db;
        public WarrantyRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }
    }
}
