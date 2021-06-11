using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoccerBet.Api.ViewModels;
using SoccerBet.Business.Interfaces;
using SoccerBet.Test.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerBet.Test.Fixture
{
    public class AccountTestFixture
    {
        private readonly Mock<UserManager<IdentityUser>> userManagerMock;
        private readonly Mock<SignInManager<IdentityUser>> signManagerMock;
        private readonly Mock<INotification> notificationMock;
        private Business.Notifications.Notification notification;
        public AccountTestFixture()
        {
            userManagerMock = new Mock<UserManager<IdentityUser>>(
                                Mock.Of<IUserStore<IdentityUser>>(),
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null);

            signManagerMock = new Mock<SignInManager<IdentityUser>>(
                                    userManagerMock.Object,
                                    Mock.Of<IHttpContextAccessor>(),
                                    Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(),
                                    null,
                                    null,
                                    null,
                                    null);

            notificationMock = new Mock<INotification>();
        }

        public async Task<IActionResult> Register(RegisterUserViewModel registerUserViewModel)
        {
            if(((Microsoft.AspNetCore.Mvc.StatusCodeResult)TestModelHelper.CustomResponse(registerUserViewModel)).StatusCode == 400)
            {
                return new BadRequestResult();
            }
                
            var user = new IdentityUser
            {
                UserName = registerUserViewModel.Email,
                Email = registerUserViewModel.Email,
                EmailConfirmed = true
            };

            IdentityResult result = IdentityResult.Success;
            userManagerMock.Setup(s => s.CreateAsync(It.IsAny<IdentityUser>(), It.IsAny<string>())).ReturnsAsync(result);

            if (result.Succeeded)
            {
                var signManagerResult = new Mock<Task<SignInManager<IdentityUser>>>();
                signManagerMock.Setup(r => r.SignInAsync(user, false,null)).Returns(Task.FromResult(signManagerResult));
            }
            foreach (var error in result.Errors)
            {
                notification = new Business.Notifications.Notification(error.Description);
                notificationMock.Setup(r => r.Handle(notification));
            }

            return TestModelHelper.CustomResponse(registerUserViewModel);
        }

        public async Task<IActionResult> Login(LoginUserViewModel loginUserViewModel)
        {
            if (((Microsoft.AspNetCore.Mvc.StatusCodeResult)TestModelHelper.CustomResponse(loginUserViewModel)).StatusCode == 400)
            {
                return new BadRequestResult();
            }

            Microsoft.AspNetCore.Identity.SignInResult result = Microsoft.AspNetCore.Identity.SignInResult.Success;
            signManagerMock.Setup(r => r.PasswordSignInAsync(loginUserViewModel.Email, loginUserViewModel.Password, false, true)).Returns(Task.FromResult(result));

            if (result.Succeeded)
            {
                return TestModelHelper.CustomResponse(loginUserViewModel);
            }
            if (result.IsLockedOut)
            {
                notification = new Business.Notifications.Notification("Usuário temporariamente bloqueado por tentativas inválidas");
                notificationMock.Setup(r => r.Handle(notification));
                return TestModelHelper.CustomResponse(loginUserViewModel);
            }

            notification = new Business.Notifications.Notification("Usuário ou Senha incorretos");
            notificationMock.Setup(r => r.Handle(notification));
            return TestModelHelper.CustomResponse(loginUserViewModel);
        }
    }
}
