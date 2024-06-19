using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

public class AccountController : Controller
{
	private readonly DiamondShopContext db;

	public AccountController(DiamondShopContext dbContext)
	{
		db = dbContext;
	}

	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(string username, string password)
	{
		var user = await db.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
		if (user != null)
		{
			HttpContext.Session.SetString("UserId", user.UserID.ToString());
			HttpContext.Session.SetInt32("RoleID", user.RoleID);
			return RedirectToAction("Index", "Home");
		}
		else
		{
			ModelState.AddModelError("", "Invalid username or password");
			return View();
		}
	}


	public IActionResult Logout()
	{
		HttpContext.Session.Clear();
		return RedirectToAction("Index", "Home");
	}
}


//public IActionResult Register(string username, string password, string checkPassword)
//{
//    try
//    {
//        if (password != checkPassword)
//        {
//            return BadRequest("Passwords do not match.");
//        }

//        User newUser = new User
//        {
//            Username = username,
//            Password = password, // Note: Store passwords securely using hashing
//                                 // Set other properties as needed
//        };

//        _userService.AddUser(newUser); // Assuming _userService is your service with the AddUser method

//        return Ok("User registered successfully.");
//    }
//    catch (Exception ex)
//    {
//        return BadRequest(ex.Message);
//    }
//}
//    }