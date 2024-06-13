using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Entities;
using Service.Interface;
using Service.Service;
using System.ComponentModel.DataAnnotations;

namespace DiamondShopSystem.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        [Required]
        public string UserName { get; set; }

        [BindProperty]
        [Required]
        public string Password { get; set; }

        [BindProperty]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Message { get; set; }

        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public IndexModel(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public void OnPostLogin()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
                {
                    Message = "Please fill in all fields.";
                    throw new Exception(Message);
                }

                var user = _userService.Login(UserName, Password);
                if (user != null)
                {
                    Message = "User Logged In";
                    string token = _jwtService.GenerateToken(user.UserId, (int)user.RoleId);
                    Console.WriteLine(Message);
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Console.WriteLine(Message);
            }
        }

        public void OnPostRegister()
        {
            try
            {
                if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Email))
                {
                    Message = "Please fill in all fields.";
                    throw new Exception(Message);
                }

                var user = new User
                {
                    Username = UserName,
                    Password = Password,
                    Email = Email
                };

                _userService.AddUser(user);
                Message = "User Added";
                Console.WriteLine(Message);
            }
            catch (Exception ex)
            {
                Message = ex.Message;
                Console.WriteLine(Message);
            }
        }

    }

}
