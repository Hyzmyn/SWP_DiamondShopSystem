
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
    internal class OrderDetailService : IOrderDetailService
    {
        private IOrderDetailRepository _repo;


        public OrderDetailService(IOrderDetailRepository repo)
        {
            _repo = repo;
        }
    }
}
