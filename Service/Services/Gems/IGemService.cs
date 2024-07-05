
using Repository.Repositories;
using Repository.Models;
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
        Task<decimal> GetDiamondPrice(string cut, decimal carat, string color, string clarity);
        Task<Gem> GetByID(int id);

	}
}
