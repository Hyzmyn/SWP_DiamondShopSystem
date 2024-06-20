using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Service.Services.Users;

public class AccountController : Controller
{
	private readonly IUserService _userService;

	public AccountController(IUserService userService)
	{
        _userService = userService;
    }

	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(string username, string password)
	{
        User user = await _userService.LoginAsync(username, password);
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


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(string username, string password, string checkPassword)
    {
        try
        {
            if (password != checkPassword)
            {
                return BadRequest("Passwords do not match.");
            }

            User newUser = new User
            {
                Username = username,
                Password = password, 
            };

            await _userService.AddUserAsync(newUser);

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }


    public IActionResult Logout()
	{
		HttpContext.Session.Clear();
		return RedirectToAction("Index", "Home");
	}
}



   