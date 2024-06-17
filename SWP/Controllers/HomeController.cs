using Microsoft.AspNetCore.Mvc;
using Repository.Models;
using Service.Interface;
using Service.Service;
using Service.Service.ViewModels;
using System.Diagnostics;

namespace SWP.Controllers
{

    public class HomeController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Index()
        {

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            try
            {
                var user = _userService.Login(username, password);
                if (user != null)
                {
                    // Login successful, redirect to a secure page
                    Console.WriteLine("login success");
                    return View("Index");

                }
                else
                {
                    // Login failed, return to the login page with an error message
                    ViewBag.ErrorMessage = "Invalid username or password";
                    return View("Index");
                }
            }
            catch (Exception ex)
            {
                // Log the exception and display a generic error message
                _logger.LogError(ex, "An error occurred while logging in");
                ViewBag.ErrorMessage = "An error occurred, please try again later";
                return View("Error");
            }
        }

        public IActionResult register(string username, string password, string checkPassword)
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
                    Password = password, // Note: Store passwords securely using hashing
                                         // Set other properties as needed
                };

                _userService.AddUser(newUser); // Assuming _userService is your service with the AddUser method

                return Ok("User registered successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
