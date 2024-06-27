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

namespace Service.Services
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
        public async Task<decimal> GetDiamondPrice(string cut, decimal carat, string color, string clarity)
        {
            RandomizeValue randomValues = RandomNumberStore.RandomValues;
            decimal cutValue = 0m;
            decimal colorValue = 0m;
            decimal clarityValue = 0m;

            switch (cut.ToLower())
            {
                case "excellent":
                    cutValue = randomValues.Excellent;
                    break;
                case "verygood":
                    cutValue = randomValues.VeryGood;
                    break;
                case "good":
                    cutValue = randomValues.Good;
                    break;
                default:
                    throw new ArgumentException("Invalid cut value.");
            }

            // Determine the value for color
            switch (color.ToUpper())
            {
                case "D":
                    colorValue = randomValues.D;
                    break;
                case "E":
                    colorValue = randomValues.E;
                    break;
                case "F":
                    colorValue = randomValues.F;
                    break;
                case "J":
                    colorValue = randomValues.J;
                    break;
                default:
                    throw new ArgumentException("Invalid color value.");
            }

            // Determine the value for clarity
            switch (clarity.ToUpper())
            {
                case "IF":
                    clarityValue = randomValues.IF;
                    break;
                case "VVS1":
                    clarityValue = randomValues.VVS1;
                    break;
                case "VVS2":
                    clarityValue = randomValues.VVS2;
                    break;
                case "VS1":
                    clarityValue = randomValues.VS1;
                    break;
                case "VS2":
                    clarityValue = randomValues.VS2;
                    break;
                default:
                    throw new ArgumentException("Invalid clarity value.");
            }

            // Calculate the total price
            decimal totalPrice = (cutValue + colorValue + clarityValue + (carat * randomValues.CaratPrice));

            return await Task.FromResult(totalPrice);
        }
    }
}



//public async Task<decimal> GetDiamondPrice(string cut, decimal carat, string color, string clarity, string make, string certificate)
//{
//    string apiUrl = $"http://www.idexonline.com/DPService.asp?Cut={cut}&Carat={carat}&Color={color}&Clarity={clarity}&Make={make}&Cert={certificate}";

//    HttpResponseMessage response = await _httpClient.GetAsync(apiUrl);

//    if (response.IsSuccessStatusCode)
//    {
//        string responseBody = await response.Content.ReadAsStringAsync();

//        XDocument xmlDoc = XDocument.Parse(responseBody);
//        XElement priceElement = xmlDoc.Descendants("price").FirstOrDefault();

//        if (priceElement != null && decimal.TryParse(priceElement.Value, out decimal price))
//        {
//            return price;
//        }

//        throw new Exception("Unable to extract price from API response.");
//    }
//    else
//    {
//        throw new Exception($"API request failed with status code: {response.StatusCode}");
//    }
//}


