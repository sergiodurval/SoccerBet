using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoccerBet.Api.Controllers;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Notifications;
using SoccerBet.Test.Builders;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Xunit;

namespace SoccerBet.Test
{
    public class BetControllerTest
    {
        Faker fake;

        public BetControllerTest()
        {
            fake = new Faker();
        }

        [Fact(DisplayName = "send bet successfuly")]
        public async Task SendBetSuccessfully()
        {
            //Arrange
            var bet = BetBuilder.New().Build();
            var mockBetService = new Mock<IBetService>();
            mockBetService.Setup(r => r.SendBet(bet)).Returns(Task.FromResult(bet));
            var mockNotification = new Mock<INotification>();
            var mockAuthorizeUser = new Mock<ControllerContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, fake.Person.FirstName),
                                        new Claim(ClaimTypes.Name, fake.Person.Email)}));

            var betController = new BetController(mockNotification.Object, mockBetService.Object);
            betController.ControllerContext = new ControllerContext();
            betController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            //Act
            var actionResult = await betController.SendBet(bet);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact(DisplayName = "send bet not successfuly")]
        public async Task SendBetNotSuccessfuly()
        {
            //Arrange
            var bet = BetBuilder.New().GenerateInvalidBet();
            var mockBetService = new Mock<IBetService>();
            mockBetService.Setup(r => r.SendBet(bet)).Returns(Task.FromResult(bet));
            var mockNotification = new Mock<INotification>();
            mockNotification.Setup(r => r.GetNotifications()).Returns(CreateNotifications());
            mockNotification.Setup(r => r.HasNotification()).Returns(true);
            var mockAuthorizeUser = new Mock<ControllerContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, fake.Person.FirstName),
                                        new Claim(ClaimTypes.Name, fake.Person.Email)}));

            var betController = new BetController(mockNotification.Object, mockBetService.Object);
            betController.ControllerContext = new ControllerContext();
            betController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            //Act
            var actionResult = await betController.SendBet(bet);

            //Assert
            Assert.IsType<BadRequestObjectResult>(actionResult);
        }

        [Theory(DisplayName = "find bet by userId successfuly")]
        [InlineData("3dac7b4b-df76-4dc1-8dc2-21507e6fd8e7")]
        public async Task FindBetSuccessfully(string userId)
        {
            //Arrange
            var mockBetService = new Mock<IBetService>();
            var result = BetBuilder.New().GenerateBetList();
            mockBetService.Setup(r => r.GetBetByUserId(userId)).Returns(Task.FromResult(result));
            var mockNotification = new Mock<INotification>();
            var mockAuthorizeUser = new Mock<ControllerContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, fake.Person.FirstName),
                                        new Claim(ClaimTypes.Name, fake.Person.Email)}));

            var betController = new BetController(mockNotification.Object, mockBetService.Object);
            betController.ControllerContext = new ControllerContext();
            betController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            //Act
            var actionResult = await betController.FindBet(userId);

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Fact(DisplayName = "find bet not successfuly")]
        public async Task FindBetNotSuccessfully()
        {
            //Arrange
            var mockBetService = new Mock<IBetService>();
            var mockNotification = new Mock<INotification>();
            var mockAuthorizeUser = new Mock<ControllerContext>();
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, fake.Person.FirstName),
                                        new Claim(ClaimTypes.Name, fake.Person.Email)}));

            var betController = new BetController(mockNotification.Object, mockBetService.Object);
            betController.ControllerContext = new ControllerContext();
            betController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            //Act
            var actionResult = await betController.FindBet(string.Empty);

            //Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }

        public List<Notification> CreateNotifications()
        {
             var listNotification = new List<Notification>()
             {
                 new Notification(fake.Lorem.ToString())
             };

            return listNotification;
        }
    }
}
