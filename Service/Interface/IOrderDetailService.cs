using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IOrderDetailService
    {
        public List<OrderDetail> GetAllOrderDetail();
        public OrderDetail GetAnOrder(int id);
        public List<OrderDetail> SearchOrderDetail(int id) ;
        public void AddOrderDetail(OrderDetail orderDetail);
        public void UpdateOrderDetail(OrderDetail orderDetail);
        public void deleteOrderDetail(int id);
    }
}
