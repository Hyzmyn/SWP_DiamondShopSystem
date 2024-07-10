using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public interface ICartService
	{
		Task AddToCartAsync(int productId, int userId, int quantity);
		Task<List<OrderDetail>> GetCartItemsAsync(int userId);
		Task RemoveFromCartAsync(int orderDetailId);
		Task UpdateQuantityAsync(int orderDetailId, int quantity);
		Task UpdateNiAsync(int orderDetailId, string ni);
        Task UpdateOrder(int orderId, decimal price);
        Task<int> GetOrderId(int userId);
    }
}
