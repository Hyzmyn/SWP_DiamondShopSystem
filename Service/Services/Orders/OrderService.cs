﻿using Repository.Repositories;
using Repository.Models;
using SelectPdf;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _repo;
        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public Task AddOrderAsync(Order order)
        {
			//var existingOrder = _repo.GetAsync(order.OrderID);
			//if (existingOrder == null)
			//{
			//	order
			//	order
			//	await _repo.CreateAsync(user);
			//	await _repo.SaveAsync();
			//}
			//else
			//{
			//	user = null;
			//}
			throw new NotImplementedException();
		}

        public Task DeleteOrderAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Order>> GetOrdersAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderAsync(Order order)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _repo.GetOrdersByUserIdAsync(userId);
        }
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _repo.GetByIdAsync(orderId);
        }

        
    }
}
