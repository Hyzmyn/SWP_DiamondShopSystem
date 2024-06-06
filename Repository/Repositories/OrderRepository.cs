using Repository.Entities;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private DiamondShopContext _db;

        public OrderRepository()
        {
            _db = new();
        }

        public Order? GetById(int id)
        {
            
            return _db.Orders.Find(id);
        }
        public List<Order> GetAll()
        {
            
            return _db.Orders.ToList();
        }
        public void Create(Order order)
        {
            
            _db.Orders.Add(order);
            _db.SaveChanges();

        }

        public void Update(Order order)
        {
            
            _db.Orders.Update(order);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            
            var order = _db.Orders.FirstOrDefault(x => x.OrderId == id);

            if (order != null)
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
            }
        }
    }
}
