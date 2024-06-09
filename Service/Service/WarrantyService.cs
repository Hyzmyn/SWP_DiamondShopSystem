using Repository.Entities;
using Repository.Interface;
using Repository.Repositories;
using Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Service
{
    public class WarrantyService : IWarrantyService
    {
        private IWarrantyRepository _repo;
        public WarrantyService(IWarrantyRepository repo)
        {
            _repo = repo;
        }
    }
}
