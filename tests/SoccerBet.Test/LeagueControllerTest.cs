using ExpectedObjects;
using Microsoft.AspNetCore.Mvc;
using SoccerBet.Business.Models;
using SoccerBet.Test.Builders;
using SoccerBet.Test.Fixture;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
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
        public void GetMatchesLeagueNotSuccessfully(string leagueId)
        {
            //Act
            var league = _leagueService.GetAllMatchs(Guid.Parse(leagueId));
            IHttpActionResult result;

            //arrange
            if(league == null)
            {
                result = new NotFoundResult(new System.Net.Http.HttpRequestMessage());
            }
            else
            {
                result = new OkResult(new System.Net.Http.HttpRequestMessage());
            }

            Assert.IsType(typeof(NotFoundResult),result);
        }



    }
}
