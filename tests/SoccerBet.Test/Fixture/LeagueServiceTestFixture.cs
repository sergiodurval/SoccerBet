using Moq;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Business.Services;
using SoccerBet.Test.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerBet.Test.Fixture
{
    public class LeagueServiceTestFixture
    {
        private readonly Mock<ILeagueRepository> _leagueRepositoryMock;
        private readonly LeagueService _leagueService;

        public LeagueServiceTestFixture()
        {
            _leagueRepositoryMock = new Mock<ILeagueRepository>();
            _leagueService = new LeagueService(_leagueRepositoryMock.Object);
        }

        public IEnumerable<League> GetAll()
        {
            try
            {
                var leagues = LeagueBuilder.New().Build();
                _leagueRepositoryMock.Setup(r => r.GetAll()).Returns(Task.FromResult((IEnumerable<League>)leagues));

                return leagues;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public League GetAllMatchs(Guid leagueId)
        {
            try
            {
                var league = LeagueBuilder.New().GenerateLeagueWithMatchs(leagueId);
                _leagueRepositoryMock.Setup(r => r.GetAllMatchs(leagueId)).Returns(Task.FromResult(league));

                return league;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
