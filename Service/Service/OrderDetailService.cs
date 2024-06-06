using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    internal class OrderDetailService
    {
        private OrderDetailRepository _repo = new OrderDetailRepository();
        public List<OrderDetail> GetAllOrderDetail() => _repo.GetAll();
        public OrderDetail GetAnOrder(int id) => _repo.GetbyID(id);
        public List<OrderDetail> SearchOrderDetail(int id) => _repo.GetAll().Where(x => x.OrderDetailId.ToString().ToLower().Equals(id.ToString().ToLower())).ToList();
        public void AddOrderDetail(OrderDetail orderDetail) => _repo.Create(orderDetail);
        public void UpdateOrderDetail(OrderDetail orderDetail) => _repo.Update(orderDetail);
        public void deleteOrderDetail(int id) => _repo.Delete(id);

    }
}
