using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Entities;
using Service.Interface;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace DiamondShopSystem.Pages
{
    public class PrivacyModel : PageModel
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

        public PrivacyModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnPost()
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
