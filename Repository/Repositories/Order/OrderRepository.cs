
using Microsoft.EntityFrameworkCore;
using Repository.Models;
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
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _db.Orders
                            .Where(o => o.UserID == userId)
                            .Include(o => o.OrderDetails)
                            .ToListAsync();
        }
        public async Task<Order> GetByIdAsync(int orderId)
        {
            return await _db.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).FirstOrDefaultAsync(o => o.OrderID == orderId);
        }

    }
}
