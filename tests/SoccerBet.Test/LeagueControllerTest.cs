using Microsoft.AspNetCore.Mvc;
using Moq;
using SoccerBet.Api.Controllers;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Test.Builders;
using SoccerBet.Test.Fixture;
using System;
using System.Collections.Generic;
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
        public void MustReturnAllLeagues()
        {
            //Arrange
            var leagues = LeagueBuilder.New().Build();

            //Act
            var result = _leagueService.GetAll();
            var resultToList = (List<League>)result;

            //Assert
            Assert.Equal(leagues.Count, resultToList.Count);
        }

        [Theory(DisplayName = "Get matchs by leagueid")]
        [InlineData("e5cc3844-30ad-4977-8045-6d35d6844190")]
        public void GetMatchesLeagueSuccessfully(string leagueId)
        {
            //Act
            var result = _leagueService.GetAllMatchs(Guid.Parse(leagueId));

            //Assert
            Assert.IsType<League>(result);
        }

        [Theory(DisplayName = "Get matchs by leagueid but return is null")]
        [InlineData("7df5d9bf-d645-4737-bf98-2a6d548570f6")]
        public async void GetMatchesLeagueNotSuccessfully(string leagueId)
        {
            //Arrange
            var mockService = new Mock<ILeagueService>();
            var league = LeagueBuilder.New().GenerateLeagueWithMatchs(Guid.Parse(leagueId));
            mockService.Setup(r => r.GetAllMatchs(Guid.Parse(leagueId))).ReturnsAsync(league);
            var controller = new LeagueController(mockService.Object);

            //Act
            var actionResult = await controller.GetMatchByLeagueId(Guid.Parse(leagueId));

            //Assert
            Assert.IsType<NotFoundResult>(actionResult);
        }



    }
}
