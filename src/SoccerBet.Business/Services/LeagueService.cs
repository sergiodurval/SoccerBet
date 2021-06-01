using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerBet.Business.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly ILeagueRepository _leagueRepository;

        public LeagueService(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        public async Task<IEnumerable<League>> GetAll()
        {
            try
            {
               var leagues = await _leagueRepository.GetAll();
               return leagues;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<League> GetAllMatchs(Guid leagueId)
        {
            try
            {
                var league = await _leagueRepository.GetAllMatchs(leagueId);
                return league;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
