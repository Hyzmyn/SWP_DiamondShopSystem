using Repository.Entities;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class WarrantyService
    {
        public WarrantyRepository _repo = new WarrantyRepository();
        public List<Warranty> GetAllWarranty() => _repo.GetAll();
        public Warranty GetWarranty(int id) => _repo.GetByID(id);
        public void AddWarranty(Warranty warranty) => _repo.Create(warranty);
        public void UpdateWarranty(Warranty Warranty) => _repo.Update(Warranty);
        public void DeleteWarranty(int Warranty) => _repo.Delete(Warranty);
    }
}
