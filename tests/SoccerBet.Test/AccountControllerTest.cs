using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoccerBet.Api.Controllers;
using SoccerBet.Business.Interfaces;
using SoccerBet.Test.Builders;
using System.Threading.Tasks;
using Xunit;


namespace SoccerBet.Test
{

    public class AccountControllerTest
    {
        
        //todo:arrumar este teste
        [Fact(DisplayName = "create user")]
        public async Task RegisterUserSuccessfully()
        {
            //Arrange
            var registerUserViewModel = UserBuilder.New().Build();
            var mockNotification = new Mock<INotification>();
            var userManagerMock = new Mock<UserManager<IdentityUser>>(
                                Mock.Of<IUserStore<IdentityUser>>(),
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null,
                                null);

            var signInManagerMock = new Mock<SignInManager<IdentityUser>>(
                                    userManagerMock.Object,
                                    Mock.Of<IHttpContextAccessor>(),
                                    Mock.Of<IUserClaimsPrincipalFactory<IdentityUser>>(),
                                    null,
                                    null,
                                    null,
                                    null);

            var user = new IdentityUser
            {
                UserName = registerUserViewModel.UserName,
                Email = registerUserViewModel.Email,
                EmailConfirmed = true
            };

            var mockIdentityResult = new Mock<IdentityResult>();
            //mockIdentityResult.Setup(r => r.Succeeded == true);

            userManagerMock.Setup(r => r.CreateAsync(user, registerUserViewModel.Password)).Returns(Task.FromResult(mockIdentityResult.Object));

            var controller = new AccountController(mockNotification.Object, signInManagerMock.Object, userManagerMock.Object);
            
            //Act

            var actionResult = await controller.Register(registerUserViewModel);
            userManagerMock.Verify(r => r.CreateAsync(It.Is<IdentityUser>(c => c.UserName == user.UserName && c.Email == user.Email)));

            //Assert
            Assert.IsType<OkResult>(actionResult);
        }
    }
}
