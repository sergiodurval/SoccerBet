using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SoccerBet.App.Interfaces;
using SoccerBet.App.ViewModels;

namespace SoccerBet.App.Controllers
{
    public class LoginController : Controller
    {
        private readonly IAccountService _accountService;

        public LoginController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = await _accountService.Login(loginViewModel);

            if(!user.Success)
            {
                ModelState.AddModelError(string.Empty, string.Join("<br/>", user.Errors));
                return View("Index", loginViewModel);
            }

            user.Email = loginViewModel.Email;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name , user.Email),
                new Claim("Token",string.Format($"Bearer {user.Token}"))
            };

            var claimsIdentity = new ClaimsIdentity(claims, "Login");
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Home");
        }
    }
}
