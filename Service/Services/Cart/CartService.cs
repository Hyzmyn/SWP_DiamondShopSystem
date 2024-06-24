using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
	public class CartService : ICartService
    {
		private readonly DiamondShopContext _context;

		public CartService(DiamondShopContext context)
		{
			_context = context;
		}

		public async Task AddToCartAsync(int productId, int userId, int quantity)
		{
			var order = await _context.Orders
				.FirstOrDefaultAsync(o => o.UserID == userId && !o.OrderStatus);

			if (order == null)
			{
				order = new Order
				{
					UserID = userId,
					TimeOrder = DateTime.Now,
					OrderStatus = false,
					Note = "",
				};
				_context.Orders.Add(order);
				await _context.SaveChangesAsync();
			}

			var orderDetail = await _context.OrderDetails
				.FirstOrDefaultAsync(od => od.OrderID == order.OrderID && od.ProductID == productId);

			if (orderDetail == null)
			{
				var product = await _context.Products.FindAsync(productId);
				var user = await _context.Users.FindAsync(userId);
				orderDetail = new OrderDetail
				{
					OrderID = order.OrderID,
					ProductID = productId,
					Quantity = quantity,
					Price = product.TotalCost,
					NiSize = user.NiSize
				};
				_context.OrderDetails.Add(orderDetail);
			}
			else
			{
				orderDetail.Quantity += quantity;
			}

			await _context.SaveChangesAsync();
			await UpdateOrderTotalPrice(order.OrderID);
		}

		public async Task<List<OrderDetail>> GetCartItemsAsync(int userId)
		{
			var order = await _context.Orders
				.FirstOrDefaultAsync(o => o.UserID == userId && !o.OrderStatus);

			if (order == null)
				return new List<OrderDetail>();

			return await _context.OrderDetails
				.Include(od => od.Product)
				.Where(od => od.OrderID == order.OrderID)
				.ToListAsync();
		}

        public async Task RemoveFromCartAsync(int orderDetailId)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(orderDetailId);
            if (orderDetail != null)
            {
                _context.OrderDetails.Remove(orderDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateQuantityAsync(int orderDetailId, int quantity)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(orderDetailId);
            if (orderDetail != null)
            {
                orderDetail.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
        }

        private async Task UpdateOrderTotalPrice(int orderId)
		{
			var order = await _context.Orders.FindAsync(orderId);
			if (order != null)
			{
				order.TotalPrice = await _context.OrderDetails
					.Where(od => od.OrderID == orderId)
					.SumAsync(od => od.Price * od.Quantity);
				await _context.SaveChangesAsync();
			}
		}
	}
}
