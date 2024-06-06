using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Interface
{
    public interface IWarrantyRepository
    {
        public List<Warranty> GetAll();
        public Warranty? GetByID(int WarrantyID);
        public void Create(Warranty warranty);
        public void Update(Warranty warranty);
        public void Delete(int WarrantyID);
    }
}
