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

                var price = CalculateAdjustedPrice(product.TotalCost, user.NiSize);

                orderDetail = new OrderDetail
                {
                    OrderID = order.OrderID,
                    ProductID = productId,
                    Quantity = quantity,
                    Price = price,
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
        public async Task UpdateNiAsync(int orderDetailId, string ni)
        {
            var orderDetail = await _context.OrderDetails
    .Include(od => od.Product)
    .FirstOrDefaultAsync(od => od.OrderDetailID == orderDetailId);
            if (orderDetail != null)
            {
                var price = CalculateAdjustedPrice(orderDetail.Product.TotalCost, ni);
                orderDetail.NiSize = ni;
                orderDetail.Price = price;

                await _context.SaveChangesAsync();
            }
        }

        private decimal CalculateAdjustedPrice(decimal basePrice, string ringSizeStr)
        {
            if (!int.TryParse(ringSizeStr, out int ringSize))
                throw new ArgumentException("Ring size must be a valid number between 6 and 20.");

            if (ringSize < 6 || ringSize > 20)
                throw new ArgumentOutOfRangeException("Ring size must be between 6 and 20.");

            decimal increasePercentage = ((decimal)(ringSize - 6) / (20 - 6)) * 10;
            return basePrice * (1 + increasePercentage / 100);
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

        public async Task UpdateOrder(int orderId, decimal price)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.TotalPrice = price;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> GetOrderId(int userId)
        {
            // Logic to retrieve the order ID for the user
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.UserID == userId && !o.OrderStatus);
            return order?.OrderID ?? 0;
        }
    }

}
