
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories

{
    public interface IOrderRepository : IBaseRepository<Order>
    {

        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        Task<Order> GetByIdAsync(int orderId);
        Task<Order> GetOrderByUserIdAndProductIdAsync(int userId, int productId);
    }
}
