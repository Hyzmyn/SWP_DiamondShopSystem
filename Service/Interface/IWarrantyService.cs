using Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interface
{
    public interface IWarrantyService
    {
        public List<Warranty> GetAllWarranty();
        public Warranty GetWarranty(int id);
        public void AddWarranty(Warranty warranty);
        public void UpdateWarranty(Warranty Warranty);
        public void DeleteWarranty(int Warranty);
    }
}
