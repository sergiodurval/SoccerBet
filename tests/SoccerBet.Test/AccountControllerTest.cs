using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoccerBet.Api.Controllers;
using SoccerBet.Business.Interfaces;
using SoccerBet.Test.Builders;
using SoccerBet.Test.Fixture;
using System.Threading.Tasks;
using Xunit;


namespace SoccerBet.Test
{

    public class AccountControllerTest
    {
        
        [Fact(DisplayName = "create user")]
        public async Task RegisterUserSuccessfully()
        {
            //Arrange
            var registerUserViewModel = UserBuilder.New().Build();

            //Act
            var fixture = new AccountTestFixture();
            var result = await fixture.Register(registerUserViewModel);

            //Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact(DisplayName = "create user but errors ocurred")]
        public async Task RegisterUserNotSuccessfully()
        {
            //Arrange
            var registerUserViewModel = UserBuilder.New().CreateRegisterUserWithNoConfirmPassword();

            //Act
            var fixture = new AccountTestFixture();
            var result = await fixture.Register(registerUserViewModel);


            //Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
