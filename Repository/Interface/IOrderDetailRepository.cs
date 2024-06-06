using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IOrderDetailRepository
    {
        public List<OrderDetail> GetAll();
        public OrderDetail Get(int id);
        public OrderDetail? GetbyID(int id);
        public void Create(OrderDetail orderdetail);
        public void Update(OrderDetail orderdetail);
        public void Delete(int id);
    }
}
