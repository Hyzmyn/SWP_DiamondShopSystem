using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Cart
{
	public interface ICartService
	{
		Task AddToCartAsync(int productId, int userId, int quantity);
		Task<List<OrderDetail>> GetCartItemsAsync(int userId);
		Task RemoveFromCartAsync(int orderDetailId);
		Task UpdateQuantityAsync(int orderDetailId, int quantity);
	}
}
