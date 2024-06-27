﻿
using Repository.Interface;
using Repository.Models;
using Repository.Repositories;
using service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _repo;
        private IOrderDetailService _orderDetailService;

        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _repo.GetOrdersByUserIdAsync(userId);
        }
        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _repo.GetByIdAsync(orderId);
        }

        public Task AddOrderAsync(Order order)
        {
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
    }
}
