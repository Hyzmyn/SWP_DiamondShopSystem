using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repository.Entities;
using Service.Interface;
using Service.Service;

namespace DiamondShopSystem.Pages
{
    public class IndexModel : PageModel
    {

        [BindProperty]
        public string Username { get; set; }
        [BindProperty]
        public string Password { get; set; }


        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public IndexModel(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        public void OnPost()
        {
            try
            {
                User user = _userService.Login(Username, Password);
                if (user != null)
                {
                    Console.WriteLine(Username + ":" + Password);
                    string token = _jwtService.GenerateToken(user.UserId, (int)user.RoleId);
                    Console.WriteLine(token);
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


    }

}
