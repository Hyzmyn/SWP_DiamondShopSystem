using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using Service.Services;
using SWP.Helper;

namespace SWP.Areas.Customer.Controllers
{
    [Area("customer")]
    [Route("customer/warranties")]

    public class WarrantiesController : Controller
    {
        private readonly DiamondShopContext _context;
        private readonly ICompositeViewEngine _viewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWarrantyService _warrantyService;
        private readonly DiamondShopContext _dbContext;

        public WarrantiesController(DiamondShopContext context, ICompositeViewEngine viewEngine, IServiceProvider serviceProvider, IWarrantyService warrantyService, DiamondShopContext dbContext)
        {
            _context = context;
            _viewEngine = viewEngine;
            _serviceProvider = serviceProvider;
            _warrantyService = warrantyService;
            _dbContext = dbContext;
        }


        // GET: Manager/Warranties
   
        [HttpGet("/warranty/")]
        public async Task<IActionResult> Index()
        {
            var diamondShopContext = _context.Warranties.Include(w => w.Order).ThenInclude(u => u.User)
                .Include(w => w.Product).ThenInclude(o => o.Gems).OrderByDescending(o=>o.WarrantyID);
            return View(await diamondShopContext.ToListAsync());
        }

        // GET: Manager/Warranties/Details/5
       
        [HttpGet("/warrantydetails/{id}")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warranty = await _context.Warranties
                .Include(w => w.Order).ThenInclude(u => u.User)
                .Include(w => w.Product).ThenInclude(o=>o.Gems)
                .FirstOrDefaultAsync(m => m.WarrantyID == id);
            if (warranty == null)
            {
                return NotFound();
            }

            return View(warranty);
        }


        // GET: Manager/Warranties/Edit/5
        
        [HttpGet("/editwarranty/")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warranty = await _context.Warranties.FindAsync(id);
            if (warranty == null)
            {
                return NotFound();
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "Note", warranty.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ImageUrl1", warranty.ProductID);
            return View(warranty);
        }

        // POST: Manager/Warranties/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost("/editwarranty/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Warranty warranty)
        {
            if (id != warranty.WarrantyID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(warranty);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WarrantyExists(warranty.WarrantyID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderID"] = new SelectList(_context.Orders, "OrderID", "Note", warranty.OrderID);
            ViewData["ProductID"] = new SelectList(_context.Products, "ProductID", "ImageUrl1", warranty.ProductID);
            return View(warranty);
        }

        // GET: Manager/Warranties/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var warranty = await _context.Warranties
                .Include(w => w.Order)
                .Include(w => w.Product)
                .FirstOrDefaultAsync(m => m.WarrantyID == id);
            if (warranty == null)
            {
                return NotFound();
            }

            return View(warranty);
        }

        // POST: Manager/Warranties/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var warranty = await _context.Warranties.FindAsync(id);
            if (warranty != null)
            {
                _context.Warranties.Remove(warranty);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WarrantyExists(int id)
        {
            return _context.Warranties.Any(e => e.WarrantyID == id);
        }
       
        [HttpGet("/genpdf/{id}")]
        public async Task<IActionResult> GenerateWarrantyPdf(int id)
        {
            // Example: Generate the HTML for the PDF
            var htmlTask = Task.FromResult("<html><body><h1>Warranty Document</h1></body></html>");
            var model = await _dbContext.Warranties.Include(o => o.Order).ThenInclude(o => o.User).Include(o => o.Product).ThenInclude(o => o.Gems).FirstOrDefaultAsync(p => p.WarrantyID == id);
            // Generate and save the PDF
            await _warrantyService.ExPortPdf(HtmlAsync(model), model.WarrantyID.ToString(), "Warranty");

            // Redirect to the download action
            return RedirectToAction("DownloadWarrantyPdf", new { id = model.WarrantyID.ToString() });
        }
        [Route("/download/{id}")]
        [HttpGet]
        public async Task<IActionResult> DownloadWarrantyPdf(string id)
        {
            if (id == null)
            {
                return BadRequest("Invalid ID");
            }

            // Calculate the base directory of the project (2 levels up from bin)
            string baseDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string wwwrootPath = Path.Combine(baseDirectory, "wwwroot", "Warranty");
           
            // Check if the file exists
            string filePath = Path.Combine(wwwrootPath, $"Warranty{id}.pdf");
            // Return the PDF file
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = $"Warranty{id}.pdf"
            };
        }
        public async Task<string> HtmlAsync(Warranty model)
        {
            try
            {
                string partialViewHtml = await ViewRenderer.RenderPartialViewToStringAsync(this, "WarrantyPdf", model, _viewEngine, _serviceProvider);
                return partialViewHtml;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error rendering HTML: {ex.Message}");
                throw;
            }
        }
    }


}
