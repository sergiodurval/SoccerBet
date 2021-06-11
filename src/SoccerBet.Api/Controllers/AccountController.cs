using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SoccerBet.Api.ViewModels;
using SoccerBet.Business.Interfaces;

namespace SoccerBet.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController(INotification notification, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager) : base(notification)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = registerUserViewModel.Email,
                Email = registerUserViewModel.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, registerUserViewModel.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
            }
            foreach (var error in result.Errors)
            {
                NotifyError(error.Description);
            }


            return CustomResponse(registerUserViewModel);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserViewModel loginUserViewModel)
        {
            if (!ModelState.IsValid)
                return CustomResponse(loginUserViewModel);

            var result = await _signInManager.PasswordSignInAsync(loginUserViewModel.Email, 
                                                                  loginUserViewModel.Password, 
                                                                  false, 
                                                                  true);
            if (result.Succeeded)
            {
                return CustomResponse(loginUserViewModel);
            }
            if (result.IsLockedOut)
            {
                NotifyError("Usuário temporariamente bloqueado por tentativas inválidas");
                return CustomResponse(loginUserViewModel);
            }

            NotifyError("Usuário ou Senha incorretos");
            return CustomResponse(loginUserViewModel);
        }
    }
}
