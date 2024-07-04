using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Repository.Models;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Service.Services
{
    public class WarrantyService : IWarrantyService
    {
        private readonly DiamondShopContext _dbContext;
        private readonly IWarrantyRepository _repo;

        
        public WarrantyService(DiamondShopContext dbContext, IWarrantyRepository repo)
        {
            _dbContext = dbContext;
            _repo = repo;
            
        }

        public async Task AddWarrantyAsync(int userId)
        {
            var order = await _dbContext.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(o => o.Product)
                .ThenInclude(o => o.Gems)
                .FirstOrDefaultAsync(o => o.UserID == userId && !o.OrderStatus);

            if (order == null) return;

            var warranties = new List<Warranty>();

            foreach (var orderDetail in order.OrderDetails)
            {
                string instance = $"{orderDetail.Product.ProductCode} {orderDetail.Product.Gems.GemCode}";
                decimal pricePerUnit = orderDetail.Price / orderDetail.Quantity;

                switch (pricePerUnit)
                {
                    case <= 100:
                        instance += " BASIC-04";
                        break;
                    case <= 200:
                        instance += " ELITE-03";
                        break;
                    case <= 300:
                        instance += " TECH-02";
                        break;
                    default:
                        instance += " ACME-01";
                        break;
                }

                for (int i = 0; i < orderDetail.Quantity; i++)
                {
                    var warranty = new Warranty
                    {
                        WarrantyStatus = true,
                        OrderID = order.OrderID,
                        ProductID = orderDetail.ProductID,
                        BuyDate = DateTime.Now,
                        EndDate = DateTime.Now.AddMonths(36),
                        Instance = instance
                    };

                    warranties.Add(warranty);
                }
            }

            await _dbContext.Warranties.AddRangeAsync(warranties);
            order.OrderStatus = true;
            await _dbContext.SaveChangesAsync();
        }

        public async Task ExPortPdf(Task<string> htmlTask, string id, string type)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException(nameof(id));
            }

            try
            {
                string html = await htmlTask;

                HtmlToPdf converter = new HtmlToPdf();

                converter.Options.PdfPageSize = PdfPageSize.A4;
                converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;
                converter.Options.MarginLeft = 10;
                converter.Options.MarginRight = 10;
                converter.Options.MarginTop = 20;
                converter.Options.MarginBottom = 20;

                PdfDocument doc = converter.ConvertHtmlString(html);

                string baseDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));

                string wwwrootPath = Path.Combine(baseDirectory, "wwwroot", type);


                Directory.CreateDirectory(wwwrootPath);

                string filePath = Path.Combine(wwwrootPath, $"{type}{id}.pdf");
                doc.Save(filePath);

                doc.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
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
