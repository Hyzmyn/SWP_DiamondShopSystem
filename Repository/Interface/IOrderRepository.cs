using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IOrderRepository
    {


        public Order? GetById(int id);

        public List<Order> GetAll();


        public void Create(Order order);


        public void Update(Order order);


        public void Delete(int id);
        
        
    }
}
