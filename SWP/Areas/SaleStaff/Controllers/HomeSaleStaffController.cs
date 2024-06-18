using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Repository.Models;
using Service.Interface;
using Microsoft.AspNetCore.Http;



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
                PriceRate = productDto.PriceRate,
            };

            context.Products.Add(product);
            context.SaveChanges();


			TempData["Message"] = "Add Product Successfully.";
			return RedirectToAction("ProductList", "HomeSaleStaff");
		}


		[Route("historyorder")]


        public IActionResult HistoryOrder()
        {
            return View();
        }


    }
}
