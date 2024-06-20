﻿
using ervice.Services.OrderDetails;
using Repository.Interface;
using Repository.Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.OrderDetails
{
    internal class OrderDetailService : IOrderDetailService
    {
        private IOrderDetailRepository _repo;


        public OrderDetailService(IOrderDetailRepository repo)
        {
            _repo = repo;
        }

        public Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderDetailAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDetail>> GetOrderDetailsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }
    }
}