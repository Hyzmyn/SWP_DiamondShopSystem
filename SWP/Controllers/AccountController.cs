using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Service.Services;

public class AccountController : Controller
{

	private readonly DiamondShopContext db;
	private readonly IUserService userService;
	public AccountController(DiamondShopContext dbContext, IUserService user)
	{
		db = dbContext;
		userService = user;

	}

	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(string username, string password)
	{
		var user = await userService.LoginAsync(username, password);

        if (user != null)
		{
			HttpContext.Session.SetString("UserId", user.UserID.ToString());
			HttpContext.Session.SetInt32("RoleID", (int)user.RoleID);
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

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string username, string password, string checkPassword)
    {
        try
        {
            if (password != checkPassword)
            {
                throw new Exception("password doesn't match");
            }

            var newUser = new User
            {
                Username = username,
                Password = password
            };

            await userService.AddUserAsync(newUser);
            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError("", $"Error: {ex.Message}");
            return RedirectToAction("Index", "Home");
        }
    }


    public IActionResult Logout()
	{
		HttpContext.Session.Clear();
		return RedirectToAction("Index", "Home");
	}
}
