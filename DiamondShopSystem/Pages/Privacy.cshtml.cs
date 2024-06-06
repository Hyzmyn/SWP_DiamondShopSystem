using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Entities;
using Service.Interface;
using System.Net;

namespace DiamondShopSystem.Pages
{
    public class PrivacyModel : PageModel
    {

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }


        private readonly IUserService _userService;

        public PrivacyModel(IUserService userService)
        {
            _userService = userService;
        }

        public void OnPost()
        {
            var user = new User
            {
                UserName = Username,
                Password = Password,
                Email = Email
            };
            if (user == null)
            {
                NotFound();
            }
            else
            {
                _userService.AddUser(user);
            }
        }
    }
}
