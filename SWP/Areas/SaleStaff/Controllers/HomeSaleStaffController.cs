using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;
using Microsoft.AspNetCore.Http;
using System;
using Service.Services;



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
		public HomeSaleStaffController(DiamondShopContext context, IWebHostEnvironment environment)
        {
            this.context = context;
			this.environment = environment;
			

        }
	   [Route("")]
        [Route("salestaff")]
       
        public IActionResult Index()
        {
            return View();
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
			if(productDto.ImageUrl1 == null)
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
                MaterialID = productDto.MaterialID,
                CategoryID = productDto.CategoryID,
                ProductionCost = productDto.ProductionCost,
                PriceRateID = productDto.PriceRateID,
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
                MaterialID = product.MaterialID,
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


			// Update the product in the database
			product.ProductCode = productDto.ProductCode;
			product.ProductName = productDto.ProductName;
			product.ImageUrl1 = newFileName1;
			product.ImageUrl2 = newFileName2;
			product.GemID = productDto.GemID;
			product.MaterialID = productDto.MaterialID;
			product.CategoryID = productDto.CategoryID;
			product.ProductionCost = productDto.ProductionCost;
			product.PriceRateID = productDto.PriceRateID;

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





		[Route("historyorder")]


        public IActionResult HistoryOrder()
        {
            return View();
        }


    }
}
