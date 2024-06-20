using Repository.Interface;
using Repository.Models;
using Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Service.Services.GemPriceLists
{
    public class GemPriceListService : IGemPriceListService
    {
        private IGemPriceListRepository _repo;
        private readonly HttpClient _httpClient;

        public GemPriceListService(IGemPriceListRepository repo, IHttpClientFactory httpClientFactory)
        {
            _repo = repo;
            _httpClient = httpClientFactory.CreateClient();
        }

        public Task AddGemPriceListAsync(GemPriceList gemPriceList)
        {
            throw new NotImplementedException();
        }


        public Task DeleteGemPriceListAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<GemPriceList>> GetGemPriceListsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateGemPriceListAsync(GemPriceList gemPriceList)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetDiamondPrice(string cut, decimal carat, string color, string clarity, string make, string certificate)
        {
            string apiUrl = $"http://www.idexonline.com/DPService.asp?Cut={cut}&Carat={carat}&Color={color}&Clarity={clarity}&Make={make}&Cert={certificate}";

            HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();

                XDocument xmlDoc = XDocument.Parse(responseBody);
                XElement priceElement = xmlDoc.Descendants("price").FirstOrDefault();

                if (priceElement != null && decimal.TryParse(priceElement.Value, out decimal price))
                {
                    return price;
                }

                throw new Exception("Unable to extract price from API response.");
            }
            else
            {
                throw new Exception($"API request failed with status code: {response.StatusCode}");
            }
        }
    }
}
