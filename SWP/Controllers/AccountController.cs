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
            HttpContext.Session.SetString("Username", user.Username);
            TempData["ResetInputs"] = true;
			return RedirectToAction("Index", "Home");
		}
		else
		{
			TempData["LoginError"] = "Invalid username or password";
			TempData["ResetInputs"] = true;
			return RedirectToAction("Index", "Home");

		}
	}



	public IActionResult Logout()
	{
		HttpContext.Session.Clear();
		return RedirectToAction("Index", "Home");
	}
}
