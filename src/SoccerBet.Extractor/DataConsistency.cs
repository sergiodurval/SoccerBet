using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using SoccerBet.Business.Interfaces;
using SoccerBet.Business.Models;
using SoccerBet.Extractor.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;
using SoccerBet.Extractor.Interfaces;

namespace SoccerBet.Extractor
{
    

    public class DataConsistency : IDataConsistency
    {
        
        private readonly IMapper _mapper;
        private readonly ILeagueRepository _leagueRepository;
        private ILogger _logger;
        public DataConsistency(IMapper mapper , 
                               ILeagueRepository leagueRepository , 
                               ILogger<DataConsistency> logger)
        {
            _mapper = mapper;
            _leagueRepository = leagueRepository;
            _logger = logger;
        }

        public void ConsistencyRule(List<LeagueExtractModel> leagues)
        {
            foreach(var league in leagues)
            {
                _logger.LogInformation($"Liga:{league.Name} - Quantidade de rodadas:{league.Rounds.Count}");
                DecoupleData(league);
            }
        }

        private async Task<LeagueExtractModel> ValidateLeagueExists(string leagueName)
        {
            var leagueExtractModel = _mapper.Map<LeagueExtractModel>(await _leagueRepository.SearchByName(leagueName));
            return leagueExtractModel;
        }

        private async Task<LeagueExtractModel> AddLeague(LeagueExtractModel leagueExtractModel)
        {
            var league = await ValidateLeagueExists(leagueExtractModel.Name);

            if(league == null)
            {
                return _mapper.Map<LeagueExtractModel>(await _leagueRepository.Add(_mapper.Map<League>(leagueExtractModel)));
            }

            return league;
        }

        private async Task AddRound(List<Round> rounds)
        {
            throw new NotImplementedException();
        }

        private async Task AddMatch(Match match)
        {
            throw new NotImplementedException();
        }

        private async void DecoupleData(LeagueExtractModel leagueExtractModel)
        {
            await AddLeague(leagueExtractModel);
        }

        

    }
}
