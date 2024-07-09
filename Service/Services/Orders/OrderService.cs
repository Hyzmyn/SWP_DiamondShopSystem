using Repository.Repositories;
using Repository.Models;
using SelectPdf;
using Microsoft.EntityFrameworkCore;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _repo;
        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task AddOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Order>> GetOrdersAsync(string keyword, int pageNumber, int pageSize, string sortBy)
        {
			int defaultPageSize = 10;
			if (pageNumber <= 0 && pageSize <= 0)
			{
				pageSize = defaultPageSize;
				pageNumber = 1;
			}

			List<Order> orders = _repo.Get().ToList();

			if (!string.IsNullOrEmpty(keyword))
			{
				orders = orders.Where(x =>
					x.OrderID.ToString().Contains(keyword.ToLower())).ToList();
			}

            var ordersList = orders.OrderBy(x => x.TimeOrder)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToList();

			var totalOrders = ordersList.Count;
			var totalPages = pageSize > 0 ? (int)Math.Ceiling((double)totalOrders / pageSize) : 0;

			if (pageNumber > totalPages)
			{
				pageNumber = totalPages;
			}

			return ordersList;
		}

        public async Task UpdateOrderAsync(Order order)
        {
			var existingOrder = await _repo.GetAsync(order.OrderID);
			if (existingOrder != null)
			{
				order.OrderID = existingOrder.OrderID;
				_repo.Update(order);
				await _repo.SaveAsync();
			}
			else
			{
				order = null;
			}
		}
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _repo.GetOrdersByUserIdAsync(userId);
        }
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _repo.GetByIdAsync(orderId);
        }
        public async Task<Order> GetOrderByUserIdAndProductIdAsync(int userId, int productId)
        {
            return await _repo.GetOrderByUserIdAndProductIdAsync(userId, productId);
        }

    }
}
