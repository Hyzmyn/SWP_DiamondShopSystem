
using Repository.Interface;
using Repository.Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public interface IGemService
    {
        Task<List<Gem>> GetGemsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy);
        Task DeleteGemAsync(int id);
        Task AddGemAsync(Gem gem);
        Task UpdateGemAsync(Gem gem);

    }
}
