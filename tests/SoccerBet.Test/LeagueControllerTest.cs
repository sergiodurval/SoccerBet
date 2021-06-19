using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SoccerBet.Api.Configuration;
using SoccerBet.Api.Controllers;
using SoccerBet.Business.Interfaces;
using SoccerBet.Test.Builders;
using SoccerBet.Test.Fixture;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SoccerBet.Test
{

    public class LeagueControllerTest
    {
        private readonly LeagueServiceTestFixture _leagueService;

        public LeagueControllerTest()
        {
            _leagueService = new LeagueServiceTestFixture();
        }

        [Fact(DisplayName = "GetAll leagues")]
        public async Task MustReturnAllLeagues()
        {
            //Arrange
            var leagues = LeagueBuilder.New().Build();
            var mockNotification = new Mock<INotification>();
            var mockLeagueService = new Mock<ILeagueService>();
            var mockMapper = new Mock<IMapper>();
            mockLeagueService.Setup(r => r.GetAll()).Returns(Task.FromResult(leagues));
            var controller = new LeagueController(mockNotification.Object, mockLeagueService.Object , mockMapper.Object);

            //Act
            var actionResult = await controller.Index();

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Theory(DisplayName = "Get matchs by leagueid")]
        [InlineData("e5cc3844-30ad-4977-8045-6d35d6844190")]
        public async Task GetMatchesLeagueSuccessfully(string leagueId)
        {
            //Arrange
            var result = _leagueService.GetAllMatchs(Guid.Parse(leagueId));
            var mockNotification = new Mock<INotification>();
            var mockLeagueService = new Mock<ILeagueService>();
            mockLeagueService.Setup(r => r.GetAllMatchs(Guid.Parse(leagueId))).Returns(Task.FromResult(result));
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfig());
            });

            var mapper = mockMapper.CreateMapper();
            var controller = new LeagueController(mockNotification.Object, mockLeagueService.Object , mapper: mapper);

            //Act
            var actionResult = await controller.GetMatchByLeagueId(Guid.Parse(leagueId));

            //Assert
            Assert.IsType<OkObjectResult>(actionResult);
        }

        [Theory(DisplayName = "Get matchs by leagueid but return is null")]
        [InlineData("7df5d9bf-d645-4737-bf98-2a6d548570f6")]
        public async void GetMatchesLeagueNotSuccessfully(string leagueId)
        {
            //Arrange
            var mockService = new Mock<ILeagueService>();
            var mockNotification = new Mock<INotification>();
            var mockMapper = new Mock<IMapper>();
            var league = LeagueBuilder.New().GenerateLeagueWithMatchs(Guid.Parse(leagueId));
            mockService.Setup(r => r.GetAllMatchs(Guid.Parse(leagueId))).ReturnsAsync(league);
            var controller = new LeagueController(mockNotification.Object , mockService.Object , mockMapper.Object);

            //Act
            var actionResult = await controller.GetMatchByLeagueId(Guid.Parse(leagueId));

            //Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }



    }
}
