
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderDetailRepository : BaseRepository<OrderDetail>, IOrderDetailRepository
    {
        private DiamondShopContext _db;
        public OrderDetailRepository(DiamondShopContext context) : base(context)
        {
            _db = context;
        }
    }
}
