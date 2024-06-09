using Repository.Entities;
using Repository.Interface;
using Repository.Repositories;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _repo;
        public OrderService(IOrderRepository repo)
        {
            _repo = repo;
        }
    }
}
