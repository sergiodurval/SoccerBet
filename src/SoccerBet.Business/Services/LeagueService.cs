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
    }
}
