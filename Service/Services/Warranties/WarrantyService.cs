
using Repository.Interface;
using Repository.Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services.Warranties
{
    public class WarrantyService : IWarrantyService
    {
        private IWarrantyRepository _repo;
        public WarrantyService(IWarrantyRepository repo)
        {
            _repo = repo;
        }

        public Task AddWarrantyAsync(Warranty warranty)
        {
            throw new NotImplementedException();
        }

        public Task DeleteWarrantyAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Warranty>> GetWarrantiesAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateWarrantyAsync(Warranty warranty)
        {
            throw new NotImplementedException();
        }
    }
}
