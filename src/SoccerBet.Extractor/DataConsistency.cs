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
        private readonly IRoundRepository _roundRepository;
        private readonly IMatchRepository _matchRepository;
        private ILogger _logger;
        public DataConsistency(IMapper mapper,
                               ILeagueRepository leagueRepository,
                               ILogger<DataConsistency> logger, 
                               IRoundRepository roundRepository, 
                               IMatchRepository matchRepository)
        {
            _mapper = mapper;
            _leagueRepository = leagueRepository;
            _logger = logger;
            _roundRepository = roundRepository;
            _matchRepository = matchRepository;
        }

        public async Task ConsistencyRule(List<LeagueExtractModel> leagues)
        {
            foreach(var league in leagues)
            {
                _logger.LogInformation($"Liga:{league.Name} - Quantidade de rodadas:{league.Rounds.Count}");
                 await DecoupleData(league);
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
            foreach (var round in rounds)
            {
                _logger.LogInformation($"rodada:{round.Number} - quantidade de partidas:{round.Matchs.Count()}");
                await _roundRepository.Add(round);

                foreach (var match in round.Matchs)
                {
                        _logger.LogInformation($"rodada:{round.Number} data partida:{match.MatchDate} - {match.HomeTeam} x {match.AwayTeam}");
                        match.LeagueId = round.LeagueId;
                        match.RoundId = round.Id;
                        match.CreatedAt = DateTime.Now;
                        match.UpdatedAt = DateTime.Now;
                        await AddMatch(match);
                }
            }
        }

        private async Task AddMatch(Match match)
        {
            await _matchRepository.Add(match);
        }

        private async Task DecoupleData(LeagueExtractModel leagueExtractModel)
        {
            var rounds = _mapper.Map<IEnumerable<Round>>(leagueExtractModel.Rounds).ToList();
            var leagueModel = await AddLeague(leagueExtractModel);
            rounds.Select(c => { c.LeagueId = leagueModel.Id; return c; }).ToList();
            await AddRound(rounds);
        }

        

    }
}
