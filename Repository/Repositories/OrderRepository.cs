using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        private DiamondShopContext _db;
    public OrderRepository(DiamondShopContext context) : base(context)
    {
        _db = context;
    }
}
}
