using ExpectedObjects;
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

        [Fact(DisplayName = "Get matchs by leagueid")]
        public void GetMatchesLeagueSuccessfully()
        {
            throw new NotImplementedException();
        }

        
    }
}
