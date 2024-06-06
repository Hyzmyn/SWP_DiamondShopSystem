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

        public Order? GetById(int id)
        {
            _db = new();
            return _db.Orders.Find(id);
        }
        public List<Order> GetAll()
        {
            _db = new();
            return _db.Orders.ToList();
        }
        public void Create(Order order)
        {
            _db = new();
            _db.Orders.Add(order);
            _db.SaveChanges();

        }

        public void Update(Order order)
        {
            _db = new();
            _db.Orders.Update(order);
            _db.SaveChanges();
        }

        public void Delete(int id)
        {
            _db = new();
            var order = _db.Orders.FirstOrDefault(x => x.OrderId == id);

            if (order != null)
            {
                _db.Orders.Remove(order);
                _db.SaveChanges();
            }
        }
    }
}
