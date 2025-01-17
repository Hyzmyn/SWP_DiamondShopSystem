﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;
using Service.Services;
using Microsoft.AspNetCore.Http;
using System;
using Microsoft.EntityFrameworkCore;



namespace SWP.Areas.SaleStaff.Controllers
{

    [Area("salestaff")]
    [Route("salestaff")]
    [Route("salestaff/homesalestaff")]
    public class HomeSaleStaffController : Controller
    {
        private readonly DiamondShopContext context;
        private readonly IWebHostEnvironment environment;
        private readonly IProductService _productService;
        DiamondShopContext db = new DiamondShopContext();
        public HomeSaleStaffController(DiamondShopContext context, IWebHostEnvironment environment, IProductService productService)
        {
            this.context = context;
            this.environment = environment;
			_productService = productService;


		}

        [Route("")]
        [Route("homesalestaff")]

        public IActionResult HomeSaleStaff()
        {
            return View();
        }

        [Route("productlist")]
        public IActionResult ProductList()
        {
            var lstProDuct = db.Products.Include(p => p.Gems).Include(p => p.PriceRateLists).ToList(); 
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
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
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
            productDto.TotalCost = await _productService.CalculateProductPrice(productDto.PriceRateID, productDto.GemID, productDto.Weight, productDto.ProductionCost);
            Product product = new Product()
            {
                ProductCode = productDto.ProductCode,
                ProductName = productDto.ProductName,
                ImageUrl1 = newFileName1,
                ImageUrl2 = newFileName2,
                GemID = productDto.GemID,
                Weight = productDto.Weight,
                CategoryID = 1,
                ProductionCost = productDto.ProductionCost,
                PriceRateID = productDto.PriceRateID,
                TotalCost = productDto.TotalCost,
            };
            
            context.Products.Add(product);
            context.SaveChanges();


            TempData["Message"] = "Add Product Successfully.";
            return RedirectToAction("ProductList", "HomeSaleStaff");
        }

        [Route("editproduct")]
        public IActionResult EditProduct(string ProductCode)
        {
            var product = context.Products.FirstOrDefault(p => p.ProductCode == ProductCode);
            if (product == null)
            {
                return RedirectToAction("ProductList", "HomeSaleStaff");
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
        public async Task<IActionResult> EditProduct(string ProductCode, ProductDto productDto)
        {
            var product = context.Products.FirstOrDefault(p => p.ProductCode == ProductCode);
            if (product == null)
            {
                return RedirectToAction("ProductList", "HomeSaleStaff");
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
            var price = await _productService.CalculateProductPrice(productDto.PriceRateID, productDto.GemID, productDto.Weight, productDto.ProductionCost);

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
            product.TotalCost = price;

            context.SaveChanges();
            return RedirectToAction("ProductList", "HomeSaleStaff");
        }






        [Route("deleteproduct")]
        public IActionResult DeleteProduct(string productCode)
        {
            var product = context.Products.FirstOrDefault(p => p.ProductCode == productCode);
            if (product == null)
            {
                TempData["Error"] = "Product not found.";
                return RedirectToAction("ProductList", "HomeSaleStaff");
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

            return RedirectToAction("ProductList", "HomeSaleStaff");
        }

        [Route("orderlist")]
        public IActionResult OrderList()
        {
            var orders = context.Orders.Where(o => o.OrderStatus == false).ToList();
            return View(orders);
        }

        
        [Route("historyorder")]
        public IActionResult HistoryOrder()
        {
            var historyOrders = context.Orders.Where(o => o.OrderStatus == true).ToList(); // Assuming inactive orders are historical
            return View(historyOrders);
        }

        [Route("orderdetails")]
        public IActionResult OrderDetails(int orderId)
        {
            var order = context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefault(o => o.OrderID == orderId);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
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
                return RedirectToAction("DiscountList", "HomeSaleStaff");
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

        [Route("gempricelist")]
        public IActionResult GemPriceList()
        {
            var gemPriceList = context.GemPriceLists.ToList();
            return View(gemPriceList);
        }

        [Route("materialpricelist")]
        public IActionResult MaterialPriceList()
        {
            var materialPriceList = context.MaterialPriceLists.ToList();
            return View(materialPriceList);
        }

        [Route("gem")]
        public IActionResult Gem()
        {
            
            return View();
        }


}
}
