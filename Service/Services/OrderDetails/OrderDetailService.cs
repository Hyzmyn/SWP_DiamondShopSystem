
using service.Services;
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
    public class OrderDetailService : IOrderDetailService
    {
        private IOrderDetailRepository _repo;
        private IGemPriceListService _gemPriceListService;
        private IProductMaterialService _productMaterialService;


        public OrderDetailService(IOrderDetailRepository repo, IGemPriceListService gemPriceListService, IProductMaterialService productMaterialService)
        {
            _repo = repo;
            _gemPriceListService = gemPriceListService;
            _productMaterialService = productMaterialService;
        }

        public Task AddOrderDetailAsync(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        public Task DeleteOrderDetailAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderDetail>> GetOrderDetailsAsync(string keyword, int pageNumber, int pageSize, int defaultPageSize, string sortBy)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderDetailAsync(OrderDetail orderDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<decimal> GetOrderDetailPrice(decimal Weight, string cut, decimal carat, string color, string clarity)
        {
            var gemPrice = await _gemPriceListService.GetDiamondPrice(cut, carat, color, clarity);
            var materialPrice = await _productMaterialService.GetMaterialPrice(Weight);

            return gemPrice + materialPrice;
        }
    }
}
