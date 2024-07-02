
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IWarrantyService
    {
        Task<List<Warranty>> GetWarrantiesAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy);
        Task DeleteWarrantyAsync(int id);
        Task AddWarrantyAsync(int userId);
        Task UpdateWarrantyAsync(Warranty Warranty);

        Task ExPortPdf(Task<string> html, string id, string type);

    }
}
