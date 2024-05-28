using Repository;
using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class OderService
    {
        private OrderRepository _repo = new OrderRepository();

        public List<Order> GetAllOrder() => _repo.GetAll();
        
        public Order? GetAnOrder(int id) => _repo.GetById(id);

        public List<Order> SearchOrder(string keyword) => _repo.GetAll().Where(x => x.OrderId.ToString().Contains(keyword.ToLower()) ||
                                                                                    x.OrderDetailId.ToString().Contains(keyword.ToLower())).ToList();
 
        public void AddOrder(Order order) => _repo.Create(order);

        public void UpdateOrder(Order order) => _repo.Update(order);

        public void DeleteOrder(int id) => _repo.Delete(id);
    }
}
