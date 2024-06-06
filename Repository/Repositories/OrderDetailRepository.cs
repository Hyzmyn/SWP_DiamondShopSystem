using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class OrderDetailRepository
    {
        private DiamondShopContext _db;
        public List<OrderDetail> GetAll()
        {
            _db = new();
            return _db.OrderDetails.ToList();
        }
        public OrderDetail Get(int id)
        {
            _db = new();
            return _db.OrderDetails.FirstOrDefault();
        }
        public OrderDetail? GetbyID(int id)
        {
            _db = new();
            return _db.OrderDetails.Find(id);
        }
        public void Create(OrderDetail orderdetail) 
        {
            _db = new();
            _db.OrderDetails.Add(orderdetail);
            _db.SaveChanges();
        }
        public void Update(OrderDetail orderdetail)
        {
            _db = new();
            _db.OrderDetails.Update(orderdetail);
            _db.SaveChanges();
        }
        public void Delete(int id)
        {
            _db = new();
            var OrderDetail = _db.OrderDetails.FirstOrDefault(x =>x.Equals(id));
            if (OrderDetail != null)
            {
                _db.OrderDetails.Remove(OrderDetail);
                _db.SaveChanges();
            }
        }
    }
}
