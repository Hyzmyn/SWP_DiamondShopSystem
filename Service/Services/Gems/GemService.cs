
using Repository.Interface;
using Repository.Models;
using Repository.Repositories;
using Service.Services.Gems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service.Service
{
    public class GemService : IGemService
    {
        private IGemRepository _repo;
        private readonly HttpClient _httpClient;

        public GemService(IGemRepository repo, IHttpClientFactory httpClientFactory)
        {
            _repo = repo;
            _httpClient = httpClientFactory.CreateClient();
        }

        public Task AddGemAsync(Gem gem)
        {
            throw new NotImplementedException();
        }

        public Task DeleteGemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Gem>> GetGemsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGemAsync(Gem gem)
        {
            throw new NotImplementedException();
        }


    }
}
