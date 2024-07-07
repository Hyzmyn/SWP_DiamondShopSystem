using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Repository.Models;
using Service.Services;

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
            var user = context.Users.FirstOrDefault(u => u.UserID == id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [Route("edituser")]
        public IActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var existingUser = context.Users.FirstOrDefault(u => u.UserID == user.UserID);
                if (existingUser != null)
                {
                    existingUser.Username = user.Username;
                    existingUser.Password = user.Password;
                    existingUser.Email = user.Email;
                    existingUser.PhoneNumber = user.PhoneNumber;
                    existingUser.Address = user.Address;
                    existingUser.RoleID = user.RoleID;
                    existingUser.UserStatus = user.UserStatus;
                    existingUser.NiSize = user.NiSize;
                    existingUser.CreatedAt = user.CreatedAt;

                    try
                    {
                        context.SaveChanges();
                        return RedirectToAction("InformationUser");
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Unable to save changes: {ex.Message}");
                    }
                }
            }
            return View(user);
        }

       

    }
}
