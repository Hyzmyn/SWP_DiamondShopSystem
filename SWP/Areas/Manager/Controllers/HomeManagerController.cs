using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.EntityFrameworkCore;
using Repository.Models;
using Service.Services;
using SWP.Helper;

namespace SWP.Areas.Manager.Controllers
{
    [Area("manager")]
    [Route("manager")]
    [Route("manager/homemanager")]
    public class HomeManagerController : Controller
    {
        private readonly DiamondShopContext context;
        private readonly IWebHostEnvironment environment;
        DiamondShopContext db = new DiamondShopContext();
        private readonly ICompositeViewEngine _viewEngine;
        private readonly IServiceProvider _serviceProvider;
        private readonly IWarrantyService _warrantyService;

        public HomeManagerController(DiamondShopContext context, IWebHostEnvironment environment, DiamondShopContext db, ICompositeViewEngine viewEngine, IServiceProvider serviceProvider, IWarrantyService warrantyService)
        {
            this.context = context;
            this.environment = environment;
            this.db = db;
            _viewEngine = viewEngine;
            _serviceProvider = serviceProvider;
            _warrantyService = warrantyService;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("gemlist")]
		
		public IActionResult GemList()
        {
            var lstGem = db.Gems.ToList();
            return View(lstGem);
        }


        [Route("creategem")]
		[HttpGet]
		public IActionResult CreateGem()
        {
			
			return View();
		}
        [Route("creategem")]
        [HttpPost]
		public IActionResult CreateGem(Gem gem)
		{
			if (ModelState.IsValid)
			{
				context.Add(gem);
				context.SaveChanges();
				TempData["SuccessMessage"] = "Gem created successfully!";
				return RedirectToAction(nameof(GemList));
			}
			return View(gem);
		}
        [Route("genpdf/{id}")]
        [HttpGet]
        public async Task<IActionResult> GenerateWarrantyPdf(int id)
        {

          
            var model = await context.Gems.FirstOrDefaultAsync(o => o.GemID == id);

            await _warrantyService.ExPortPdf(HtmlAsync(model), id.ToString(), "Certificate");

            return RedirectToAction("DownloadWarrantyPdf", new { id = id.ToString() });
        }
        [Route("download/{id}")]
        [HttpGet]
        public async Task<IActionResult> DownloadWarrantyPdf(string id)
        {
            if (id == null)
            {
                return BadRequest("Invalid ID");
            }


            string baseDirectory = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\.."));
            string wwwrootPath = Path.Combine(baseDirectory, "wwwroot", "Certificate");

            string filePath = Path.Combine(wwwrootPath, $"Certificate{id}.pdf");
            // Return the PDF file
            var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return new FileStreamResult(stream, "application/pdf")
            {
                FileDownloadName = $"Certificate{id}.pdf"
            };
        }
        public async Task<string> HtmlAsync(Gem model)
        {
            try
            {
                string partialViewHtml = await ViewRenderer.RenderPartialViewToStringAsync(this, "Certificate", model, _viewEngine, _serviceProvider);
                return partialViewHtml;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error rendering HTML: {ex.Message}");
                throw;
            }
        }

        [Route("editgem")]
        [HttpGet]
        public IActionResult EditGem(string gemCode)
        {
            var gem = context.Gems.FirstOrDefault(g => g.GemCode == gemCode);
            if (gem == null)
            {
                return RedirectToAction(nameof(GemList));
            }

            var gemDto = new GemDto
            {
                GemID = gem.GemID,
                GemCode = gem.GemCode,
                GemName = gem.GemName,
                Origin = gem.Origin,
                FourC = gem.FourC,
                Proportion = gem.Proportion,
                Polish = gem.Polish,
                Symmetry = gem.Symmetry,
                Fluorescence = gem.Fluorescence,
                Active = gem.Active
            };

            ViewData["GemCode"] = gem.GemCode;

            return View(gemDto);
        }
        [Route("editgem")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditGem(string gemCode, GemDto gemDto)
        {
            var gem = context.Gems.FirstOrDefault(g => g.GemCode == gemCode);
            if (gem == null)
            {
                return RedirectToAction(nameof(GemList));
            }

            if (!ModelState.IsValid)
            {
                ViewData["GemCode"] = gem.GemCode;
                return View(gemDto);
            }

            gem.GemCode = gemDto.GemCode;
            gem.GemName = gemDto.GemName;
            gem.Origin = gemDto.Origin;
            gem.FourC = gemDto.FourC;
            gem.Proportion = gemDto.Proportion;
            gem.Polish = gemDto.Polish;
            gem.Symmetry = gemDto.Symmetry;
            gem.Fluorescence = gemDto.Fluorescence;
            gem.Active = gemDto.Active;

            context.SaveChanges();

            TempData["SuccessMessage"] = "Gem updated successfully.";
            return RedirectToAction(nameof(GemList));
        }


        [HttpPost]
        [Route("deletegem/{gemCode}")]
        public IActionResult DeleteGem(string gemCode)
        {
            var gem = context.Gems.FirstOrDefault(g => g.GemCode == gemCode);
            if (gem == null)
            {
                TempData["Error"] = "Gem not found.";
                return RedirectToAction(nameof(GemList));
            }

            try
            {
                context.Gems.Remove(gem);
                context.SaveChanges();

                TempData["Message"] = "Gem deleted successfully.";
            }
            catch (Exception ex)
            {
                
                TempData["Error"] = $"An error occurred while deleting the gem: {ex.Message}";
            }

            return RedirectToAction(nameof(GemList));
        }

        [Route("gempricelist")]
        public IActionResult GemPriceList()
        {
            var gemPriceList = db.GemPriceLists.ToList();
            return View(gemPriceList);
        }

        [HttpGet]
        [Route("creategempricelist")]
        public IActionResult CreateGemPriceList()
        {
            return View();
        }

        [HttpPost]
        [Route("creategempricelist")]
        public IActionResult CreateGemPriceList(GemPriceList gemPriceList)
        {
            if (ModelState.IsValid)
            {
                // Save to database logic here
                context.GemPriceLists.Add(gemPriceList);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Gem price list entry created successfully!";
                return RedirectToAction(nameof(GemPriceList));
            }

            // If ModelState is not valid, return the view with validation errors
            return View(gemPriceList);
        }
        [HttpGet]
        [Route("editgempricelist/{gemID}")]
        public IActionResult EditGemPriceList(int gemID)
        {
            var gemPriceList = context.GemPriceLists.Find(gemID);
            if (gemPriceList == null)
            {
                TempData["ErrorMessage"] = "Gem Price List entry not found.";
                return RedirectToAction(nameof(GemPriceList));
            }

            return View(gemPriceList);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("editgempricelist/{gemID}")]
        public IActionResult EditGemPriceList(int gemID, GemPriceList gemPriceList)
        {
            if (gemID != gemPriceList.GemID)
            {
                TempData["ErrorMessage"] = "Gem ID mismatch.";
                return RedirectToAction(nameof(GemPriceList));
            }

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(gemPriceList);
                    context.SaveChanges();
                    TempData["SuccessMessage"] = "Gem Price List entry updated successfully.";
                    return RedirectToAction(nameof(GemPriceList));
                }
                catch (DbUpdateConcurrencyException)
                {
                    TempData["ErrorMessage"] = "Concurrency error occurred.";
                }
            }

            return View(gemPriceList);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("deletegempricelist/{gemID}")]
        public IActionResult DeleteGemPriceList(int gemID)
        {
            var gemPriceList = context.GemPriceLists.Find(gemID);
            if (gemPriceList == null)
            {
                TempData["ErrorMessage"] = "Gem Price List entry not found.";
                return RedirectToAction(nameof(GemPriceList));
            }

            try
            {
                context.GemPriceLists.Remove(gemPriceList);
                context.SaveChanges();
                TempData["SuccessMessage"] = "Gem Price List entry deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the Gem Price List entry: {ex.Message}";
            }

            return RedirectToAction(nameof(GemPriceList));
        }

        [Route("productlist")]
        public IActionResult ProductList()
        {
            var lstProDuct = db.Products.ToList();
            return View(lstProDuct);
        }

        [Route("createproduct")]
        [HttpGet]
        public IActionResult CreateProduct()
        {
            var product = context.Products.OrderByDescending(p => p.ProductCode).ToList();
            return View();
        }

        //xử lí yêu cầu gửi 
        //xử lí yêu cầu up dữ liệu lên

        [Route("createproduct")]
        [HttpPost]
        public IActionResult CreateProduct(ProductDto productDto)
        {
            //xác thực tệp hình ảnh, nếu nó null thì thêm lỗi vào trạng thái
            if (productDto.ImageUrl1 == null)
            {
                ModelState.AddModelError("ImageUrl1", "The image file is required");
            }
            if (productDto.ImageUrl2 == null)
            {
                ModelState.AddModelError("ImageUrl2", "The image file is required");
            }
            //ktra xem có bất kì lỗi xác thực nào trong sp ko, nếu trạng thái mô hình ko hợp lệ thì trả về 
            if (!ModelState.IsValid)
            {
                return View(productDto);
            }

            //lưu hình ảnh vào file
            string newFileName1 = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName1 += Path.GetExtension(productDto.ImageUrl1!.FileName);
            string newFileName2 = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName2 += Path.GetExtension(productDto.ImageUrl2!.FileName);

            string imageFullPath1 = environment.WebRootPath + "/images/product/" + newFileName1;
            string imageFullPath2 = environment.WebRootPath + "/images/product/" + newFileName2;
            using (var stream = System.IO.File.Create(imageFullPath1))
            {
                productDto.ImageUrl1.CopyTo(stream);

            }

            using (var stream = System.IO.File.Create(imageFullPath2))
            {

                productDto.ImageUrl2.CopyTo(stream);
            }
            Product product = new Product()
            {
                ProductCode = productDto.ProductCode,
                ProductName = productDto.ProductName,
                ImageUrl1 = newFileName1,
                ImageUrl2 = newFileName2,
                GemID = productDto.GemID,
				Weight = productDto.Weight,
                CategoryID = productDto.CategoryID,
                ProductionCost = productDto.ProductionCost,
                PriceRateID = productDto.PriceRateID,
                
            };

            context.Products.Add(product);
            context.SaveChanges();


            TempData["Message"] = "Add Product Successfully.";
            return RedirectToAction("ProductList", "HomeManager");
        }

        [Route("editproduct")]
        public IActionResult EditProduct(string ProductCode)
        {
            var product = context.Products.FirstOrDefault(p => p.ProductCode == ProductCode);
            if (product == null)
            {
                return RedirectToAction("ProductList", "HomeManager");
            }
            //tạo productDto from product
            var productDto = new ProductDto()
            {
                ProductCode = product.ProductCode,
                ProductName = product.ProductName,
                GemID = product.GemID,
                Weight = product.Weight,
                CategoryID = product.CategoryID,
                ProductionCost = product.ProductionCost,
                PriceRateID = product.PriceRateID,
            };

            ViewData["ProductCode"] = product.ProductCode;
            ViewData["ImageUrl1"] = product.ImageUrl1;
            ViewData["ImageUrl2"] = product.ImageUrl2;

            return View(productDto);
        }


        [Route("editproduct")]
        [HttpPost]
        public IActionResult EditProduct(string ProductCode, ProductDto productDto)
        {
            var product = context.Products.FirstOrDefault(p => p.ProductCode == ProductCode);
            if (product == null)
            {
                return RedirectToAction("ProductList", "HomeManager");
            }

            if (!ModelState.IsValid)
            {
                ViewData["ProductCode"] = product.ProductCode;
                ViewData["ImageUrl1"] = product.ImageUrl1;
                ViewData["ImageUrl2"] = product.ImageUrl2;
                return View(productDto);
            }

            productDto.ProductionCost = product.ProductionCost;
            productDto.PriceRateID = product.PriceRateID;

            // Initialize the file names with existing image URLs
            string newFileName1 = product.ImageUrl1;
            string newFileName2 = product.ImageUrl2;

            if (productDto.ImageUrl1 != null)
            {
                newFileName1 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(productDto.ImageUrl1.FileName);
                string imageFullPath1 = Path.Combine(environment.WebRootPath, "images/product", newFileName1);
                using (var stream = new FileStream(imageFullPath1, FileMode.Create))
                {
                    productDto.ImageUrl1.CopyTo(stream);
                }

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(product.ImageUrl1))
                {
                    string oldImageFullPath1 = Path.Combine(environment.WebRootPath, "images/product", product.ImageUrl1);
                    if (System.IO.File.Exists(oldImageFullPath1))
                    {
                        System.IO.File.Delete(oldImageFullPath1);
                    }
                }
            }

            if (productDto.ImageUrl2 != null)
            {
                newFileName2 = DateTime.Now.ToString("yyyyMMddHHmmssfff") + Path.GetExtension(productDto.ImageUrl2.FileName);
                string imageFullPath2 = Path.Combine(environment.WebRootPath, "images/product", newFileName2);
                using (var stream = new FileStream(imageFullPath2, FileMode.Create))
                {
                    productDto.ImageUrl2.CopyTo(stream);
                }

                // Delete the old image if it exists
                if (!string.IsNullOrEmpty(product.ImageUrl2))
                {
                    string oldImageFullPath2 = Path.Combine(environment.WebRootPath, "images/product", product.ImageUrl2);
                    if (System.IO.File.Exists(oldImageFullPath2))
                    {
                        System.IO.File.Delete(oldImageFullPath2);
                    }
                }
            }


            // Update the product in the database
            product.ProductCode = productDto.ProductCode;
            product.ProductName = productDto.ProductName;
            product.ImageUrl1 = newFileName1;
            product.ImageUrl2 = newFileName2;
            product.GemID = productDto.GemID;
            product.Weight = productDto.Weight;
            product.CategoryID = productDto.CategoryID;
            product.ProductionCost = productDto.ProductionCost;
            product.PriceRateID = productDto.PriceRateID;

            context.SaveChanges();
            return RedirectToAction("ProductList", "HomeManager");
        }






        [Route("deleteproduct")]
        public IActionResult DeleteProduct(string productCode)
        {
            var product = context.Products.FirstOrDefault(p => p.ProductCode == productCode);
            if (product == null)
            {
                TempData["Error"] = "Product not found.";
                return RedirectToAction("ProductList", "HomeManager");
            }

            try
            {
                string imageFullPath1 = Path.Combine(environment.WebRootPath, "images/product", product.ImageUrl1);
                string imageFullPath2 = Path.Combine(environment.WebRootPath, "images/product", product.ImageUrl2);

                // Check if the files exist before attempting to delete
                if (System.IO.File.Exists(imageFullPath1))
                {
                    System.IO.File.Delete(imageFullPath1);
                }
                else
                {
                    TempData["Warning"] = $"Image file {product.ImageUrl1} not found.";
                }

                if (System.IO.File.Exists(imageFullPath2))
                {
                    System.IO.File.Delete(imageFullPath2);
                }
                else
                {
                    TempData["Warning"] = $"Image file {product.ImageUrl2} not found.";
                }

                context.Products.Remove(product);
                context.SaveChanges();

                TempData["Message"] = "Product deleted successfully.";
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here for further analysis if needed
                TempData["Error"] = $"An error occurred while deleting the product: {ex.Message}";
            }

            return RedirectToAction("ProductList", "HomeManager");
        }

        [Route("orderlist")]
        public IActionResult OrderList()
        {
            var orders = context.Orders.ToList();
            return View(orders);
        }

        [Route("createorder")]
        [HttpGet]
        public IActionResult CreateOrder()
        {
            return View();
        }

        // POST: CreateOrder[
        [Route("createorder")]
        [HttpPost]
        public IActionResult CreateOrder(OrderDto orderDto)
        {
            // Log input data
            Console.WriteLine($"Received OrderDto: {Newtonsoft.Json.JsonConvert.SerializeObject(orderDto)}");

            if (!ModelState.IsValid)
            {
                Console.WriteLine("ModelState is not valid. Errors:");
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
                return View(orderDto);
            }

            try
            {
                var order = new Order
                {
                    UserID = orderDto.UserID,
                    TotalPrice = orderDto.TotalPrice,
                    TimeOrder = orderDto.TimeOrder,
                    Note = orderDto.Note,
                    OrderStatus = orderDto.OrderStatus
                };

                // Log the order object
                Console.WriteLine($"Created Order object: {Newtonsoft.Json.JsonConvert.SerializeObject(order)}");

                context.Orders.Add(order);
                var changes = context.SaveChanges();

                Console.WriteLine($"Changes saved to database: {changes}");

                if (changes > 0)
                {
                    TempData["SuccessMessage"] = "Order created successfully.";
                    return RedirectToAction(nameof(OrderList));
                }
                else
                {
                    ModelState.AddModelError("", "No changes were saved to the database.");
                    return View(orderDto);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred: {ex}");
                ModelState.AddModelError("", "Error saving order: " + ex.Message);
                return View(orderDto);
            }

        }

        [Route("editorder")]
        public IActionResult EditOrder(int orderId)
        {
            var order = context.Orders.FirstOrDefault(o => o.OrderID == orderId);
            if (order == null)
            {
                return RedirectToAction("OrderList", "HomeManager");
            }

            var orderDto = new OrderDto
            {
                OrderID = order.OrderID,
                UserID = order.UserID,
                TotalPrice = order.TotalPrice,
                TimeOrder = order.TimeOrder,
                Note = order.Note,
                OrderStatus = order.OrderStatus
            };

            ViewData["OrderID"] = order.OrderID;

            return View(orderDto);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("editorder")]
        public IActionResult EditOrder(int orderId, OrderDto orderDto)
        {
            var order = context.Orders.FirstOrDefault(o => o.OrderID == orderId);
            if (order == null)
            {
                return RedirectToAction("OrderList", "HomeManager");
            }

            if (!ModelState.IsValid)
            {
                ViewData["OrderID"] = order.OrderID;
                return View(orderDto);
            }

            order.UserID = orderDto.UserID;
            order.TotalPrice = orderDto.TotalPrice;
            order.TimeOrder = orderDto.TimeOrder;
            order.Note = orderDto.Note;
            order.OrderStatus = orderDto.OrderStatus;

            context.SaveChanges();

            TempData["Message"] = "Order updated successfully.";
            return RedirectToAction("OrderList", "HomeManager");
        }


        [Route("deleteorder")]
        public IActionResult DeleteOrder(int orderId)
        {
            try
            {
                var order = context.Orders.Find(orderId);
                if (order == null)
                {
                    TempData["ErrorMessage"] = "Order not found.";
                    return RedirectToAction("OrderList", "HomeManager");
                }

                context.Orders.Remove(order);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Order deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the order: {ex.Message}";
                // Log the exception here for further analysis if needed
            }

            return RedirectToAction("OrderList", "HomeManager");
        }

        [Route("discountlist")]
        public ActionResult DiscountList()
        {
            var discounts = context.Discounts.ToList();
            return View(discounts);
        }

        [HttpGet]
        [Route("createDiscount")]
        public IActionResult CreateDiscount()
        {
            return View();
        }

        [HttpPost]
        [Route("createDiscount")]
        public IActionResult CreateDiscount(DiscountDto discountDto)
        {
            if (!ModelState.IsValid)
            {
                return View(discountDto);
            }

            var discount = new Discount
            {
                OrderID = discountDto.OrderID,
                DiscountCode = discountDto.DiscountCode,
                DiscountAmount = discountDto.DiscountAmount,
                DiscountDate = discountDto.DiscountDate,
                DiscountStatus = discountDto.DiscountStatus
            };

            try
            {
                context.Discounts.Add(discount);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Discount created successfully.";
                return RedirectToAction("DiscountList", "HomeManager");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Failed to create discount: {ex.Message}";
                return View(discountDto);
            }
        }

        [HttpGet]
        [Route("editDiscount")]
        public IActionResult EditDiscount(int discountId)
        {
            // Retrieve the discount from your data source based on discountId
            var discount = context.Discounts.FirstOrDefault(d => d.DiscountID == discountId);

            if (discount == null)
            {
                return NotFound();
            }

            var discountDto = new DiscountDto
            {
                DiscountID = discount.DiscountID,
                OrderID = discount.OrderID,
                DiscountCode = discount.DiscountCode,
                DiscountAmount = discount.DiscountAmount,
                DiscountDate = discount.DiscountDate,
                DiscountStatus = discount.DiscountStatus
            };

            return View(discountDto);
        }

        [HttpPost]
        [Route("editDiscount")]
        public IActionResult EditDiscount(DiscountDto discountDto)
        {
            if (!ModelState.IsValid)
            {
                return View(discountDto);
            }

            try
            {
                // Retrieve the existing discount from your data source
                var discountToUpdate = context.Discounts.FirstOrDefault(d => d.DiscountID == discountDto.DiscountID);

                if (discountToUpdate == null)
                {
                    return NotFound();
                }

                // Update the properties of the retrieved discount entity
                discountToUpdate.DiscountCode = discountDto.DiscountCode;
                discountToUpdate.DiscountAmount = discountDto.DiscountAmount;
                discountToUpdate.DiscountDate = discountDto.DiscountDate;
                discountToUpdate.DiscountStatus = discountDto.DiscountStatus;

                // Save changes to the database
                context.SaveChanges();

                TempData["SuccessMessage"] = "Discount updated successfully.";
                return RedirectToAction("DiscountList");
            }
            catch (Exception ex)
            {
                // Log the exception and handle it accordingly
                TempData["ErrorMessage"] = $"An error occurred while updating the discount: {ex.Message}";
                return View(discountDto);
            }
        }

        //DELETEDISCOUNT
        [HttpGet]
        public IActionResult DeleteDiscount(int discountId)
        {
            var discount = context.Discounts.FirstOrDefault(d => d.DiscountID == discountId);
            if (discount == null)
            {
                TempData["ErrorMessage"] = "Discount not found.";
                return RedirectToAction("DiscountList");
            }

            return View(discount);
        }
        [HttpPost, ActionName("DeleteDiscount")]
        public IActionResult DeleteConfirmed(int discountId)
        {
            try
            {
                var discount = context.Discounts.FirstOrDefault(d => d.DiscountID == discountId);
                if (discount == null)
                {
                    TempData["ErrorMessage"] = "Discount not found.";
                    return RedirectToAction("DiscountList");
                }

                context.Discounts.Remove(discount);
                context.SaveChanges();

                TempData["SuccessMessage"] = "Discount deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"An error occurred while deleting the discount: {ex.Message}";
            }

            return RedirectToAction("DiscountList");
        }

        

        [Route("informationuser")]
        public IActionResult InformationUser()
        {
            var informationUser = context.Users.Where(u => u.RoleID == 5).ToList();
            return View(informationUser);
        }
        [Route("priceratelist")]
        public IActionResult PriceRateList()
        {
            var priceRateList = context.PriceRateLists.ToList();
            return View(priceRateList);
        }

        [HttpGet]
        [Route("createpricerate")]
        public IActionResult CreatePriceRate()
        {
            return View();
        }

        [HttpPost]
        [Route("createpricerate")]
        [ValidateAntiForgeryToken]
        public IActionResult CreatePriceRate(PriceRateList priceRateList)
        {
            if (!ModelState.IsValid)
            {   
                
                priceRateList.EffDate = DateTime.Now;
                context.Add(priceRateList);
                context.SaveChanges();
                return RedirectToAction(nameof(PriceRateList));
            }
            return View(priceRateList);
        }

        [HttpGet]
        [Route("editpricerate")]
        public IActionResult Edit(int id)
        {
            var priceRateList = context.PriceRateLists.Find(id);
            if (priceRateList == null)
            {
                return NotFound();
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, PriceRateList priceRateList)
        {
            if (id != priceRateList.PriceRateID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                var existingPriceRateList = context.PriceRateLists.Find(id);
                if (existingPriceRateList == null)
                {
                    return NotFound();
                }

                existingPriceRateList.PriceRate = priceRateList.PriceRate;
                context.Update(existingPriceRateList);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(priceRateList);
        }

        
        [Route("deletepricerate")]
        
        public IActionResult DeletePriceRate(int id)
        {
            var priceRateList = context.PriceRateLists.Find(id);
            if (priceRateList != null)
            {
                context.PriceRateLists.Remove(priceRateList);
                context.SaveChanges();
                TempData["SuccessMessage"] = "Price rate deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Item not found.";
            }
            return RedirectToAction("PriceRateList", "HomeManager", new { area = "Manager" });
        }





    }
}
