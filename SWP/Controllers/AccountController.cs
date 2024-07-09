using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Service.Services;
using Service.ViewModel;

public class AccountController : Controller
{

	private readonly DiamondShopContext db;
	private readonly IUserService userService;
    private readonly EmailService _emailService;
	public AccountController(DiamondShopContext dbContext, IUserService user, EmailService emailService)
	{
		db = dbContext;
		userService = user;
        _emailService = emailService;
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

        if (user != null && user.UserStatus == true)
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
    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await userService.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return RedirectToAction("ForgotPassword");
        }

        var token = Guid.NewGuid().ToString();
        await userService.SavePasswordResetTokenAsync(user.Email.ToString(), token);

        var callbackUrl = Url.Action("ResetPassword", "Account", new { token }, protocol: Request.Scheme);
        var message = $"Please reset your password by clicking <a href='{callbackUrl}'>here</a>";

        await _emailService.SendEmailAsync(model.Email, "Reset Password", message);

        return View("ForgotPasswordConfirmation");
    }

    [HttpGet]
    public IActionResult ResetPassword(string token)
    {
        var model = new ResetPasswordViewModel { Token = token };
        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var result = await userService.ResetPasswordAsync(model.Token, model.Email, model.Password);
        if (result)
        {
            return RedirectToAction("Index", "Home");
        }

        ModelState.AddModelError(string.Empty, "Invalid token or email.");
        return View(model);
    }

}
