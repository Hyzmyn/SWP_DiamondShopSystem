using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Repository.Models;
using Service.Services;
using Service.ViewModel;
using System.Diagnostics;

namespace SWP.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    public class HomeAdminController : Controller
    {
        private readonly DiamondShopContext context;
        private readonly IWebHostEnvironment environment;
        DiamondShopContext db = new DiamondShopContext();
        public HomeAdminController(DiamondShopContext context, IWebHostEnvironment environment, DiamondShopContext db)
        {
            this.context = context;
            this.environment = environment;
            this.db = db;
           
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            return View();
        }
        [Route("informationuser")]
        public IActionResult InformationUser()
        {
            var informationUser = context.Users.ToList();
            return View(informationUser);
        }
        [HttpGet]
        [Route("edituser")]
        public IActionResult Edit(int id)
        {
            var user = context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [Route("edituser")]
        public IActionResult Edit(int id, User updatedUser)
        {
            
            var user = context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            // Chỉ cập nhật các trường được chỉnh sửa
            if (!string.IsNullOrWhiteSpace(updatedUser.Username))
                user.Username = updatedUser.Username;
            if (!string.IsNullOrWhiteSpace(updatedUser.Email))
                user.Email = updatedUser.Email;
            if (!string.IsNullOrWhiteSpace(updatedUser.PhoneNumber))
                user.PhoneNumber = updatedUser.PhoneNumber;
            if (!string.IsNullOrWhiteSpace(updatedUser.Address))
                user.Address = updatedUser.Address;
            if (updatedUser.NiSize != null)
                user.NiSize = updatedUser.NiSize;
            if (!string.IsNullOrWhiteSpace(updatedUser.Password))
                user.Password = updatedUser.Password;
            if (updatedUser.RoleID != 0)
                user.RoleID = updatedUser.RoleID;
            if (updatedUser.UserStatus != null)
                user.UserStatus = updatedUser.UserStatus;

            context.Users.Update(user);
            context.SaveChanges();

            TempData["SuccessMessage"] = "User updated successfully.";
            return RedirectToAction("InformationUser");
        }

        

    }
}
