using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories;
using Repository.Models;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Service.Services
{
    public class WarrantyService : IWarrantyService
    {
        private readonly IWarrantyRepository _repo;

        
        public WarrantyService(IWarrantyRepository repo)
        {
            _repo = repo;
            
        }

        public async Task AddWarrantyAsync(int userId)
        {
            await _repo.AddWarrantyAsync(userId);
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
        public async Task<Warranty> GetWarrantyByProductAndOrderAsync(int productId, int orderId)
        {
            return await _repo.GetWarrantyByProductAndOrderAsync(productId, orderId);
        }
    }
}
